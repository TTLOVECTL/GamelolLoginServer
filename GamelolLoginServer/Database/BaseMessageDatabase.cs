using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamelolLoginServer.DataMessage;
using MySql.Data.MySqlClient;

namespace GamelolLoginServer.Database
{
    /// <summary>
    /// 玩家基本信息数据库的存取
    /// </summary>
    public class BaseMessageDatabase
    {
        private MySqlConnection mySqlConnection = null;

        public BaseMessageDatabase()
        {
            mySqlConnection = DatabaseConnnection.Instcance.GetMyConnection();
        }

        /// <summary>
        /// 根据指定的Id获取玩家的基础信息
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public PlayerBaseMessage GetPlayerBaseMessageByPlayerId(int playerid)
        {
            PlayerBaseMessage playerBaseMessage = null;
            string get_sql = "select * from tb_playerbasemessage where playerid=" + playerid.ToString();
            MySqlCommand mySqlCommand = new MySqlCommand(get_sql, mySqlConnection);
            MySqlDataReader reader = null;
            try
            {
                mySqlConnection.Open();
                reader = mySqlCommand.ExecuteReader();
                playerBaseMessage = new PlayerBaseMessage();
                while (reader.Read())
                {
                    playerBaseMessage.PlayerId = int.Parse(reader[0].ToString());
                    playerBaseMessage.PlayerName = reader[1].ToString();
                    playerBaseMessage.PlayerLevel = int.Parse(reader[2].ToString());
                    playerBaseMessage.PlayerExperence = int.Parse(reader[3].ToString());
                    playerBaseMessage.PlayerHeadImage = reader[4].ToString();
                    playerBaseMessage.PlayerGoldNumber = int.Parse(reader[5].ToString());
                    playerBaseMessage.PlayerDiamondsNumber = int.Parse(reader[6].ToString());
                    playerBaseMessage.PlayerVolumeNumber = int.Parse(reader[7].ToString());
                    playerBaseMessage.PlayerInscriptionNumber = int.Parse(reader[8].ToString());
                    break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlCommand.Dispose();
                reader.Close();
                mySqlConnection.Close();
            }
            return playerBaseMessage;
        }

        /// <summary>
        /// 初始化新的玩家信息
        /// </summary>
        /// <returns></returns>
        public int InitPlayerBaseMessage()
        {
            string insert_sql = "insert into tb_playerbasemessage(playername,playerlevel,playerexperence,playerheadimage,glodnumber,diamondsnumber,volumenumber,inscriptionnumber) " +
                "values('0',0,0,'0',0,0,0,0)";
            string get_sql = "select max(playerid) from tb_playerbasemessage";
            MySqlCommand mySqlCommand = null;
            mySqlCommand = new MySqlCommand(insert_sql, mySqlConnection);

            try
            {
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            mySqlCommand = new MySqlCommand(get_sql, mySqlConnection);
            MySqlDataReader reader = null;
            int playerId = 0;
            try
            {
                reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    playerId = int.Parse(reader[0].ToString());
                    break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                mySqlCommand.Dispose();
                reader.Close();
                mySqlConnection.Close();
            }
            return playerId;

        }

        /// <summary>
        /// 更改指定玩家id的玩家名称
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="playerName"></param>
        public void UpdatePlayerName(int playerId,string playerName) {
            string update_sql = "update tb_playerbasemessage set playername='"+playerName+"' where playerid ="+playerId.ToString();
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
        /// 更改指定玩家Id的玩家当前经验值
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="playerExperence"></param>
        public void UpdatePlayerExperence(int playerId, int playerExperence) {
            string update_sql = "update tb_playerbasemessage set playerexperence=" + playerExperence + " where playerid =" + playerId.ToString();
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
        /// 更改指点玩家Id的等级
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="playerLevel"></param>
        public void UpdatePlayerLevel(int playerId, int playerLevel) {
            string update_sql = "update tb_playerbasemessage set playerlevel=" + playerLevel + " where playerid =" + playerId.ToString();
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
        /// 更改指定玩家的头像框
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="headImage"></param>
        public void UpdatePlayerHeadImage(int playerId, string headImage) {
            string update_sql = "update tb_playerbasemessage set playerheadimage ='" + headImage + "' where playerid =" + playerId.ToString();
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
        /// 更改指定玩家的金币数量
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="glodnumber"></param>
        public void UpdatePlayerGlodnumber(int playerId, int glodnumber) {
            string update_sql = "update tb_playerbasemessage set glodnumber=" + glodnumber + " where playerid =" + playerId.ToString();
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
        /// 更改指定玩家的钻石数量
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="diamondsnumber"></param>
        public void UpdatePlayerDiamondsnumber(int playerId, int diamondsnumber) {
            string update_sql = "update tb_playerbasemessage set diamondsnumber=" + diamondsnumber + " where playerid =" + playerId.ToString();
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
        /// 更改指定玩家的点券数量
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="volumenumber"></param>
        public void UpdatePlayerVolumenumber(int playerId, int volumenumber) {
            string update_sql = "update tb_playerbasemessage set volumenumber=" + volumenumber + " where playerid =" + playerId.ToString();
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
        /// 更改指定玩家的符文碎片数量
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="inscriptionnumber"></param>
        public void UpdatePlayerInscriptionnumber(int playerId, int inscriptionnumber) {
            string update_sql = "update tb_playerbasemessage set inscriptionnumber=" + inscriptionnumber + " where playerid =" + playerId.ToString();
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
