using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogServerDataMessage
{
    public enum LogType
    {
        /// <summary>
        /// 系统日志
        /// </summary>
        SYSTEM_LOG,

        /// <summary>
        /// 错误日志
        /// </summary>
        ERROR_LOG,

        /// <summary>
        /// 登录日志
        /// </summary>
        LOGIN_LOG,

        /// <summary>
        /// 消费日志
        /// </summary>
        CONSUME_LOG,
        
        /// <summary>
        /// 收入日志
        /// </summary>
        INCOME_LOG,

        /// <summary>
        /// 行为日志
        /// </summary>
        BEHAEIOUS_LOG

    }
}
