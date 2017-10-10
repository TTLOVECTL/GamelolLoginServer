using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AceNetFrame.ace.auto
{
    public class MessageEncoding
    {
        public static byte[] Encode(object value) { 
            SocketModel sm=value as SocketModel;
            ByteArray ba=new ByteArray ();
            ba.write(sm.type );
            ba.write (sm.area);
            ba.write(sm.command);
            if(sm.message!=null ){
                byte[] messaage=SerializeUtil.encode(sm.message);
                ba.write(messaage);
            }
            byte[] result=ba.getBuff ();
            ba.close();
            return  result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Decode(byte[] value) {
            ByteArray ba = new ByteArray(value);
            SocketModel sm = new SocketModel();
            int type;
            int area;
            int commmand;
            ba.read(out type);
            ba.read(out area);
            ba.read(out commmand );
            sm.type = type;
            sm.area = area;
            sm.command = commmand;
            if (ba.Readnable) {
                byte[] message;
                ba.read(out message,ba.Length -ba.Postion);
                sm.message = SerializeUtil.decode(message);
            }
            ba.close();
            return sm;

        }
    }
}
