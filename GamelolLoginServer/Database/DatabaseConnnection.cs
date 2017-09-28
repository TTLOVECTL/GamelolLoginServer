using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GamelolLoginServer.Util;
namespace GamelolLoginServer.Database
{
    /// <summary>
    /// 数据库连接操作
    /// </summary>
    public class DatabaseConnnection
    {
        private static DatabaseConnnection _instance = null;

        private MySqlConnection mySqlConnection = null;

        private DatabaseConnnection() {
            InitConnection();

        }

        private void InitConnection() {
            string m_string_sqlcon = "server="+ConfigurationSetting.GetConfigurationValue("databaseUrl") +";user id="+ ConfigurationSetting.GetConfigurationValue("databaseUsername") +
                ";password="+ ConfigurationSetting.GetConfigurationValue("databasePassword") + ";database="+ ConfigurationSetting.GetConfigurationValue("databaseName");
            mySqlConnection = new MySqlConnection(m_string_sqlcon);
        }

        public static DatabaseConnnection Instcance {
           get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnnection();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取当前与数据库连接的实例
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetMyConnection() {
            if (mySqlConnection == null) {
                InitConnection();
            }
            return mySqlConnection;
        }

        /// <summary>
        /// 销毁与数据库建立的连接
        /// </summary>
        public void ConnectionDispose() {
            if (mySqlConnection != null)
            {
                mySqlConnection.Dispose();
            }
        }
    }
}
