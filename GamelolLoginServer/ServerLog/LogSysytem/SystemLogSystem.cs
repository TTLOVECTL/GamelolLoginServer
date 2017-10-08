using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogServerDataMessage;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GamelolLoginServer.Util;
using RpgGame.NetConnection;
namespace GamelolLoginServer.ServerLog.LogSysytem
{
    public class SystemLogSystem
    {
        private  static SystemLogSystem instance=null;

        private static PerformanceCounter cpu;

        //private MEMORY_INFO MemInfo;

        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);

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
          
        }

        /// <summary>
        /// 将系统日志信息发送给日志服务器
        /// </summary>
        public void SendMessageToLogServer() {
            cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            MEMORY_INFO MemInfo;
            MemInfo = new MEMORY_INFO();
            while (true)
            {
                SystemLogMessage systemLogMessage = new SystemLogMessage();
                GlobalMemoryStatus(ref MemInfo);
                string a = MemInfo.dwMemoryLoad.ToString();
                systemLogMessage.memoryAvailable =float.Parse(a)/100;
                var percentage = cpu.NextValue();
                systemLogMessage.cpuLoad = percentage / 100;
                systemLogMessage.serverId = int.Parse(ConfigurationSetting.GetConfigurationValue("serverId"));
                systemLogMessage.serverName = ConfigurationSetting.GetConfigurationValue("serverName");
                NetWorkScript.Instance.write((int)LogType.SYSTEM_LOG,0,0,systemLogMessage);
                System.Threading.Thread.Sleep(60000);
            }
           
        }
    }

    //定义CPU的信息结构    
    [StructLayout(LayoutKind.Sequential)]
    public struct CPU_INFO
    {
        public uint dwOemId;
        public uint dwPageSize;
        public uint lpMinimumApplicationAddress;
        public uint lpMaximumApplicationAddress;
        public uint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public uint dwProcessorLevel;
        public uint dwProcessorRevision;
    }


    //定义内存的信息结构    
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public uint dwTotalPhys;
        public uint dwAvailPhys;
        public uint dwTotalPageFile;
        public uint dwAvailPageFile;
        public uint dwTotalVirtual;
        public uint dwAvailVirtual;
    }


    //定义系统时间的信息结构    
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME_INFO
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
}
