using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogServerDataMessage;
using GamelolLoginServer.TextFile;
namespace GamelolLoginServer.ServerLog.LogSysytem
{
    public class LoginLogSystem
    {
        private static  LoginLogSystem _instance=null;

        /// <summary>
        /// 文本文件写入流
        /// </summary>
        private TextFileWrite textFileWrite = null;

        private LoginLogSystem() {
            textFileWrite = new TextFileWrite("D:\\GamelolLogin.txt");
        }

        public static LoginLogSystem Instance {
            get {
                if (_instance == null) {
                    _instance = new LoginLogSystem();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 将日志信息写入到文本文件中
        /// </summary>
        /// <param name="loginLogMessage"></param>
        public void LoginLogWrite(LoginLogMessage loginLogMessage) {
            string data = "[" + loginLogMessage.loginTime + "] " + loginLogMessage.loginPlayerId.ToString() + " " + loginLogMessage.loginIP; 
            textFileWrite.WirteLine(data);
        }
    }
}
