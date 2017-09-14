using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNet;
using System.IO;
using LitJson;
using GamelolLoginServer.InteractiveMessage;
using GamelolLoginServer.Database;
using GamelolLoginServer.DataMessage;
namespace GamelolLoginServer.LoginServer
{
    public class ServerDateManager
    {
        public static void ReceiveFunctionFromClient(TcpPlayer player, BinaryReader reader)
        {
            int  messageType = reader.ReadInt32();

            LoginMessage loginMessage = JsonMapper.ToObject<LoginMessage>(reader.ReadString());
            
            switch (messageType)
            {
                case 1:
                    Manager.RegistLogic(player, loginMessage);
                    break;
                case 2:
                    Manager.LoginLogic(player, loginMessage);
                    break;
            }
        }
    }

    public class Manager {

        public static void RegistLogic(TcpPlayer player, LoginMessage loginMessage)
        {
            LoginMessageDatabase loginMessageDatabase = new LoginMessageDatabase();
            BinaryWriter write = player.BeginSend(Packet.SelfClientPacket);
            if (loginMessageDatabase.GetPlayerLoginMessageByAccount(loginMessage.account) != null)
            {
               
                write.Write(1);
                write.Write(2);
            }
            else {
                write.Write(1);
                write.Write(1);
                int playerId = new BaseMessageDatabase().InitPlayerBaseMessage();
                PlayerLoginMessage message = new PlayerLoginMessage();
                message.LoginPassword = loginMessage.password;
                message.LoginAccount = loginMessage.account;
                message.LoginPlayer = playerId;
                loginMessageDatabase.InsertPlayerLoginMessage(message);
            }

            player.EndSend();
        }


        public static void LoginLogic(TcpPlayer player, LoginMessage loginMessage)
        {
            

            LoginMessageDatabase loginMessageDatabase = new LoginMessageDatabase();
            BinaryWriter write = player.BeginSend(Packet.SelfClientPacket);
            PlayerLoginMessage message = loginMessageDatabase.GetPlayerLoginMessageByAccount(loginMessage.account);
            write.Write(2);
            if (message!= null)
            {
                if (message.LoginPassword.Equals(loginMessage.password))
                {
                    //have someting to do
                    write.Write(1);
                }
                else {
                    write.Write(3);
                }
            }
            else
            {
                write.Write(2);
            }
            player.EndSend();
        }
    }
}
