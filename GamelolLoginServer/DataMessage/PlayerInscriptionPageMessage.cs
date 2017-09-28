using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.DataMessage
{
    /// <summary>
    /// 符文页信息
    /// </summary>
    public class PlayerInscriptionPageMessage
    {
        private int _inscriptionPageId;

        private string _inscriptionPageName;

        private int _playerId;

        private SortedDictionary<int,int> _redInscriptionList=new SortedDictionary<int, int>();

        private SortedDictionary<int,int> _blueInscriptionList=new SortedDictionary<int, int>();

        private SortedDictionary<int,int> _greenInscriptionList=new SortedDictionary<int, int>();

        /// <summary>
        /// 符文页Id
        /// </summary>
        public int InscriptionPageId {
            get => _inscriptionPageId;
            set => _inscriptionPageId = value;
        }

        /// <summary>
        /// 符文页名称
        /// </summary>
        public string  InscriptionPageName {
            get => _inscriptionPageName;
            set => _inscriptionPageName = value;
        }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public int PlayerId {
            get => _playerId;
            set => _playerId = value;
        }

        /// <summary>
        /// 符文页红色符文
        /// </summary>
        public SortedDictionary<int, int> RedInscriptionList {
            get => _redInscriptionList;
            set => _redInscriptionList = value;
        }

        /// <summary>
        /// 符文页蓝色符文
        /// </summary>
        public SortedDictionary<int, int> BlueInscriptionList {
            get => _blueInscriptionList;
            set => _blueInscriptionList = value;
        }

        /// <summary>
        /// 符文页绿色符文
        /// </summary>
        public SortedDictionary<int, int> GreenInscriptionList {
            get => _greenInscriptionList;
            set => _greenInscriptionList = value;
        }
    }
}
