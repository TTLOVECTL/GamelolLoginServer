using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrame.ace.auto;
using AceNetFrame.ace;
using GamelolLoginServer.Util;
namespace GamelolLoginServer.LoginServer
{
    public class LoginServerStart
    {
        public static void StartServer() {
            try
            {
                NetServer server = new NetServer(1000);
                server.lengthEncode = LengthEncoding.encode;
                server.lengthDecode = LengthEncoding.decode;
                server.serDecode = MessageEncoding.Decode;
                server.serEncode = MessageEncoding.Encode;
                server.center = new HandlerCenter();
                server.init();
                server.Start(int.Parse(ConfigurationSetting.GetConfigurationValue("tcpPort")));
            }
            catch (Exception e)
            {
                Console.WriteLine("Server Error " + e.TargetSite);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
            }
        }
    }
}
