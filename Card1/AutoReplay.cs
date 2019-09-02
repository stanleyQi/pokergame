using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using static Card1.Setting;
using static Card1.MotionManager;
using static Card1.RecordMove;
using static Card1.GameManager;
using static Card1.MainForm;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace Card1
{
    class AutoReplay
    {
        public static bool isAutoReplaying = false;

        /// <summary>method:ReplayGame
        /// Performing operation when replay button is clicked. 
        /// </summary>
        /// <param name="cms"></param>
        /// <param name="mainform"></param>
        public static void ReplayGame(CardMoveStore cms,MainForm mainform)
        {
            // Get the initial distributing of cards
            List<List<Card>> initCardsWithinAreaList = GetInitForReplay(cms,mainform);

            // Get the Movement records
            List<MoveNode> mnList = cms.moves;

            // Setup the inital distrubuting
            SetupInitalForReplay(initCardsWithinAreaList, mainform);

            // Start replaying(loop all the movement records and simulate the movement by using user32.dll and timer)
            StartReplay(mnList, mainform);

        }

        /// <summary>method:SetupInitalForReplay
        /// Setup condition for perform replaying
        /// </summary>
        /// <param name="initCardsWithinAreaList"></param>
        /// <param name="mainform"></param>
        private static void SetupInitalForReplay(List<List<Card>> initCardsWithinAreaList, MainForm mainform)
        {
            List<List<Card>> undoList = initCardsWithinAreaList;
            undoScreenShot(mainform, undoList);

            foreach (Card cardInUndeal in mainform.undeal.Controls)
            {
                cardInUndeal.Click += cardInUndeal_Click;
            }
        }

        /// <summary>method:StartReplay
        /// Perform replay by using timer
        /// </summary>
        /// <param name="mnList"></param>
        /// <param name="mainform"></param>
        private static void StartReplay(List<MoveNode> mnList, MainForm mainform)
        {
            AutoReplay.isAutoReplaying = true;

            int movestep = 0;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = AUTOPLAY_INTERVAL;
            timer.Tick += (object obj, EventArgs e) => {
                if (movestep == mnList.Count) { timer.Enabled = false; AutoReplay.isAutoReplaying = false; return; }
                MoveNode currentMN = mnList[movestep];
                PerformMove(currentMN, mainform);

                movestep++;
            };

            timer.Enabled = true;
        }

        /// <summary>method:mouse_event
        /// register win32 for mouse moving simulation
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButton"></param>
        /// <param name="dwExtraInfo"></param>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButton, int dwExtraInfo);
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004
        }

        /// <summary>method:PerformMove
        /// Perform every step during replaying
        /// </summary>
        /// <param name="currentMN"></param>
        /// <param name="mainform"></param>
        private static void PerformMove(MoveNode currentMN, MainForm mainform)
        {
            int currentTopCard = currentMN.cards[currentMN.cards.Length-1];

            Card card = Tools.ParseToCardFromNumber(currentTopCard, currentMN.source);
            Panel sourcePnl = (Panel)mainform.Controls.Find(Tools.ReverseParseArea(currentMN.source), true)[0];
            Panel targetPnl = (Panel)mainform.Controls.Find(Tools.ReverseParseArea(currentMN.target), true)[0];

            if ("undeal".Equals(Tools.ReverseParseArea(currentMN.source)) && "unuse".Equals(Tools.ReverseParseArea(currentMN.target)))
            {
                int fromX = mainform.Location.X + mainform.panel1.Location.X + sourcePnl.Location.X + card.Width / 2;
                int fromY = mainform.Location.Y + mainform.panel1.Location.Y + sourcePnl.Location.Y + card.Height / 2;

                Cursor.Position = new System.Drawing.Point(fromX, fromY);
                mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
            else if ("unuse".Equals(Tools.ReverseParseArea(currentMN.source)) && "undeal".Equals(Tools.ReverseParseArea(currentMN.target)))
            {
                int fromX = mainform.Location.X + mainform.panel1.Location.X + targetPnl.Location.X + targetPnl.Width / 2;
                int fromY = mainform.Location.Y + mainform.panel1.Location.Y + targetPnl.Location.Y + targetPnl.Height / 2;

                Cursor.Position = new System.Drawing.Point(fromX, fromY);
                mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
            else {
                // Get the position of the card within the corresponding panel
                int cardPointYInSource;
                if ("unuse".Equals(Tools.ReverseParseArea(currentMN.source)))
                {
                    cardPointYInSource = 0;
                }
                else {
                    Card topCard = (Card)sourcePnl.Controls.Find(card.Name, true)[0];
                    cardPointYInSource = (sourcePnl.Controls.Count - 1 - sourcePnl.Controls.GetChildIndex(topCard)) * 30;
                }

                Point cardPointInSource = new Point(0, cardPointYInSource);

                //去掉form的标题栏
                //定位form在屏幕左上角
                int fromX = mainform.Location.X + mainform.panel1.Location.X + sourcePnl.Location.X + cardPointInSource.X + card.Width / 2;
                int fromY = mainform.Location.Y + mainform.panel1.Location.Y + sourcePnl.Location.Y + cardPointInSource.Y + 15;

                int toX = mainform.Location.X + mainform.panel1.Location.X + targetPnl.Location.X + 15;
                int toY = mainform.Location.Y + mainform.panel1.Location.Y + targetPnl.Location.Y + 15;

                Cursor.Position = new System.Drawing.Point(fromX, fromY);
                mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                Cursor.Position = new System.Drawing.Point(toX, toY);
                mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }
            lblCurrentStep.Text = currentMN.step.ToString();
        }

        /// <summary>method:GetCardPositionInSourceForReplay
        /// tools method for GetCardPositionInSourceForReplay
        /// </summary>
        /// <param name="currentTopCard"></param>
        /// <param name="source"></param>
        /// <param name="mainform"></param>
        /// <returns></returns>
        private static Point GetCardPositionInSourceForReplay(int currentTopCard, int source, MainForm mainform)
        {
            Point point = new Point();

            Card card = Tools.ParseToCardFromNumber(currentTopCard, source);
            Panel sourcePnl = (Panel)mainform.Controls.Find(Tools.ReverseParseArea(source),true)[0];

            // Get the position of the card within the corresponding panel
            point.X = 0;
            point.Y = sourcePnl.Controls.GetChildIndex(card) * 30; 
            return point;
        }

        /// <summary>method:GetInitForReplay
        /// tools method for  setting up replay condition
        /// </summary>
        /// <param name="cms"></param>
        /// <param name="mainform"></param>
        /// <returns></returns>
        public static List<List<Card>> GetInitForReplay(CardMoveStore cms, MainForm mainform)
        {
            List<List<Card>> result = new List<List<Card>>();
            int[][] initScreenshot = cms.game.init;
            for (int i = 0; i < initScreenshot.Length; i++)
            {
                List<Card> cardList = new List<Card>();
                if (initScreenshot[i].Length > 0)
                {
                    for (int j = 0; j < initScreenshot[i].Length; j++)
                    {
                        cardList.Add(Tools.ParseToCard(initScreenshot[i][j], i));
                    }
                }
                result.Add(cardList);
            }
            return result;
        }
    }
}
