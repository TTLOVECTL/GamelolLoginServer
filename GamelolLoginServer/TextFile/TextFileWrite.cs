using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GamelolLoginServer.TextFile
{
    /// <summary>
    /// 文本信息写入类
    /// </summary>
    public class TextFileWrite
    {
        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream fileStream = null;

        /// <summary>
        /// 文件写入流
        /// </summary>
        private StreamWriter streamWriter = null;

        /// <summary>
        /// 构造函数，初始化文件流和文件写入流
        /// </summary>
        /// <param name="path"></param>
        public TextFileWrite(string path) {
            fileStream = new FileStream(path,FileMode.Append);
            streamWriter = new StreamWriter(fileStream);
        }

        /// <summary>
        /// 将指定数据写入Text文件中，换行
        /// </summary>
        /// <param name="data"></param>
        public void WirteLine(string data) {
            streamWriter.WriteLine(data);
            streamWriter.Flush();
        }

        /// <summary>
        /// 将指定数据写入Text文件中，不换行
        /// </summary>
        /// <param name="data"></param>
        public void Write(string data) {
            streamWriter.Write(data);
            streamWriter.Flush();
        }

        /// <summary>
        /// 关闭写入流和文件流
        /// </summary>
        public void CloseStream()
        {
            fileStream.Close();
            streamWriter.Close();
        }
    }
}
