using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AceNetFrame.ace
{
    public class UserToken
    {
        /// <summary>
        /// 登录用户玩家ID
        /// </summary>
        public int playerId=0;

        /// <summary>
        /// 接受信息异步事件
        /// </summary>
        public SocketAsyncEventArgs receiveEvent;

        /// <summary>
        /// 发送信息异步事件
        /// </summary>
        public SocketAsyncEventArgs sendEvent;

        /// <summary>
        /// 连接客户端的Socket
        /// </summary>
        public Socket clientSocket;

        /// <summary>
        /// 暂时存储发送给客户端的信息
        /// </summary>
        public Queue<byte[]> writeQueue=new Queue<byte[]>();
        
        /// <summary>
        /// 定义发送信息的委托
        /// </summary>
        /// <param name="e"></param>
        public delegate void SendProcess(SocketAsyncEventArgs e);
        public SendProcess sendProcess;

        /// <summary>
        /// 定义关闭客户端连接的委托
        /// </summary>
        /// <param name="token"></param>
        /// <param name="error"></param>
        public delegate void CloseProcess(UserToken token, string error);
        public CloseProcess closeProcess;

        /// <summary>
        /// 定义关于信息接受处理的委托
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        public delegate void MessageReceive(UserToken token,object message);
        public MessageReceive messageReceive;

        #region 编码解码器委托
        public LengthEncode lEncode;
        public LengthDecode lDecode;
        public SerEncode sEncode;
        public SerDecode sDecode;
        #endregion

        /// <summary>
        /// 是否读客户端发来的信息
        /// </summary>
        private bool isReading =false;

        /// <summary>
        /// 是否将队列里的信息发给客户端
        /// </summary>
        private bool isWriteing=false;

        public List<byte> cache = new List<byte>();

        public UserToken(int buffSize){
            receiveEvent=new SocketAsyncEventArgs ();
            receiveEvent.UserToken=this;
            sendEvent=new SocketAsyncEventArgs ();
            sendEvent.UserToken=this;
            byte[] buff=new byte[buffSize];
            receiveEvent.SetBuffer(buff,0,buff.Length);
        }

        /// <summary>
        /// 接受客户端的信息
        /// </summary>
        /// <param name="buff"></param>
        public void receive(byte[] buff){
            cache.AddRange(buff);
           // Console.WriteLine(buff[1]);
            if(!isReading){
               onData();
            }
        }

        /// <summary>
        /// 处理客户端的信息
        /// </summary>
        public void onData(){
            byte[] buffer=null;
            if(lDecode!=null){
                buffer=lDecode(ref cache);
                if(buffer==null){
                    isReading=false;
                    return;
                }
            }else{
                if(cache.Count ==0) return ;
                buffer=cache.ToArray();
                cache.Clear();
            }
            if (sDecode == null) throw new Exception("message decode process is null");
            if (messageReceive == null) throw new Exception("messageReceive process is null");
            object message=sDecode(buffer);
            messageReceive(this,message);
            onData ();
        }

        /// <summary>
        /// 将队列中的数据发送给客户端
        /// </summary>
        public void onWrite(){
            if(writeQueue.Count==0){
                isWriteing=false;
                return ;
            }
            byte[] buff=writeQueue.Dequeue();
            sendEvent.SetBuffer(buff,0,buff.Length);
            bool result=clientSocket.SendAsync(sendEvent);
            if(!result)
                sendProcess(sendEvent);
        }

        /// <summary>
        /// 调用OnWrite方法
        /// </summary>
        public void sendEnd(){
            onWrite();
        }

        /// <summary>
        /// 将发给客户端的数据写入队列中
        /// </summary>
        /// <param name="buff"></param>
        public void write(byte[] buff){
            if(clientSocket==null){
                closeProcess(this,"客户端与服务器已经断开连接");
                return ;
            }
            writeQueue.Enqueue(buff);
            if(!isWriteing){
                isWriteing=true;
                onWrite();
            }
        }

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        public void close(){
            if(clientSocket==null){
                Console.WriteLine ("ClientSocket is null");
            }
            try{
                writeQueue .Clear();
                cache.Clear();
                isReading=false;
                isWriteing=false;
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket=null;
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
    
}
