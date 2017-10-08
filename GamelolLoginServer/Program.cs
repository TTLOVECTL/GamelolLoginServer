using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GamelolLoginServer.LoginServer;
using GamelolLoginServer.ServerLog.LogSysytem;
using GamelolLoginServer.XmlFile;
namespace GamelolLoginServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //启动游戏登录服务器
            new LoginServerBuild();
            SystemLogSystem.Instance.SendMessageToLogServer();

        }
    }
}
