using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GamelolLoginServer.TextFile
{
    /// <summary>
    ///文本文件读取类
    /// </summary>
    public class TextFileRead
    {
        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream fileStream = null;

        /// <summary>
        /// 文件读取流
        /// </summary>
        private StreamReader streamReader = null;

        /// <summary>
        /// 初始化文件读入流和文件流
        /// </summary>
        /// <param name="path"></param>
        public TextFileRead(string path) {
            fileStream = new FileStream(path ,FileMode.Open);
            streamReader = new StreamReader(fileStream,Encoding.Default);
        }

        /// <summary>
        /// 从文件里读取所有的信息
        /// </summary>
        /// <returns></returns>
        public List<string> ReadLine() {
            List<string> data = new List<string>();
            String line;
            while ((line = streamReader.ReadLine()) != null)
            {
                data.Add(line);
            }
            return data;
        }

    }
}
