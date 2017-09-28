using System.Collections;
using System.Collections.Generic;
//using RpgGame.Enumeration;
//using RpgGame.Interface;

//namespace RpgGame.NetConnection
//{
//    public class MessageManager : MonoBehaviour
//    {
//        IHandle user;
//        IHandle login;
//        // Use this for initialization
//        void Awake()
//        {
//            user = GetComponent<UserHandler>();
//            login = GetComponent<LoginHandler>();
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            while (NetWorkScript.Instance.messageList.Count > 0)
//            {
//                SocketModel model = NetWorkScript.Instance.messageList[0];
//                NetWorkScript.Instance.messageList.RemoveAt(0);
//                StartCoroutine("MessageReceive", model);
//            }
//        }

//        void MessageReceive(SocketModel model)
//        {
//            switch (model.type)
//            {
//                case Protocol.TYPE_USER:
//                    user.MessageReceive(model);
//                    break;
//                case Protocol.TYPE_LOGIN:
//                    login.MessageReceive(model);
//                    break;
//            }
//        }
//    }
//}