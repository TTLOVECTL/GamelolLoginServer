using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceNetFrame.ace
{
    public class UserTokenPool
    {
        private  Stack<UserToken> pool;

        /// <summary>
        /// 初始化栈的大小
        /// </summary>
      public  UserTokenPool(int size) {
            pool = new Stack<UserToken>();
        }

        /// <summary>
        /// 从栈中取出一个UserToken类型的元素
        /// </summary>
        /// <returns></returns>
        public UserToken pop() {
            if (0 == pool.Count) return null;
            UserToken token = pool.Pop();
            return token;
        }

        /// <summary>
        /// 将一个UserToken类型的元素存入找中
        /// </summary>
        /// <param name="item"></param>
        public void push(UserToken item) {
            if (item != null)
                pool.Push(item);
        }

        /// <summary>
        /// 获取栈的大小
        /// </summary>
        /// <returns></returns>
        public int getSize() {
            return pool.Count;
        }
    }
}
