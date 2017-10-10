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
using LogServerDataMessage;
using GamelolLoginServer.Util;
namespace GamelolLoginServer.LoginServer.HandlerTool
{
    public class LoginHandler : HandlerInterface
    {
        public void ClientClose(UserToken token)
        {
            throw new NotImplementedException();
        }

        public void MessageRecevie(UserToken token, SocketModel message)
        {
            LoginMessage loginMessage = JsonMapper.ToObject<LoginMessage>(message.getMessage<string>());
            LoginMessageDatabase loginMessageDatabase = new LoginMessageDatabase();
            PlayerLoginMessage message1 = loginMessageDatabase.GetPlayerLoginMessageByAccount(loginMessage.account);
            SocketModel socketModel = new SocketModel();
            socketModel.type = 1;
            socketModel.area = 2;
            if (message1 != null) {
                if (message1.LoginPassword.Equals(loginMessage.password))
                {
                    //记录登陆日志，并将器转发给日志服务器
                    LoginLogMessage loginLogMessage = new LoginLogMessage();
                    loginLogMessage.loginIP = token.clientSocket.LocalEndPoint.ToString();
                    loginLogMessage.loginPlayerId = message1.LoginPlayer;
                    RpgGame.NetConnection.NetWorkScript.Instance.write((int)LogType.LOGIN_LOG, 0, 0, loginLogMessage);
                    socketModel.command = 1;

                    CenterMessage centerMessage = new CenterMessage();
                    centerMessage.centerServerIp = ConfigurationSetting.GetConfigurationValue("centerServerIP");
                    centerMessage.centerServerPort = int.Parse(ConfigurationSetting.GetConfigurationValue("centerServerPort"));
                    string messageStr = JsonMapper.ToJson(centerMessage);
                    Console.WriteLine(messageStr);
                    socketModel.message = messageStr;
                }
                else
                {
                    socketModel.command = 3;
                }

            }
            else
            {
                socketModel.command = 2;
            }
            SendtoClient.write(token,socketModel);
        }
    }
}
