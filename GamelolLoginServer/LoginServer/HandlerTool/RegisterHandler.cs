using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrame.ace.auto;
using AceNetFrame.ace;
using LitJson;
using GamelolLoginServer.InteractiveMessage;
using GamelolLoginServer.Database;
using GamelolLoginServer.DataMessage;

namespace GamelolLoginServer.LoginServer.HandlerTool
{
    public class RegisterHandler : HandlerInterface
    {
        public void ClientClose(UserToken token)
        {
            throw new NotImplementedException();
        }

        public void MessageRecevie(UserToken token, SocketModel message)
        {
            LoginMessage loginMessage = JsonMapper.ToObject<LoginMessage>(message.getMessage<string>());
            LoginMessageDatabase loginMessageDatabase = new LoginMessageDatabase();
            if (loginMessageDatabase.GetPlayerLoginMessageByAccount(loginMessage.account) != null)
            {
                SocketModel socketModel = new SocketModel();
                socketModel.type = 1;
                socketModel.area = 1;
                socketModel.command = 2;
                SendtoClient.write(token, socketModel);
            }
            else {
                SocketModel socketModel = new SocketModel();
                socketModel.type = 1;
                socketModel.area = 1;
                socketModel.command = 1;
                SendtoClient.write(token, socketModel);
                int playerId = new BaseMessageDatabase().InitPlayerBaseMessage();
                PlayerLoginMessage message = new PlayerLoginMessage();
                message.LoginPassword = loginMessage.password;
                message.LoginAccount = loginMessage.account;
                message.LoginPlayer = playerId;
                loginMessageDatabase.InsertPlayerLoginMessage(message);
            }
        }
    }
}
