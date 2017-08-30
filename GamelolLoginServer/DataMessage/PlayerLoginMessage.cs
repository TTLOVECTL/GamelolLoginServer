using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamelolLoginServer.DataMessage
{
    public class PlayerLoginMessage
    {
        private int _loginId;

        private int _loginAccount;

        private string _loginPassword;

        private int _loginPlayer;

        /// <summary>
        /// 登录Id
        /// </summary>
        public int LoginId
        {
            get {
                return LoginId;
            }
            set {
                _loginId = value;
            }
        }

        /// <summary>
        /// 登录账户
        /// </summary>
        public int LoginAccount {
            get {
                return LoginAccount;
            }
            set {
                _loginAccount = value;
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword {
            get {
                return _loginPassword;
            }
            set {
                _loginPassword = value;
            }
        }

        /// <summary>
        /// 登录用户
        /// </summary>
        public int LoginPlayer {
            get {
                return _loginPlayer;
            }

            set {
                _loginPlayer = value;
            }
        }
    }
}
