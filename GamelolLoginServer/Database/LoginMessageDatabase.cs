using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GamelolLoginServer.DataMessage;

namespace GamelolLoginServer.Database
{
    /// <summary>
    /// 用户登录信息数据库存取操作
    /// </summary>
    public class LoginMessageDatabase
    {
        private MySqlConnection mySqlConnection = null;

        public LoginMessageDatabase() {
            mySqlConnection = DatabaseConnnection.Instcance.GetMyConnection();

        }

        /// <summary>
        /// 根据指定的账户查找登录信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public PlayerLoginMessage GetPlayerLoginMessageByAccount(int account){
            PlayerLoginMessage playerLoginMessage = null;
            MySqlCommand cmd = new MySqlCommand("select * from tb_playloginmessage where loginaccount="+account.ToString(), mySqlConnection);
            MySqlDataReader reader = null;
            try
            {
                mySqlConnection.Open();
                reader = cmd.ExecuteReader();
                playerLoginMessage = new PlayerLoginMessage();
                while (reader.Read())
                {
                    playerLoginMessage.LoginId = int.Parse(reader[0].ToString());
                    playerLoginMessage.LoginAccount = int.Parse(reader[1].ToString());
                    playerLoginMessage.LoginPassword = reader[2].ToString();
                    playerLoginMessage.LoginPlayer = int.Parse( reader[3].ToString());
                    break;
                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                reader.Close();
                mySqlConnection.Close();
            }
            return playerLoginMessage;
        }

        /// <summary>
        /// 向数据库中插入登录账户信息
        /// </summary>
        /// <param name="playerLoginMessage"></param>
        public void InsertPlayerLoginMessage(PlayerLoginMessage playerLoginMessage) {
            string insert_sql = "insert into tb_playloginmessage(loginAccount,loginPassword,playerid) values(" + playerLoginMessage.LoginAccount +
                ",'" + playerLoginMessage.LoginPassword + "'," + playerLoginMessage.LoginPlayer + ")";
            MySqlCommand cmd = new MySqlCommand(insert_sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                mySqlConnection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 删除指定账号的登录信息
        /// </summary>
        /// <param name="account"></param>
        public void DeletePlayerLoginMessageByAccount(int account) {
            string delete_sql = "delete from tb_playloginmessage where loginaccount=" + account.ToString();
            MySqlCommand cmd = new MySqlCommand(delete_sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
                cmd.Dispose();
            }
        }


    }

}
