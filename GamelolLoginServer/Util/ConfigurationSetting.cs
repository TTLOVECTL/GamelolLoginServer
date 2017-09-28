using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace GamelolLoginServer.Util
{
    public class ConfigurationSetting
    {
        /// <summary>
        ///获取配置文件的值
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetConfigurationValue(string keyname) {
            string keyValue= ConfigurationManager.AppSettings[keyname];
            return keyValue;

        }
    }
}