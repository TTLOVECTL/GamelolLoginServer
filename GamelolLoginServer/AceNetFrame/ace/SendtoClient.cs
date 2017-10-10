using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceNetFrame.ace.auto;

namespace AceNetFrame.ace
{
    public class SendtoClient
    {
        public static void write(UserToken token,SocketModel model)
        {
            //Console.WriteLine("有消息发出");
            byte[] bs = MessageEncoding.Encode(model);
            bs = LengthEncoding.encode(bs);
            token.write(bs);
        }

        public static void write(UserToken token, int type, int area, int command, object message) {
            SocketModel model =new  SocketModel(type, area, command, message);
            SendtoClient.write(token,model);

        }
        public  SocketModel createSocketModel(int type, int area, int command, object message)
        {
            return new SocketModel(type, area, command, message);
        }
    }
}
