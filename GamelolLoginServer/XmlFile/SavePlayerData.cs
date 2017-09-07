using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamelolLoginServer.DataMessage;
using GamelolLoginServer.Database;
using System.Data;
using System.Xml;

namespace GamelolLoginServer.XmlFile
{
    public class SavePlayerData
    {
        public static void SavaDataToXml(int playerid) {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            XmlNode root = xmlDoc.CreateElement("PlayerMessage");
            xmlDoc.AppendChild(root);

            PlayerBaseMessage playerBaseMessage = new BaseMessageDatabase().GetPlayerBaseMessageByPlayerId(playerid);
            XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "BaseMessage", null);
            CreateNode(xmlDoc, node1, "playerId", playerBaseMessage.PlayerId.ToString());
            CreateNode(xmlDoc, node1, "playerName", playerBaseMessage.PlayerName);
            CreateNode(xmlDoc, node1, "playerLevel", playerBaseMessage.PlayerLevel.ToString());
            CreateNode(xmlDoc, node1, "playerExperence", playerBaseMessage.PlayerExperence.ToString());
            CreateNode(xmlDoc, node1, "playerHeadImage", playerBaseMessage.PlayerHeadImage.ToString());
            CreateNode(xmlDoc, node1, "playerGoldNumber", playerBaseMessage.PlayerGoldNumber.ToString());
            CreateNode(xmlDoc, node1, "playerDiamondsNumber", playerBaseMessage.PlayerDiamondsNumber.ToString());
            CreateNode(xmlDoc, node1, "playerVolumeNumber", playerBaseMessage.PlayerVolumeNumber.ToString());
            CreateNode(xmlDoc, node1, "playerInscriptionNumber", playerBaseMessage.PlayerInscriptionNumber.ToString());
            root.AppendChild(node1);

            List<PlayerInscriptionMessage> playerInscriptionMessage = new InscriptionMessageDatabase().GetPlayerInscriptionListById(playerid);
            XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "InscriptionMessage", null);
            foreach (PlayerInscriptionMessage item in playerInscriptionMessage) {
                XmlNode nodeItem = CreateNode(xmlDoc, node2, "Inscription", "");
                CreateNode(xmlDoc, nodeItem, "inscriptionId", item.InscriptionId.ToString());
                CreateNode(xmlDoc, nodeItem, "inscriptionNumber", item.InscriptionNumber.ToString());
                CreateNode(xmlDoc, nodeItem, "inscriptionUseNumber", item.InscriptionUserNumber.ToString());
            }
            root.AppendChild(node2);

            List<PlayerInscriptionPageMessage> playerInscriptionPageMessage = new InscriptionPageMessageDatabase().GetInscriptionPageList(playerid);
            XmlNode node3 = xmlDoc.CreateNode(XmlNodeType.Element, "InscriptionPageMessage", null);
            foreach (PlayerInscriptionPageMessage item in playerInscriptionPageMessage) {
                XmlNode nodeItem = CreateNode(xmlDoc, node3, "InscriptionPage", "");
                CreateNode(xmlDoc, nodeItem, "InscriptionPageId", item.InscriptionPageId.ToString());
                CreateNode(xmlDoc, nodeItem, "InscriptionPageName", item.InscriptionPageName);
                foreach (KeyValuePair<int, int> redItem in item.RedInscriptionList) {
                    XmlElement  node4=(XmlElement)CreateNode(xmlDoc, nodeItem, "RedInscription", redItem.Value.ToString());
                    node4.SetAttribute("SoitId",redItem.Key.ToString());
                }
                foreach (KeyValuePair<int, int> redItem in item.BlueInscriptionList)
                {
                    XmlElement node4 = (XmlElement)CreateNode(xmlDoc, nodeItem, "BlueInscription", redItem.Value.ToString());
                    node4.SetAttribute("SoitId", redItem.Key.ToString());
                }
                foreach (KeyValuePair<int, int> redItem in item.GreenInscriptionList)
                {
                    XmlElement node4 = (XmlElement)CreateNode(xmlDoc, nodeItem, "GreenInscription", redItem.Value.ToString());
                    node4.SetAttribute("SoitId", redItem.Key.ToString());
                }
            }
            root.AppendChild(node3);

            try
            {
                xmlDoc.Save("D://"+playerid.ToString()+".xml");
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }
        }

        private static  XmlNode CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
            return node;
        }

    }
}
