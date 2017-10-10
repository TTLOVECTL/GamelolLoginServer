using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrame.ace;
using AceNetFrame.ace.auto;
using GamelolLoginServer.LoginServer.HandlerTool;
namespace GamelolLoginServer.LoginServer
{
    public class HandlerCenter : AbsHandleCenter
    {
        private HandlerInterface loginHander;

        private HandlerInterface registerHander;

        public HandlerCenter() {
            registerHander = new RegisterHandler();
            loginHander = new LoginHandler();
        }

        public override void ClientClose(UserToken token, string error)
        {
            Console.WriteLine("客户端"+token.clientSocket.ToString()+"断开了与登录服务器的连接");
        }

        public override void MessageRecive(UserToken token, object message)
        {
            SocketModel socketModel = (SocketModel)message;
            switch (socketModel.type) {
                case 1:
                    registerHander.MessageRecevie(token,socketModel);
                    break;
                case 2:
                    loginHander.MessageRecevie(token,socketModel);
                    break;
            }
        }
    }
}
