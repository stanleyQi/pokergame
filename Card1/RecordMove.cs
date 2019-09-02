using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using static Card1.Setting;

namespace Card1
{
   
    class RecordMove
    {
        public static CardMoveStore cms;

        // 
        /// <summary>method:SetupRecordCapability
        /// create a CardMoveStore for storing the infomation of each step;
        /// perform once only when the application starts
        /// About convert json to object ref: https://blog.csdn.net/sajiazaici/article/details/77647625 
        /// </summary>
        /// <param name="mainform"></param>
        public static void SetupRecordCapability(MainForm mainform)
        {
            // read in json from json file

            // json(string)  <-> c# object
            string jsonText1 = "{\"moves\":[],\"game\":{\"init\":[],\"signature\":\"\"}}";
            cms = JsonConvert.DeserializeObject<CardMoveStore>(jsonText1);
            
            cms.game.init = Tools.ParseScreenShot(mainform);
            cms.game.signature = new DateTime().ToString("yyyyMMddHHmmss");
        }

        /// <summary>method:SaveGame
        /// save game into json file
        /// </summary>
        public static void SaveGame()
        {
            string strCms = JsonConvert.SerializeObject(RecordMove.cms);

            //clear the content existing
            string jsonfile = System.Windows.Forms.Application.StartupPath + @"../../../" + @"/test/" + STORAGE_FILE_NAME;
            FileStream stream = File.Open(jsonfile, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();

            //add new content
            System.IO.File.WriteAllText(jsonfile, strCms);

        }

        /// <summary>method:RecordOneMove
        /// perform once per step moving for storing information into CardMoveStore
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static CardMoveStore RecordOneMove(Card[] cards,Control source,Control target,Form form)
        {
            cms.moves.Add(Tools.ParseMove(cards, source, target, form,cms));
            return cms;
        }
    }

    class Tools
    {
        static string[] panelNameArray = { "undeal", "unuse", "slot1", "slot2", "slot3", "slot4", "slot5", "slot6", "slot7", "set1", "set2", "set3", "set4" };

        /// <summary>method:ParseMove
        /// tools for card moving operation
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="form"></param>
        /// <param name="oldCms"></param>
        /// <returns></returns>
        public static MoveNode ParseMove(Card[] cards, Control source, Control target, Form form, CardMoveStore oldCms)
        {
            MoveNode md = new MoveNode();
            md.cards = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                md.cards[i] = (int)cards[i].Suit * 1000 + (int)cards[i].Face * 10 + ((int)cards[i].Side == 1 ? 1 : 0);
            }
            md.step = oldCms.moves.Count + 1;
            md.source = ParseArea(source.Name);
            md.target = ParseArea(target.Name);
            md.screenshot = ParseScreenShot(form);
            return md;
        }

        /// <summary>method:ParseToCard
        /// tools for card moving operation
        /// </summary>
        /// <param name="intCard"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Card ParseToCard(int intCard,int index)
        {
            //Enum.GetName(typeof(SuitEnum), intSuit)

            Card card = new Card();
            string strCard = intCard.ToString();
            card.Name = strCard;
            //card.Text = strCard+"-"+ ReverseParseAreaFromIndex(index);
            card.Suit = (SuitEnum)(int.Parse(strCard.Substring(0, 1)));
            card.Face = (FaceEnum)(int.Parse(strCard.Substring(1, 2)));
            card.Side = (BackEnum)(int.Parse(strCard.Substring(3, 1)) == 0 ? -1 : 1);
            card.Area = ReverseParseAreaFromIndex(index);

            return card;

        }

        /// <summary>method:ParseToCardFromNumber
        /// tools for card moving operation
        /// </summary>
        /// <param name="intCard"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public static Card ParseToCardFromNumber(int intCard, int area)
        {
            //Enum.GetName(typeof(SuitEnum), intSuit)

            Card card = new Card();
            string strCard = intCard.ToString();
            card.Name = strCard;
            //card.Text = strCard + "-" +ReverseParseArea(area);
            card.Suit = (SuitEnum)(int.Parse(strCard.Substring(0, 1)));
            card.Face = (FaceEnum)(int.Parse(strCard.Substring(1, 2)));
            card.Side = (BackEnum)(int.Parse(strCard.Substring(3, 1)) == 0 ? -1 : 1);
            card.Area = ReverseParseArea(area);

            return card;

        }

        /// <summary>method:ParseArea
        /// tools for card moving operation
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static int ParseArea(string area)
        {
            int intResult;
            switch (area)
            {
                case "undeal": intResult = 1; break;
                case "unuse": intResult = 2; break;
                case "slot1": intResult = 11; break;
                case "slot2": intResult = 12; break;
                case "slot3": intResult = 13; break;
                case "slot4": intResult = 14; break;
                case "slot5": intResult = 15; break;
                case "slot6": intResult = 16; break;
                case "slot7": intResult = 17; break;
                case "set1": intResult = 21; break;
                case "set2": intResult = 22; break;
                case "set3": intResult = 23; break;
                case "set4": intResult = 24; break;
                default: intResult = 1; break;
            }
            return intResult;
        }

        /// <summary>method:ReverseParseArea
        ///  tools for card moving operation
        /// </summary>
        /// <param name="intArea"></param>
        /// <returns></returns>
        public static string ReverseParseArea(int intArea)
        {
            string strResult;
            switch (intArea)
            {
                case 1: strResult = "undeal"; break;
                case 2: strResult = "unuse"; break;
                case 11:strResult="slot1"; break;
                case 12:strResult="slot2"; break;
                case 13:strResult="slot3"; break;
                case 14:strResult="slot4"; break;
                case 15:strResult="slot5"; break;
                case 16:strResult="slot6"; break;
                case 17:strResult="slot7"; break;
                case 21:strResult="set1"; break;
                case 22:strResult="set2"; break;
                case 23:strResult="set3"; break;
                case 24:strResult="set4"; break;
                default: strResult = "undeal"; break;
            }
            return strResult;
        }

        /// <summary>method:ReverseParseAreaFromIndex
        /// tools for card moving operation
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ReverseParseAreaFromIndex(int index)
        {
            string strResult;
            switch (index)
            {
                case 0: strResult = "undeal"; break;
                case 1: strResult = "unuse"; break;
                case 2: strResult = "slot1"; break;
                case 3: strResult = "slot2"; break;
                case 4: strResult = "slot3"; break;
                case 5: strResult = "slot4"; break;
                case 6: strResult = "slot5"; break;
                case 7: strResult = "slot6"; break;
                case 8: strResult = "slot7"; break;
                case 9: strResult = "set1"; break;
                case 10: strResult = "set2"; break;
                case 11: strResult = "set3"; break;
                case 12: strResult = "set4"; break;
                default: strResult = "undeal"; break;
            }
            return strResult;
        }

        /// <summary>method:ParseAreaToIndex
        /// tools for card moving operation
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static int ParseAreaToIndex(string area)
        {
            int intResult;
            switch (area)
            {
                case "undeal": intResult = 0; break;
                case "unuse": intResult = 1; break;
                case "slot1": intResult = 2; break;
                case "slot2": intResult = 3; break;
                case "slot3": intResult = 4; break;
                case "slot4": intResult = 5; break;
                case "slot5": intResult = 6; break;
                case "slot6": intResult = 7; break;
                case "slot7": intResult = 8; break;
                case "set1": intResult = 9; break;
                case "set2": intResult = 10; break;
                case "set3": intResult = 11; break;
                case "set4": intResult = 12; break;
                default: intResult = 0; break;
            }
            return intResult;
        }

        /// <summary>method:ParseScreenShot
        /// tools for card moving operation
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int[][] ParseScreenShot(Form form)
        {
            int[][] intResult = new int[13][];
            for (int i = 0; i < panelNameArray.Length; i++)
            {
                ParseScreenShot(ref intResult, panelNameArray[i], ParseAreaToIndex(panelNameArray[i]), form);
            }
            return intResult;
        }

        /// <summary>method:ParseScreenShot
        /// tools for card moving operation
        /// </summary>
        /// <param name="intResult"></param>
        /// <param name="area"></param>
        /// <param name="index"></param>
        /// <param name="form"></param>
        public static void ParseScreenShot(ref int[][] intResult,string area, int index,Form form)
        {
            Panel areaPnl = form.Controls.Find(area, true)[0] as Panel;
            int[] intResultTemp = new int[areaPnl.Controls.Count];
            for (int i = 0; i < areaPnl.Controls.Count; i++)
            {
                Card card = areaPnl.Controls[i] as Card;
                intResultTemp[i]  = (int)card.Suit * 1000 + (int)card.Face * 10 + ((int)card.Side == 1 ? 1 : 0);
            }
            intResult[index] = intResultTemp;
        }

    }
}
