using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace AceNetFrame.ace
{
    public class SerializeUtil
    {
        public static byte[] encode(object value) {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms,value);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(),0,result,0,(int)ms.Length);
            ms.Close();
            return result;
        }

        public static object decode(byte[] value) {
            MemoryStream ms = new MemoryStream(value );
            BinaryFormatter bf = new BinaryFormatter();
            //bf.Binder = new UBinder();
            object result = bf.Deserialize(ms);
            ms.Close();
            return result;
        }

    }

    public class UBinder : SerializationBinder {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            return ass.GetType(typeName);
        }
    } 

}
