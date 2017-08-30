using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.ServerLog.LogSysytem
{
    public class LoginLogSystem
    {
        private static  LoginLogSystem _instance=null;

        private LoginLogSystem() {

        }

        public static LoginLogSystem Instance {
            get {
                if (_instance == null) {
                    _instance = new LoginLogSystem();
                }
                return _instance;
            }
        }

    }
}
