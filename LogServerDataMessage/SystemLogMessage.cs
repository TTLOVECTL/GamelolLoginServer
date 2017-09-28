using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogServerDataMessage
{
    [System.Serializable]
    public class SystemLogMessage
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime dataTime;

        /// <summary>
        /// 服务器ID
        /// </summary>
        public int serverId;

        /// <summary>
        /// 服务器名称
        /// </summary>
        public string serverName;

        /// <summary>
        /// cpu占用率
        /// </summary>
        public float cpuLoad;

        /// <summary>
        /// 剩余可用内存
        /// </summary>
        public long memoryAvailable;

        /// <summary>
        /// 剩余物理内存
        /// </summary>
        public long physicalMemory;

        public SystemLogMessage()
        {
            dataTime = DateTime.Now;
        }
    }
}
