using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogServerDataMessage;

namespace GamelolLoginServer.ServerLog.LogSysytem
{
    public class SystemLogSystem
    {
        private  static SystemLogSystem instance=null;

        private SystemLogMessage systemLogMessage;

        private SystemLogSystem() {
            InitSystemLogMessage();
        }

        public static SystemLogSystem Instance {
            get {
                if (instance == null) {
                    instance = new SystemLogSystem();
                }
                return instance;
            }
        }

        private void InitSystemLogMessage() {
            systemLogMessage = new SystemLogMessage();
        }

    }
}
