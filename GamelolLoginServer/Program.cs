using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GamelolLoginServer.LoginServer;
using GamelolLoginServer.DataMessage;
using GamelolLoginServer.Database;
using GamelolLoginServer.TextFile;
using GamelolLoginServer.ServerLog.LogMessage;
using GamelolLoginServer.ServerLog.LogSysytem;
namespace GamelolLoginServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //new LoginServerBuild();
            LoginLogMessage loginLogMessage = new LoginLogMessage();
            loginLogMessage.loginPlayerId = 1995;
            loginLogMessage.loginIP="192.168.6.114";
            LoginLogSystem.Instance.LoginLogWrite(loginLogMessage);
               
        }
    }
}
