using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AceNetFrame.ace.auto
{
    public class LengthEncoding
    {
        /// <summary>
        /// 粘包长度编码
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] encode(byte[] buffer) {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            //写入消息的长度
            sw.Write(buffer.Length);
            //写入消息体
            sw.Write(buffer);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(),0,result ,0,(int)ms.Length);
            sw.Close();
            ms.Close();
            return result;
        }

        /// <summary>
        /// 粘包长度解码
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static byte[] decode(ref List<byte> cache) {
           // Console.WriteLine(cache.Count);
            if (cache.Count < 4) return null;
            MemoryStream ms = new MemoryStream(cache.ToArray());
            BinaryReader br = new BinaryReader(ms);
            //从缓存中读取int类型的消息体长度
            int length = br.ReadInt32();
            // Console.WriteLine(length);
            //如果消息体长度 大于缓存中数据长度 说明消息没有读取完 等待下次消息到达后再次处理
            if (length > ms.Length - ms.Position) {
                return null;
            }
            //读取正确的缓存数据
            byte[] result = br.ReadBytes(length);
            //清除缓存
            cache.Clear();
            //将读取后的数据剩余的数据写入缓存中
            cache.AddRange(br.ReadBytes((int)(ms.Length-ms.Position)));
            br.Close();
            ms.Close();
            return result;

        }
    }
}
