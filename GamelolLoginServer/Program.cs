using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GamelolLoginServer.LoginServer;
using GamelolLoginServer.DataMessage;
using GamelolLoginServer.Database;
namespace GamelolLoginServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoginServerBuild();
            ///GamelolLoginServer.XmlFile.SavePlayerData.SavaDataToXml(1);
        }
    }
}
