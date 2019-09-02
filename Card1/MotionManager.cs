using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Card1.Setting;
using static Card1.GameManager;
using static Card1.RulesManager;
using static Card1.AutoReplay;
using static Card1.MainForm;

namespace Card1
{
    class MotionManager
    {
        static MainForm form;
        static Panel pnlTmp = new Panel();
        
        static CardMoveStore cms;

        /// <summary>method:SetupMotionCapability
        /// for Setting up Motion Capability to panels and cards in certain panel
        /// </summary>
        /// <param name="mainform"></param>
        public static void SetupMotionCapability(MainForm mainform)
        {
            form = mainform;
            foreach (Card cardInUndeal in form.undeal.Controls)
            {
                cardInUndeal.Click += cardInUndeal_Click;
            }
           
            form.slot1.AllowDrop = true;
            form.slot1.DragEnter += slot_DragEnter;
            form.slot1.DragDrop += slot_DragDrop;
            form.slot2.AllowDrop = true;
            form.slot2.DragEnter += slot_DragEnter;
            form.slot2.DragDrop += slot_DragDrop;
            form.slot3.AllowDrop = true;
            form.slot3.DragEnter += slot_DragEnter;
            form.slot3.DragDrop += slot_DragDrop;
            form.slot4.AllowDrop = true;
            form.slot4.DragEnter += slot_DragEnter;
            form.slot4.DragDrop += slot_DragDrop;
            form.slot5.AllowDrop = true;
            form.slot5.DragEnter += slot_DragEnter;
            form.slot5.DragDrop += slot_DragDrop;
            form.slot6.AllowDrop = true;
            form.slot6.DragEnter += slot_DragEnter;
            form.slot6.DragDrop += slot_DragDrop;
            form.slot7.AllowDrop = true;
            form.slot7.DragEnter += slot_DragEnter;
            form.slot7.DragDrop += slot_DragDrop;

            form.set1.AllowDrop = true;
            form.set1.DragEnter += set_DragEnter;
            form.set1.DragDrop += set_DragDrop;
            form.set2.AllowDrop = true;
            form.set2.DragEnter += set_DragEnter;
            form.set2.DragDrop += set_DragDrop;
            form.set3.AllowDrop = true;
            form.set3.DragEnter += set_DragEnter;
            form.set3.DragDrop += set_DragDrop;
            form.set4.AllowDrop = true;
            form.set4.DragEnter += set_DragEnter;
            form.set4.DragDrop += set_DragDrop;

            form.undeal.Click += resetUndealFromUnuse_Click;
        }

        /// <summary>method:resetUndealFromUnuse_Click
        /// event handler for clicking reset icon in undeal panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void resetUndealFromUnuse_Click(object sender, EventArgs e)
        {
            if (form.undeal.Controls.Count == 0)
            {

                if (pnlTmp.Parent != null)
                {
                    for (int i = (pnlTmp.Controls.Count - 1); i >= 0; i--)
                    {
                        Card card = pnlTmp.Controls[i] as Card;
                        card.Location = card.Position;
                        card.Parent = pnlTmp.Parent;
                        card.BringToFront();
                    }
                    pnlTmp.Parent = null;
                }

                //MessageBox.Show("refreshed.");
                Card[] cardsArray = new Card[form.unuse.Controls.Count];
                int count = form.unuse.Controls.Count-1;
                for (int i = count; i >= 0; i--)
                {
                    Card card = form.unuse.Controls[i] as Card;
                    card.Area = "undeal";
                    card.Name = card.Name.Substring(0, card.Name.Length - 1) + "0";
                    //card.Text = card.Name + "-" + card.Area;
                    card.Parent = form.undeal;
                    card.Side = BackEnum.Back;
                    card.Position = new Point(0, 0);
                    card.CardDraw("undeal", 0, 0, -1);

                    card.Click += cardInUndeal_Click;
                    undealCards.Push(card);
                    cardsArray[count-i] = card;
                }
                // record move
                if (!AutoReplay.isAutoReplaying)
                {
                    cms = RecordMove.RecordOneMove(cardsArray, form.unuse, form.undeal, form);
                    lblCurrentStep.Text = cms.moves.Count.ToString();
                }
            }
        }

        /// <summary>method:set_DragDrop
        /// event handeling for drag and drop to sets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void set_DragDrop(object sender, DragEventArgs e)
        {
            pnlTmp = ((Panel)e.Data.GetData(typeof(Panel)));
            if (pnlTmp.Controls.Count == 0) return;
            pnlTmp.Name = "pnlTmp";
            Card topCard = ((Card)pnlTmp.Controls[pnlTmp.Controls.Count - 1]);
            string sourcePnlName = topCard.Area;
            Panel targetPnl = (Panel)sender;

            if (topCard.Area.ToString() == targetPnl.Name.ToString())
            {
                if (pnlTmp.Parent != null)
                {
                    for (int i = (pnlTmp.Controls.Count - 1); i >= 0; i--)
                    {
                        Card card = pnlTmp.Controls[i] as Card;
                        card.Location = card.Position;
                        card.Parent = pnlTmp.Parent;
                        card.BringToFront();
                    }
                    pnlTmp.Parent = null;
                }
                return;
            };

            //Rules:It is only allowed to put on "A" when the current set is empty
            if (!IsCanMovedToSet(new Card[1] { topCard }, targetPnl, "SetEmpty")) return;

            //Rules: It is only allowed to put on the card which is with the same suit and with 1 bigger digit than the one existing in the current set when the set is setting with certain digit 
            if (!IsCanMovedToSet(pnlTmp.Controls.Cast<Control>().ToArray(), targetPnl, "SetNotEmpty")) return;

            int movedCardsAmount = pnlTmp.Controls.Count;
            Card[] cardsArray = new Card[movedCardsAmount];
            for (int i = 0; i < movedCardsAmount; i++)
            {
                cardsArray[i] = (Card)pnlTmp.Controls[i];
            }
            int count = targetPnl.Controls.Count;
            for (int i = cardsArray.Length - 1; i >= 0; i--)
            {
                cardsArray[i].Location = new Point(0, count * 8);
                cardsArray[i].Position = cardsArray[i].Location;//////////////////
                cardsArray[i].Click -= cardInUndeal_Click;
                cardsArray[i].Parent = targetPnl;
                cardsArray[i].Area = targetPnl.Name;
                //cardsArray[i].Text = cardsArray[i].Name + "-" + cardsArray[i].Area;
                cardsArray[i].BringToFront();
                count++;
            }

            pnlTmp.Size = new Size(0, 0);
            pnlTmp.Parent = null;
            pnlTmp.SendToBack();

            //source movement
            Panel sourcePnl = (Panel)form.Controls.Find(sourcePnlName, true)[0];

            if (sourcePnl.Controls.Count >= 1)
            {
                Card lastCardInPanelAfterMove = (Card)(sourcePnl.Controls[0]);
                lastCardInPanelAfterMove.CardDraw(1);
                lastCardInPanelAfterMove.Name = lastCardInPanelAfterMove.Name.Substring(0, lastCardInPanelAfterMove.Name.Length - 1) + "1";
                //lastCardInPanelAfterMove.Text = lastCardInPanelAfterMove.Name + "-"+lastCardInPanelAfterMove.Parent.Name; 
            }

            // record move
            if (!AutoReplay.isAutoReplaying)
            {
                cms = RecordMove.RecordOneMove(cardsArray, sourcePnl, targetPnl, form);
                lblCurrentStep.Text = cms.moves.Count.ToString();
            }

            //Identification completed successfully:All the cards in each one of the 4 sets are startted from A and then listed orderly from 6 to K and being with the same suit. 
            if (IsCompletedSuccessfully(form, "Sets"))
            {
                if (MessageBox.Show("You have successfully completed the game") == DialogResult.OK)
                {
                    Button btnReplay = form.Controls.Find("btnReplay", true)[0] as Button;
                    form.customedTimer1.Stop();
                    //btnReplay.Enabled = true;
                    return;
                }
            }
        }

        /// <summary>method:set_DragEnter
        /// event handeling for entering to sets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void set_DragEnter(object sender, DragEventArgs e)
        {
            ((Panel)sender).AllowDrop = true;
            pnlTmp = ((Panel)e.Data.GetData(typeof(Panel)));

            Panel targetPanel = (Panel)sender;
            int movedCardsAmount = pnlTmp.Controls.Count;
            Card[] cardsArray = new Card[movedCardsAmount];
            for (int i = 0; i < movedCardsAmount; i++)
            {
                cardsArray[i] = (Card)pnlTmp.Controls[i];
            }
           
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>method:slot_DragDrop
        /// event handeling for drag and drop to slot(table)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void slot_DragDrop(object sender, DragEventArgs e)
        {
            if (pnlTmp.Controls.Count == 0) return;

            pnlTmp = ((Panel)e.Data.GetData(typeof(Panel)));
            pnlTmp.Name = "pnlTmp";
            Card topCard = ((Card)pnlTmp.Controls[pnlTmp.Controls.Count-1]);//-1
            Panel targetPnl = (Panel)sender;
            string sourcePnlName = topCard.Area;

            if (topCard.Area.ToString() == targetPnl.Name.ToString())
            {
                if (pnlTmp.Parent != null)
                {
                    for (int i = (pnlTmp.Controls.Count - 1); i >= 0; i--)
                    {
                        Card card = pnlTmp.Controls[i] as Card;
                        card.Location = card.Position;
                        card.Parent = pnlTmp.Parent;
                        card.BringToFront();
                    }
                    pnlTmp.Parent = null;
                }
                return;
            };

            //rules: When the slot is empty, then it is only allowed to put in the cards which the ACE as the top card. 
            if (!IsCanMovedTo(topCard, targetPnl, "SlotEmpty")) return;

            //rules:When the card's number is continuous and the colours are alternated, then they can be dropped.
            if (!IsCanMovedTo(topCard, targetPnl, "ContinuousAndColoursAlternated")) return;

            int movedCardsAmount = pnlTmp.Controls.Count;
            Card[] cardsArray = new Card[movedCardsAmount];
            for (int i = 0; i < movedCardsAmount; i++)
            {
                cardsArray[i] = (Card)pnlTmp.Controls[i];
            }
            int count = targetPnl.Controls.Count;
            for (int i = cardsArray.Length-1; i >=0; i--)
            {
                cardsArray[i].Location = new Point(0, (count) * 30);
                cardsArray[i].Position = cardsArray[i].Location;
                cardsArray[i].Click -= cardInUndeal_Click;
                cardsArray[i].Parent = targetPnl;
                cardsArray[i].Area = targetPnl.Name;
                //cardsArray[i].Text = cardsArray[i].Name + "-" + cardsArray[i].Area;
                cardsArray[i].BringToFront();
                count++;
            }

            pnlTmp.Size = new Size(0,0);
            pnlTmp.Parent = null;
            pnlTmp.SendToBack();

            //source movement
            Panel sourcePnl = (Panel)form.Controls.Find(sourcePnlName, true)[0];

            if (sourcePnl.Controls.Count >= 1)
            {
                Card lastCardInPanelAfterMove =(Card)(sourcePnl.Controls[0]);
                lastCardInPanelAfterMove.CardDraw(1);
                lastCardInPanelAfterMove.Name = lastCardInPanelAfterMove.Name.Substring(0, lastCardInPanelAfterMove.Name.Length - 1) + "1";
                //lastCardInPanelAfterMove.Text = lastCardInPanelAfterMove.Name + "-"+lastCardInPanelAfterMove.Parent.Name;
            }

            // record move
            if (!AutoReplay.isAutoReplaying)
            {
                cms = RecordMove.RecordOneMove(cardsArray, sourcePnl, targetPnl, form);
                lblCurrentStep.Text = cms.moves.Count.ToString();
            }

            //Discriminate failure:
            if (IsFailure(form))
            {
                if (MessageBox.Show("You have been unable to successfully complete the game.") == DialogResult.OK) return;
            }
        }

        /// <summary>method:slot_DragEnter
        /// event handeling for entering to slots(table)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void slot_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>method:card_MouseDown
        /// event handler for card mousedown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_MouseDown(object sender,MouseEventArgs e)
        {
            Card currentCard = ((Card)sender);
            Panel sourcePanel = (Panel)form.Controls.Find(currentCard.Area, true)[0];

            if ("undeal".Equals(currentCard.Area)) return;

            if (pnlTmp.Parent != null)
            {
                for (int i = (pnlTmp.Controls.Count - 1); i >= 0; i--)
                {
                    Card card = pnlTmp.Controls[i] as Card;
                    card.Location = card.Position;
                    card.Parent = pnlTmp.Parent;
                    card.BringToFront();
                }
                pnlTmp.Parent = null;
            }

            //rules:When the card's number is continuous and the colours are alternated, then they can be moved.
            if (!IsCanMoved(currentCard, sourcePanel, "ContinuousAndColoursAlternated")) return;

            pnlTmp.Size = new Size(SLOT_WIDTH, SLOT_HEIGHT);
            pnlTmp.Location = new Point(currentCard.Location.X, currentCard.Location.Y);
            //pnlTmp.BackColor = Color.FromArgb(20, Color.White);
            pnlTmp.BackColor = Color.Transparent;

            pnlTmp.Parent = sourcePanel;

            if (pnlTmp.Controls.Count == 0)
            {
                int moveCardsAmount = sourcePanel.Controls.GetChildIndex(currentCard) + 1;//+1
                Card[] cardsArray = new Card[moveCardsAmount];
                for (int i = 0; i < moveCardsAmount; i++)
                {
                    cardsArray[i] = (Card)sourcePanel.Controls[i];
                }
                foreach (Card item in cardsArray)
                {
                    item.Location = new Point(item.Location.X, item.Location.Y - currentCard.Location.Y);
                    pnlTmp.Controls.Add(item);
                }
            }

            pnlTmp.BringToFront();
            pnlTmp.DoDragDrop(pnlTmp, DragDropEffects.Move);

        }

        /// <summary>method:cardInUndeal_Click
        /// event handler for card click in undeal panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void cardInUndeal_Click(object sender,EventArgs e)
        {
            if (pnlTmp.Parent != null)
            {
                for (int i = (pnlTmp.Controls.Count - 1); i >= 0; i--)
                {
                    Card card = pnlTmp.Controls[i] as Card;
                    card.Location = card.Position;
                    card.Parent = pnlTmp.Parent;
                    card.BringToFront();
                }
                pnlTmp.Parent = null;
            }

            Card self = sender as Card;
            self.CardDraw("unuse", form.unuse.Controls.Count*2, form.unuse.Controls.Count*2, 1);
            self.Parent = form.unuse;
            self.BringToFront();

            // record move
            Card[] cardsArray = new Card[1];
            cardsArray[0] = self;
            cardsArray[0].Position = cardsArray[0].Location;
            cardsArray[0].Click -= cardInUndeal_Click;
            cardsArray[0].Area = "unuse";
            cardsArray[0].Name = cardsArray[0].Name.Substring(0, cardsArray[0].Name.Length - 1) + "1";
            //cardsArray[0].Text = cardsArray[0].Name + "-"+cardsArray[0].Parent.Name;

            if (!AutoReplay.isAutoReplaying)
            {
                cms = RecordMove.RecordOneMove(cardsArray, form.undeal, form.unuse, form);
                lblCurrentStep.Text = cms.moves.Count.ToString();
            }
        }

    }
}
