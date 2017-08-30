using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.DataMessage
{
    public class PlayerBaseMessage
    {
        private int _playerId;

        private string _playerName;

        private int _playerLevel;

        private int _playerExperence;

        private string _playerHeadImage;

        private int _playerGoldNumber;

        private int _playerDiamondsNumber;

        private int _playerVolumeNumber;

        private int _playerInscriptionNumber;

        /// <summary>
        /// 玩家Id
        /// </summary>
        public int PlayerId {
            get => _playerId;
            set => _playerId = value;
        }

        /// <summary>
        /// 玩家昵称
        /// </summary>
        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        /// <summary>
        /// 玩家等级
        /// </summary>
        public int PlayerLevel {
            get => _playerLevel;
            set => _playerLevel = value;
        }

        /// <summary>
        /// 玩家当前等级经验
        /// </summary>
        public int PlayerExperence {
            get => _playerExperence;
            set => _playerExperence = value;
        }

        /// <summary>
        /// 玩家当前头像的地址
        /// </summary>
        public string PlayerHeadImage
        {
            get => _playerHeadImage;
            set => _playerHeadImage = value;
        }

        /// <summary>
        /// 玩家当前的金币数量
        /// </summary>
        public int PlayerGoldNumber {
            get => _playerGoldNumber;
            set => _playerGoldNumber = value;
        }

        /// <summary>
        /// 玩家当前的钻石数量
        /// </summary>
        public int PlayerDiamondsNumber {
            get => _playerDiamondsNumber;
            set => _playerDiamondsNumber = value;
        }

        /// <summary>
        /// 玩家当前的点券数量
        /// </summary>
        public int PlayerVolumeNumber {
            get => _playerVolumeNumber;
            set => _playerVolumeNumber = value;
        }

        /// <summary>
        /// 玩家当前的符文碎片数量
        /// </summary>
        public int PlayerInscriptionNumber {
            get => _playerInscriptionNumber;
            set => _playerInscriptionNumber = value; }
    }
}
