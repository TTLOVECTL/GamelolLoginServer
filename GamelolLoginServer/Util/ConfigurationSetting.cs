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
        public static string GetConfigurationValue(string keyname) {
            System.Configuration.AppSettingsReader appSettingsReader = new AppSettingsReader();
            //return System.con.AppSettings[keyname];
            return null;

        }
    }
}