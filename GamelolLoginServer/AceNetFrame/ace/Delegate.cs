using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceNetFrame.ace
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate byte[] LengthEncode(byte[] value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    /// <returns></returns>
    public delegate byte[] LengthDecode(ref List<byte> cache);
    
    /// <summary>
    /// 定义委托将对象类型转化为字节数组
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public delegate byte[] SerEncode(object message);

    /// <summary>
    /// 定义委托字节数组转化为对象类型
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate object SerDecode(byte[] value);
}
