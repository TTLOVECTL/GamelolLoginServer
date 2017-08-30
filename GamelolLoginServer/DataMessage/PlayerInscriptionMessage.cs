using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.DataMessage
{
    /// <summary>
    /// 玩家拥有的符文信息
    /// </summary>
    public class PlayerInscriptionMessage
    {
        private int _playerId;

        private int _inscriptionId;

        private int _inscriptionNumber;

        private int _inscriptionUserNumber;

        /// <summary>
        /// 玩家Id
        /// </summary>
        public int PlayerId {
            get => _playerId;
            set => _playerId = value;
        }

        /// <summary>
        /// 符文ID
        /// </summary>
        public int InscriptionId {
            get => _inscriptionId;
            set => _inscriptionId = value;
        }

        /// <summary>
        /// 符文数量
        /// </summary>
        public int InscriptionNumber {
            get => _inscriptionNumber;
            set => _inscriptionNumber = value;
        }

        /// <summary>
        /// 当前符文的使用数量
        /// </summary>
        public int InscriptionUserNumber {
            get => _inscriptionUserNumber;
            set => _inscriptionUserNumber = value;
        }
    }
}
