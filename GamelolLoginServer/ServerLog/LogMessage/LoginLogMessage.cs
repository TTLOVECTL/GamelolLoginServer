using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.ServerLog.LogMessage
{
    /// <summary>
    /// 登录日志系统的信息
    /// </summary>
    [System.Serializable]
    public class LoginLogMessage
    {
        /// <summary>
        ///登录时间
        /// </summary>
        public string loginTime;

        /// <summary>
        /// 登录玩家ID
        /// </summary>
        public int loginPlayerId;

        /// <summary>
        /// 登录的IP地址
        /// </summary>
        public string loginIP;
    }
}
