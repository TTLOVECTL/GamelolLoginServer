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
    /// 玩家基本符文页信息数据库读取
    /// </summary>
    public class InscriptionPageMessageDatabase
    {
        private MySqlConnection mySqlConnection = null;

        public InscriptionPageMessageDatabase()
        {
            mySqlConnection = DatabaseConnnection.Instcance.GetMyConnection();
        }

        /// <summary>
        ///获取指定玩家拥有的符文页
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public List<PlayerInscriptionPageMessage> GetInscriptionPageList(int playerid) {
            List<PlayerInscriptionPageMessage> inscriptionPageList = new List<PlayerInscriptionPageMessage>();
            string get_sql = "select * from tb_playerinscriptionpagemessage where playerid=" + playerid.ToString() ;
            MySqlCommand mySqlCommand = new MySqlCommand(get_sql, mySqlConnection);
            MySqlDataReader reader = null;
            try
            {
                mySqlConnection.Open();
                reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    PlayerInscriptionPageMessage playerInscriptionPageMessage = new PlayerInscriptionPageMessage();
                    playerInscriptionPageMessage.PlayerId = int.Parse(reader[1].ToString());
                    playerInscriptionPageMessage.InscriptionPageId = int.Parse(reader[2].ToString());
                    playerInscriptionPageMessage.InscriptionPageName = reader[3].ToString();
                    string[] redstring = reader[4].ToString().Split(new char[] { '/' });
                    for (int i = 0; i < redstring.Length; i++) { 
                        playerInscriptionPageMessage.RedInscriptionList.Add(i+1,int.Parse(redstring[i]));
                    }
                   
                    string[] greenString = reader[5].ToString().Split(new char[] {'/'});
                    for (int i = 0; i < greenString.Length; i++) {
                        playerInscriptionPageMessage.GreenInscriptionList.Add(i+1, int.Parse(greenString[i]));
                    }
                    string[] blueString = reader[6].ToString().Split(new char[] { '/' });
                    for (int i = 0; i < blueString.Length; i++) {
                        playerInscriptionPageMessage.BlueInscriptionList.Add(i+1, int.Parse(blueString[i]));
                    }
                    inscriptionPageList.Add(playerInscriptionPageMessage);
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("1"+ex.Message);
            }
            finally
            {
                mySqlCommand.Dispose();
                reader.Close();
                mySqlConnection.Close();
            }
            return inscriptionPageList;
        }

        /// <summary>
        /// 更新指定玩家指点符文页的名称
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="inscriptionPageid"></param>
        /// <param name="incriptionPageName"></param>
        public void UpdateIncriptionPageName(int playerid, int inscriptionPageid, string incriptionPageName) {
            string update_sql = "update tb_playerinscriptionpagemessage set inscriptionpagename= +'" + incriptionPageName +
               "' where playerid =" + playerid.ToString() + " and inscriptionpageid="+inscriptionPageid.ToString();
            MySqlCommand cmd = new MySqlCommand(update_sql, mySqlConnection);
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

        /// <summary>
        /// 更新指定玩家指点符文页的红色符文
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="inscriptionPageId"></param>
        /// <param name="redIncription"></param>
        public void UpdateInscriptionPageRedInscription(int playerid, int inscriptionPageId, string redIncription) {
            string update_sql = "update tb_playerinscriptionpagemessage set inscriptionsoltred= +'" + redIncription +
              "' where playerid =" + playerid.ToString() + " and inscriptionpageid=" + inscriptionPageId.ToString();
            MySqlCommand cmd = new MySqlCommand(update_sql, mySqlConnection);
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

        /// <summary>
        /// 更新指定玩家指定符文页的蓝色符文
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="incriptionPageId"></param>
        /// <param name="blueInscription"></param>
        public void UpdateInscriptionPageBlueInscription(int playerid, int incriptionPageId, string blueInscription) {
            string update_sql = "update tb_playerinscriptionpagemessage set inscriptionsoltblue= +'" + blueInscription +
              "' where playerid =" + playerid.ToString() + " and inscriptionpageid=" + incriptionPageId.ToString();
            MySqlCommand cmd = new MySqlCommand(update_sql, mySqlConnection);
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

        /// <summary>
        /// 更新指定玩家指定符文页的绿色符文
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="inscriptionPageId"></param>
        /// <param name="greenInscription"></param>
        public void UpdateIncriptionPageGreenInscription(int playerid, int inscriptionPageId, string greenInscription) {
            string update_sql = "update tb_playerinscriptionpagemessage set inscriptionsoltgreen= +'" + greenInscription +
              "' where playerid =" + playerid.ToString() + " and inscriptionpageid=" + inscriptionPageId.ToString();
            MySqlCommand cmd = new MySqlCommand(update_sql, mySqlConnection);
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
