using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Card1.Setting;
using static Card1.MotionManager;
using static Card1.RecordMove;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Card1
{
    class GameManager
    {
        public static List<Card> undealCardsTemp;
        public static Stack<Card> undealCards;
        private static CardMoveStore cmsForReload;

        public static Panel undealPnl, unusePnl, slot1Pnl, slot2Pnl, slot3Pnl, slot4Pnl, slot5Pnl, slot6Pnl, slot7Pnl, set1Pnl, set2Pnl, set3Pnl, set4Pnl;
        public static Label lblCurrentStep;

        /// <summary>method:startGame
        /// creat a deck of cards(36),shuffle the cards,display the cards with back side on the "undealArea" of the table
        /// </summary>
        /// <param name="form"></param>
        /// <param name="isReload"></param>
        public static void startGame(MainForm form,bool isReload)
        {
           undealPnl = form.Controls.Find("undeal", true)[0] as Panel;
            unusePnl = form.Controls.Find("unuse", true)[0] as Panel;
           slot1Pnl = form.Controls.Find("slot1", true)[0] as Panel;
           slot2Pnl = form.Controls.Find("slot2", true)[0] as Panel;
           slot3Pnl = form.Controls.Find("slot3", true)[0] as Panel;
           slot4Pnl = form.Controls.Find("slot4", true)[0] as Panel;
           slot5Pnl = form.Controls.Find("slot5", true)[0] as Panel;
           slot6Pnl = form.Controls.Find("slot6", true)[0] as Panel;
           slot7Pnl = form.Controls.Find("slot7", true)[0] as Panel;
           set1Pnl = form.Controls.Find("set1", true)[0] as Panel;
           set2Pnl = form.Controls.Find("set2", true)[0] as Panel;
           set3Pnl = form.Controls.Find("set3", true)[0] as Panel;
           set4Pnl = form.Controls.Find("set4", true)[0] as Panel;

           lblCurrentStep = form.Controls.Find("lblCurrentStep", true)[0] as Label;

            /////////////////////////////////// if it is a new game////////////////////////////////////////
            if (isReload == false)
            {
                GenerateAdeckOfCards(out undealCardsTemp);
                Shuffle(ref undealCardsTemp);
                FirstDeal(form, ref undealCardsTemp, out undealCards);

                SetupMotionCapability(form);
                SetupRecordCapability(form); // record move
            }
            else
            {
                /////////////////////////////////// if it is a reload game////////////////////////////////////////
                // reload method should initailize the cms and dispatch cards on the table
                ReloadGame(form);
                SetupMotionCapability(form);
            }
        }

        /// <summary>method:ReloadGame
        ///  About getting local file ref: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.application.startuppath?view=netframework-4.8
        /// </summary>
        /// <param name="mainform"></param>
        public static void ReloadGame(MainForm mainform)
        {
            // read the stored file into list
            string stringJson;
            string jsonfile = System.Windows.Forms.Application.StartupPath + @"../../../" + @"/test/" + STORAGE_FILE_NAME;
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    stringJson = JToken.ReadFrom(reader).ToString().Replace("\r\n", "");
                }
            }

            // set the cms
            cmsForReload = JsonConvert.DeserializeObject<CardMoveStore>(stringJson);

            if ("1".Equals(cmsForReload.game.init[3][1].ToString().Substring(3, 1)))
            {
                foreach (MoveNode move in cmsForReload.moves)
                {
                    for (int i = 0; i < 13; i++)
                    {
                        Array.Reverse(move.screenshot[i]);
                    }

                }
                foreach (int[] area in cmsForReload.game.init)
                {
                    Array.Reverse(area);
                }
            }

            RecordMove.cms = cmsForReload;

            // display cards onto the table
            // call game manager to undo the game(pass the "undoSeletedMoveNode" to mainform for perform the undo operation)
            List<List<Card>> undoCardsWithinAreaList = new List<List<Card>>();

            int[][] undoSeletedScreenshot = new int[13][];
            if (RecordMove.cms.moves.Count == 0)
            {
                undoSeletedScreenshot = RecordMove.cms.game.init;
            }
            else
            {
                undoSeletedScreenshot = ((RecordMove.cms.moves[RecordMove.cms.moves.Count - 1]) as MoveNode).screenshot;
            }

            for (int i = 0; i < undoSeletedScreenshot.Length; i++)
            {
                List<Card> cardList = new List<Card>();
                if (undoSeletedScreenshot[i].Length > 0)
                {
                    for (int j = 0; j < undoSeletedScreenshot[i].Length; j++)
                    {
                        cardList.Add(Tools.ParseToCard(undoSeletedScreenshot[i][j], i));
                    }
                }
                undoCardsWithinAreaList.Add(cardList);

            }
            undoGame(mainform, undoCardsWithinAreaList);
        }

        /// <summary>method:undoGame
        /// start performing undo
        /// </summary>
        /// <param name="form"></param>
        /// <param name="undoCardsWithinAreaList"></param>
        public static void undoGame(MainForm form, List<List<Card>> undoCardsWithinAreaList)
        {
            // setup form according to  the undo data passed in
            List<List<Card>> undoList = undoCardsWithinAreaList; 
            undoScreenShot(form,undoList);

            foreach (Card cardInUndeal in form.undeal.Controls)
            {
                cardInUndeal.Click += cardInUndeal_Click;
            }
        }

        /// <summary>method:undoScreenShot
        /// redisplay the cards on tha table according to the infomation stored in the steplist
        /// </summary>
        /// <param name="form"></param>
        /// <param name="undoList"></param>
        public static void undoScreenShot(MainForm form,List<List<Card>> undoList)
        {
            cleanTable(form);

            for (int i = undoList.Count-1; i >=0 ; i--)
            {
                for (int j = undoList[i].Count-1; j >=0 ; j--)
                {
                    Card card = undoList[i][j];
                    int y;
                    if ("undeal".Equals(card.Area))
                    {
                        y = undoList[i].Count - 1 - j;
                    }
                    else if ("set".Equals(card.Area.Substring(0, 3)))
                    {
                        y = 8 * (undoList[i].Count - 1 - j);
                    }
                    else
                    {
                        y = 30 * (undoList[i].Count - 1 - j);
                    }

                    card.CardDraw(card.Area, 0, y, (int)card.Side);
                    AddComponentToContainer(form.Controls.Find(card.Area, true)[0] as Panel, card);
                }
               
            }
        }

        /// <summary>method:GenerateAdeckOfCards
        /// tools: for genarate undeal cards
        /// </summary>
        /// <param name="undealCardsTempParam"></param>
        private static void GenerateAdeckOfCards( out List<Card> undealCardsTempParam)
        {
            List<Card> innerList = new List<Card>();
            for (int i = 14; i >(14-(AMOUNT_CARDS/4)); i--)
            {
                for (int j = 1; j < 5; j++)
                {
                    innerList.Add(new Card(i, j)); 
                }
            }
            undealCardsTempParam = innerList;
        }

        /// <summary>method:Shuffle
        /// generate a deck of new cards randomly
        /// </summary>
        /// <param name="undealCardsTempParam"></param>
        private static void Shuffle(ref List<Card> undealCardsTempParam)
        {
            // resort the cards randomly
            Random random = new Random();
            List<Card> newList = new List<Card>();
            foreach (Card card in undealCardsTempParam)
            {
                newList.Insert(random.Next(newList.Count + 1), card);
            }
            undealCardsTempParam = newList;
        }

        /// <summary>method:FirstDeal
        /// Allocating playing cards
        /// </summary>
        /// <param name="form"></param>
        /// <param name="undealCardsTempParam"></param>
        /// <param name="undealCardsParam"></param>
        private static void FirstDeal(MainForm form,ref List<Card> undealCardsTempParam, out Stack<Card>  undealCardsParam)
        {
            Stack<Card> innerStack = new Stack<Card>();
            foreach (var card in undealCardsTempParam)
            {
                card.Click -= cardInUndeal_Click;
                innerStack.Push(card);
            }

            // deal out 28 cards to slot1~slot7 as initial display 
            for (int i = 0; i < 28; i++)
            {
                Card card = innerStack.Pop() as Card;
                switch (i) {
                    case 0:card.CardDraw("slot1", 0, 0, 1); AddComponentToContainer(slot1Pnl, card); break;
                    case 1:card.CardDraw("slot2", 0, 0, -1); AddComponentToContainer(slot2Pnl, card); break;
                    case 7:card.CardDraw("slot2", 0, 30, 1); AddComponentToContainer(slot2Pnl, card); break;
                    case 2:card.CardDraw("slot3", 0, 0, -1); AddComponentToContainer(slot3Pnl, card); break;
                    case 8:card.CardDraw("slot3", 0, 30, -1); AddComponentToContainer(slot3Pnl, card); break;
                    case 13:card.CardDraw("slot3", 0, 60, 1); AddComponentToContainer(slot3Pnl, card); break;
                    case 3: card.CardDraw("slot4", 0, 0, -1); AddComponentToContainer(slot4Pnl, card); break;
                    case 9: card.CardDraw("slot4", 0, 30, -1); AddComponentToContainer(slot4Pnl, card); break;
                    case 14:card.CardDraw("slot4", 0, 60, -1); AddComponentToContainer(slot4Pnl, card); break;
                    case 18:card.CardDraw("slot4", 0, 90, 1); AddComponentToContainer(slot4Pnl, card); break;
                    case 4: card.CardDraw("slot5", 0, 0, -1); AddComponentToContainer(slot5Pnl, card); break;
                    case 10:card.CardDraw("slot5", 0, 30, -1); AddComponentToContainer(slot5Pnl, card); break;
                    case 15:card.CardDraw("slot5", 0, 60, -1); AddComponentToContainer(slot5Pnl, card); break;
                    case 19:card.CardDraw("slot5", 0, 90, -1); AddComponentToContainer(slot5Pnl, card); break;
                    case 22:card.CardDraw("slot5", 0, 120, 1); AddComponentToContainer(slot5Pnl, card); break;
                    case 5: card.CardDraw("slot6", 0, 0, -1); AddComponentToContainer(slot6Pnl, card); break;
                    case 11:card.CardDraw("slot6", 0, 30, -1); AddComponentToContainer(slot6Pnl, card); break;
                    case 16:card.CardDraw("slot6", 0, 60, -1); AddComponentToContainer(slot6Pnl, card); break;
                    case 20:card.CardDraw("slot6", 0, 90, -1); AddComponentToContainer(slot6Pnl, card); break;
                    case 23:card.CardDraw("slot6", 0, 120, -1); AddComponentToContainer(slot6Pnl, card); break;
                    case 25:card.CardDraw("slot6", 0, 150, 1); AddComponentToContainer(slot6Pnl, card); break;
                    case 6: card.CardDraw("slot7", 0, 0, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 12:card.CardDraw("slot7", 0, 30, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 17:card.CardDraw("slot7", 0, 60, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 21:card.CardDraw("slot7", 0, 90, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 24:card.CardDraw("slot7", 0, 120, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 26:card.CardDraw("slot7", 0, 150, -1); AddComponentToContainer(slot7Pnl, card); break;
                    case 27:card.CardDraw("slot7", 0, 180, 1); AddComponentToContainer(slot7Pnl, card); break;
                }
            }

            int iForInnerStack = 0; 
            foreach (var card in innerStack)
            {
                card.CardDraw("undeal", iForInnerStack, iForInnerStack, -1);
                AddComponentToContainer(undealPnl, card);
                iForInnerStack+=2;
            }
            
            undealCardsParam = innerStack;
        }

        /// <summary>method:AddComponentToContainer
        /// tools:add a control into a container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="component"></param>
        public static void AddComponentToContainer(Control container, Control component)
        {
            Card card = (Card)component;
            card.Name = ((int)card.Suit * 1000 + (int)card.Face * 10 + ((int)card.Side == 1 ? 1 : 0)).ToString();
            container.Controls.Add(card);
            //card.Text = card.Name + "-" + card.Parent.Name;
            component.BringToFront();
        }

        /// <summary>method:cleanTable
        /// clean all the cards on the table for new game or undo operation
        /// </summary>
        /// <param name="form"></param>
        public static void cleanTable(MainForm form)
        {
            undealPnl.Controls.Clear(); unusePnl.Controls.Clear(); slot1Pnl.Controls.Clear(); slot2Pnl.Controls.Clear(); slot3Pnl.Controls.Clear(); slot4Pnl.Controls.Clear(); slot5Pnl.Controls.Clear(); slot6Pnl.Controls.Clear(); slot7Pnl.Controls.Clear(); set1Pnl.Controls.Clear(); set2Pnl.Controls.Clear(); set3Pnl.Controls.Clear(); set4Pnl.Controls.Clear();
        }
    }
}
