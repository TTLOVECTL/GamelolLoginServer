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
namespace GamelolLoginServer.LoginServer
{
    public class ServerDateManager
    {
        public static void ReceiveFunctionFromClient(TcpPlayer player, BinaryReader reader)
        {
            //解析
            string functionName = reader.ReadString();

            //UserInfo userinfo = JsonMapper.ToObject<UserInfo>(reader.ReadString());
            ////开始往客户端发送数据
            //switch (functionName)
            //{
            //    case "注册":

            //        Manager.RegistLogic(player, userinfo);

            //        break;
            //    case "登录":
            //        Manager.LoginLogic(player, userinfo);

            //        break;
            //}
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
            }

            player.EndSend();
        }


        public static void LoginLogic(TcpPlayer player, LoginMessage loginMessage)
        {
            //这里只是模拟下，这里只是简单讲登录后的获得得用户角色信息记录下来
            //可以自行修改，比如可以向本地服务器中提取该账号用户信息
            // RoleInfo roleInfo = new RoleInfo();

            LoginMessageDatabase loginMessageDatabase = new LoginMessageDatabase();
            BinaryWriter write = player.BeginSend(Packet.SelfClientPacket);

            if (loginMessageDatabase.GetPlayerLoginMessageByAccount(loginMessage.account) != null)
            {
                write.Write(1);
                write.Write(2);
            }
            else
            {
                write.Write(1);
                write.Write(1);
            }

            player.EndSend();
        }
    }
}
