using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AceNetFrame.ace
{
    public class ByteArray
    {
        private MemoryStream ms = new MemoryStream();

        private BinaryReader br;

        private BinaryWriter bw;

        /// <summary>
        /// 初始化数值
        /// </summary>
        /// <param name="buffer"></param>
        public ByteArray(byte[] buffer) {
            ms = new MemoryStream(buffer);
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        /// <summary>
        /// 
        /// </summary>
        public ByteArray() {
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        /// <summary>
        /// 获取当前数组中设置流的当前位置
        /// </summary>
        public int Postion {
            get { return (int)ms.Position; }
        }

        /// <summary>
        /// 获取当前流的长度
        /// </summary>
        public int Length {
            get { return (int)ms.Length; }
        }

        /// <summary>
        /// 判断当前位置流是否可读
        /// </summary>
        public bool Readnable {
            get { return ms.Length > ms.Position; }
        }

        #region 往流中写入数据
        public void write(int value)
        {
            bw.Write(value);
        }

        public void write(byte value)
        {
            bw.Write(value);
        }

        public void write(bool value)
        {
            bw.Write(value);
        }

        public void write(string value)
        {
            bw.Write(value);
        }

        public void write(byte[] value)
        {
            bw.Write(value);
        }

        public void write(double value)
        {
            bw.Write(value);
        }

        public void write(float value)
        {
            bw.Write(value);
        }

        public void write(long value)
        {
            bw.Write(value);
        }
        #endregion

        #region 从流中读出数据
        public void read(out int value)
        {
            value = br.ReadInt32();
        }

        public void read(out byte value)
        {
            value = br.ReadByte();
        }

        public void read(out bool value)
        {
            value = br.ReadBoolean();
        }

        public void read(out string value)
        {
            value = br.ReadString();
        }

        public void read(out byte[] value, int length)
        {
            value = br.ReadBytes(length);
        }

        public void read(out double value)
        {
            value = br.ReadDouble();
        }

        public void read(out float value)
        {
            value = br.ReadSingle();
        }

        public void read(out long value)
        {
            value = br.ReadInt64();
        }
        #endregion

        /// <summary>
        /// 设置留的当前位置为0
        /// </summary>
        public void reposition()
        {
            ms.Position = 0;
        }

        /// <summary>
        /// 获取当前字节流
        /// </summary>
        /// <returns></returns>
        public byte[] getBuff()
        {
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            return result;
        }

        /// <summary>
        /// 关闭流
        /// </summary>
        public void close() {

            ms.Close();
            bw.Close();
            br.Close();
        }
    }
}
