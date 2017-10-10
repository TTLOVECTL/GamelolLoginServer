using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceNetFrame.ace
{
    public abstract class AbsHandleCenter
    {
        /// <summary>
        /// 用于接受处理用户发来的信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        public abstract void MessageRecive(UserToken token, object message);

        /// <summary>
        /// 关闭连接并显示错误
        /// </summary>
        /// <param name="token"></param>
        /// <param name="error"></param>
        public abstract void ClientClose(UserToken token, string error);
    }
}
