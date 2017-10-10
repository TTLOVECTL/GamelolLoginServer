using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GamelolLoginServer.Util;

namespace AceNetFrame.ace
{
    public class NetServer
    {
        /// <summary>
        /// 服务连接对象
        /// </summary>
        Socket server;

        /// <summary>
        /// 最大连接的用户的数量
        /// </summary>
        private int userMax;

        /// <summary>
        /// 信号量用于控制连接的用户数量
        /// </summary>
        private Semaphore maxAcceptClient;

        /// <summary>
        /// 读取数据时的最大字节
        /// </summary>
        private int buffSize = 1024;

        /// <summary>
        /// 存储用户数据的池子
        /// </summary>
        private UserTokenPool userPool;

        /// <summary>
        /// 抽象的用户信息处理类
        /// </summary>
        public AbsHandleCenter center;

        #region 编码解码器委托
        public LengthEncode lengthEncode;
        public LengthDecode lengthDecode;
        public SerEncode serEncode;
        public SerDecode serDecode;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="max">最大连接的用户数数量</param>
        public NetServer(int max)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            userMax = max;
        }

        /// <summary>
        /// 初始化服务连接
        /// </summary>
        public void init()
        {
            Console.WriteLine("服务器已经启动，端口号为：【"+ ConfigurationSetting.GetConfigurationValue("tcpPort") + "】");
            userPool = new UserTokenPool(100);
            maxAcceptClient = new Semaphore(userMax, userMax);
            if (serEncode == null || serDecode == null) throw new Exception(" message encode or decode is null");
            if (center == null) throw new Exception("Center is null");
            for (int i = 0; i < userMax; i++)
            {
                UserToken token = new UserToken(buffSize);
                token.receiveEvent.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.sendEvent.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.lEncode = lengthEncode;
                token.lDecode = lengthDecode;
                token.sEncode = serEncode;
                token.sDecode = serDecode;
                token.sendProcess = ProcessSend;
                token.messageReceive = center.MessageRecive;
                token.closeProcess = closeClient;
                userPool.push(token);
            }
        }

        /// <summary>
        /// 开始服务器的创建
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {
            server.Bind(new IPEndPoint(IPAddress.Any, port));
            server.Listen(10);
            StartAccept(null);
        }

        /// <summary>
        /// 接受客户端发来的连接请求
        /// </summary>
        /// <param name="acceptEventArg"></param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }

            maxAcceptClient.WaitOne();
            bool isRsiaseEvent = server.AcceptAsync(acceptEventArg);
            if (!isRsiaseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// 对客户端的连接进行处理
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            UserToken userToken = userPool.pop();
            Console.WriteLine("用户登录服务器");
            userToken.clientSocket = e.AcceptSocket;
            StartReceive(userToken);
            StartAccept(e);
        }

        /// <summary>
        /// 开始接受客户端的数据信息
        /// </summary>
        /// <param name="token"></param>
        public void StartReceive(UserToken token)
        {
            try
            {
                bool result = token.clientSocket.ReceiveAsync(token.receiveEvent);
                if (!result)
                {
                    lock (token)
                    {
                        ProcessReceive(token.receiveEvent);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("receive message error" + e);
            }
        }
        
        /// <summary>
        /// 处理接受的客户端数据
        /// </summary>
        /// <param name="e"></param>
        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            UserToken userToken = e.UserToken as UserToken;
            if (userToken.receiveEvent.BytesTransferred > 0 && userToken.receiveEvent.SocketError == SocketError.Success)
            {
                byte[] bs = new byte[userToken.receiveEvent.BytesTransferred];
                Buffer.BlockCopy(userToken.receiveEvent.Buffer, 0, bs, 0, userToken.receiveEvent.BytesTransferred);
                userToken.receive(bs);
                StartReceive(userToken);
            }
            else
            {
                string error = "";
                if (userToken.receiveEvent.SocketError != SocketError.Success)
                {
                    error = userToken.receiveEvent.SocketError.ToString();
                }
                else
                {
                    error = "远程客户端自动断开连接";
                }
                closeClient(userToken,error);
            }
        }

        /// <summary>
        /// 当服务器接受客户端连接挂起始，事件调用该函数，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                ProcessAccept(e);

            }
            catch (Exception error) {
                Console.WriteLine(error.Message);
            }

        }

        /// <summary>
        /// 当服务器接受或发生信息给客户端挂起时，时间调用该函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            UserToken token=e.UserToken as UserToken;
            try
            {
                lock (token)
                {
                    if (e.LastOperation == SocketAsyncOperation.Receive)
                        ProcessReceive(e);
                    else if (e.LastOperation == SocketAsyncOperation.Send)
                        ProcessSend(e);
                }
            }
            catch (Exception e1) {
                Console.WriteLine(e1.Message);
            }
        }

        /// <summary>
        ///服务器发送信息给客户端
        /// </summary>
        /// <param name="e"></param>
        public void ProcessSend(SocketAsyncEventArgs e) {
            UserToken userToken = e.UserToken as UserToken;
            if (e.SocketError != SocketError.Success)
            {
                closeClient(userToken, e.SocketError.ToString());
            }
            else {
                userToken.sendEnd();
            }
        }
        
        /// <summary>
        /// 关闭用户连接
        /// </summary>
        /// <param name="token"></param>
        /// <param name="error"></param>
        public void closeClient(UserToken token, String error) {
            if (token.clientSocket != null) {
                lock (token) {
                    center.ClientClose(token ,error);
                    token.close();
                    maxAcceptClient.Release();
                    userPool.push(token);
                }
            }
        }
    }
}
