using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace Card1
{
    class RulesManager
    {
        /// <summary>method:IsCardsDescending
        /// for discriminating IsCardsDescending
        /// </summary>
        /// <param name="cardsArrayParam"></param>
        /// <returns></returns>
        public static bool IsCardsDescending(Card[] cardsArrayParam)
        {
            int len = cardsArrayParam.Length;
                for (int i = 0; i < len; i++)
                {
                    if ((int)cardsArrayParam[i].Face <(int)cardsArrayParam[i+1].Face)
                    {
                        return false;
                    }
                }
            return true;
        }

        /// <summary>method:IsCardsAceStarted
        /// for discriminating IsCardsAceStarted
        /// </summary>
        /// <param name="cardsArrayParam"></param>
        /// <returns></returns>
        public static bool IsCardsAceStarted(Card[] cardsArrayParam)
        {
            if (cardsArrayParam[0].Face == FaceEnum.Ace)
            {
                return true;
            }
            return false;
        }

        /// <summary>method:IsCardsSpecifiedStarted
        /// for discriminating IsCardsSpecifiedStarted
        /// </summary>
        /// <param name="startFace"></param>
        /// <param name="cardsArrayParam"></param>
        /// <returns></returns>
        public static bool IsCardsSpecifiedStarted(int startFace,Card[] cardsArrayParam)
        {
            if ((int)cardsArrayParam[0].Face == startFace)
            {
                return true;
            }
            return false;
        }

        /// <summary>method:IsCanMoved
        /// for discriminating IsCanMoved
        /// </summary>
        /// <param name="card"></param>
        /// <param name="panel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCanMoved(Card card, Panel panel, string type)
        {
            bool result = true;

            if ("ContinuousAndColoursAlternated".Equals(type))
            {
                int moveCardsAmount = panel.Controls.GetChildIndex(card) + 1;//+1
                if (moveCardsAmount>1)
                {
                    for (int i = moveCardsAmount - 1; i >= 0; i--)
                    {
                        Card currentCard = panel.Controls[i] as Card;
                        Card nextCard = panel.Controls[i - 1] as Card;
                        if (((int)currentCard.Face != (int)nextCard.Face + 1)
                          || ((int)currentCard.Suit == (int)nextCard.Suit) || ((int)currentCard.Suit%2 == (int)nextCard.Suit%2)) result = false; break;
                    }
                }
            }
            return result;
        }

        /// <summary>method:IsCanMovedTo
        /// for discriminating IsCanMovedTo certain panel
        /// </summary>
        /// <param name="card"></param>
        /// <param name="toPanel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCanMovedTo(Card card, Panel toPanel, string type)
        {
            bool result = true;

            if ("ContinuousAndColoursAlternated".Equals(type))
            {
                if (toPanel.Controls.Count > 0)
                {
                    Card lastCard = toPanel.Controls[0] as Card;
                    if ((int)lastCard.Face == 6 && (int)card.Face == 14)
                    {
                        if (((int)lastCard.Suit == (int)card.Suit) || ((int)lastCard.Suit % 2 == (int)card.Suit % 2))
                        {
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                    if ((int)lastCard.Face == 14) return false;
                    if (((int)lastCard.Face != (int)card.Face + 1)
                          || ((int)lastCard.Suit == (int)card.Suit) || ((int)lastCard.Suit % 2 == (int)card.Suit % 2)) result = false;
                }
            }
            else if ("SlotEmpty".Equals(type))
            {
                if ((toPanel.Controls.Count == 0) && ((int)card.Face != 13)) result = false;
            }
            return result;
        }

        /// <summary>method:IsCanMovedToSet
        /// for discriminating IsCanMovedToSet
        /// </summary>
        /// <param name="cardArray"></param>
        /// <param name="toPanel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCanMovedToSet(Control[] cardArray, Panel toPanel, string type)
        {
            bool result = true;

            if ("SetEmpty".Equals(type))
            {
                if ((toPanel.Controls.Count == 0) && ((int)(cardArray[0] as Card).Face != 14)) result = false;
            }
            else if ("SetNotEmpty".Equals(type) && toPanel.Controls.Count == 1)//update for TopA
            {
                if ((int)(cardArray[0] as Card).Face != 6
                   || (toPanel.Controls[0] as Card).Suit != (cardArray[0] as Card).Suit) result = false;
            }
            else if ("SetNotEmpty".Equals(type) && toPanel.Controls.Count > 1)//update for TopA
            {
                if ((int)(toPanel.Controls[0] as Card).Face != (int)(cardArray[0] as Card).Face - 1
                    || (toPanel.Controls[0] as Card).Suit != (cardArray[0] as Card).Suit) result = false;
            }
            return result;
        }

        //Identification completed successfully:All the cards in each one of the 4 sets are listed orderly from 6 to A and being with the same suit. 
        /// <summary>method:IsCompletedSuccessfully
        /// for discriminating IsCompletedSuccessfully
        /// </summary>
        /// <param name="form"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCompletedSuccessfully(MainForm form, string type)
        {
            bool result = false;

            if ("Sets".Equals(type))
            {
                Panel setPnl1 = form.Controls.Find("set1",true)[0] as Panel;
                Panel setPnl2 = form.Controls.Find("set2",true)[0] as Panel;
                Panel setPnl3 = form.Controls.Find("set3",true)[0] as Panel;
                Panel setPnl4 = form.Controls.Find("set4",true)[0] as Panel;

                // determine if all cards in a set are sorted from 6 to A and being with the same suit.
                if (IsSortedFrom6ToAWithSameSuit(setPnl1) // update for topA
                    && IsSortedFrom6ToAWithSameSuit(setPnl2)
                    && IsSortedFrom6ToAWithSameSuit(setPnl3)
                    && IsSortedFrom6ToAWithSameSuit(setPnl4)) result = true;
            }
            return result;
        }

        /// <summary>method:IsFailure
        /// for discriminating failure
        /// 1.All the cards remaining in the unuse area can not be moved to anywhere anymore.
        /// 2.All the cards displaying face in the slot can not be moved to other slots anymore.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsFailure(MainForm form)
        {


            Panel unusePnl = form.Controls.Find("unuse", true)[0] as Panel;
            Panel undealPnl = form.Controls.Find("undeal", true)[0] as Panel;

            Panel slot1Pnl = form.Controls.Find("slot1", true)[0] as Panel;
            Panel slot2Pnl = form.Controls.Find("slot2", true)[0] as Panel;
            Panel slot3Pnl = form.Controls.Find("slot3", true)[0] as Panel;
            Panel slot4Pnl = form.Controls.Find("slot4", true)[0] as Panel;
            Panel slot5Pnl = form.Controls.Find("slot5", true)[0] as Panel;
            Panel slot6Pnl = form.Controls.Find("slot6", true)[0] as Panel;
            Panel slot7Pnl = form.Controls.Find("slot7", true)[0] as Panel;

            Card[] cards1 = GetSpecialCardsInASlot(slot1Pnl);
            Card[] cards2 = GetSpecialCardsInASlot(slot2Pnl);
            Card[] cards3 = GetSpecialCardsInASlot(slot3Pnl);
            Card[] cards4 = GetSpecialCardsInASlot(slot4Pnl);
            Card[] cards5 = GetSpecialCardsInASlot(slot5Pnl);
            Card[] cards6 = GetSpecialCardsInASlot(slot6Pnl);
            Card[] cards7 = GetSpecialCardsInASlot(slot7Pnl);

            List<Card> cardListUnuseAndUndeal = new List<Card>();
            for (int i = 0; i < unusePnl.Controls.Count; i++)
            {
                cardListUnuseAndUndeal.Add(unusePnl.Controls[i] as Card);
            }
            for (int i = 0; i < undealPnl.Controls.Count; i++)
            {
                cardListUnuseAndUndeal.Add(undealPnl.Controls[i] as Card);
            }

            Card[][] cardsArr = new Card[7][];
            cardsArr[0] = cards1;
            cardsArr[1] = cards2;
            cardsArr[2] = cards3;
            cardsArr[3] = cards4;
            cardsArr[4] = cards5;
            cardsArr[5] = cards6;
            cardsArr[6] = cards7;

            if (!IsMovableFromUnuseToSlots(cardListUnuseAndUndeal, cardsArr) && !IsMovableAmongSlots(cardsArr)) return true;

            return false;
        }

        /// <summary>method:IsMovableFromUnuseToSlots
        /// Determine if you can continue moving from unuse area to any slots in the current situation 
        /// </summary>
        /// <returns></returns>
        public static bool IsMovableFromUnuseToSlots(List<Card> cardList,Card[][] cardsArr)
        {
            bool result = false;

            List<Card[]> cardsList = cardsArr.ToList();
            for (int i = 0; i < cardList.Count; i++)
            {
                
                Card card = cardList[i];
                List<Card[]> cardsList1 = cardsList.FindAll(
                        (otherCards) => {
                            if (otherCards[1] == null || card == null) { return false; }
                            return ((int)(card.Face) + 1 == (int)(otherCards[1].Face)) && ((int)(card.Suit) != (int)(otherCards[1].Suit));
                        }
                    );
                if (cardsList1.Count > 0) { result = true; break; }
            }

            return result;
        }

        /// <summary>method:IsMovebleAmongSlots
        /// Determine if you can continue moving in the current situation
        /// </summary>
        /// <returns></returns>
        public static bool IsMovableAmongSlots(Card[][] cardsArr)
        {
            bool result = false;

            List<Card[]> cardsList = cardsArr.ToList();
            for (int i = 0; i < cardsList.Count; i++)
            {
                Card[] cards = cardsList[i];
                
                List<Card[]> cardsList1 = cardsList.FindAll(
                        (otherCards) => {
                            if (otherCards[1] == null || cards[0] == null) { return false; }
                            return ((int)(cards[0].Face) + 1 == (int)(otherCards[1].Face)) && ((int)(cards[0].Suit) != (int)(otherCards[1].Suit));
                        }
                    );
                if (cardsList1.Count > 0) { result = true; break; }
            }

            return result;
        }
        
        /// <summary>method:GetSpecialCardsInASlot
        /// get the first card which is displaying face in a solt and the last card of a slot
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public static Card[] GetSpecialCardsInASlot(Panel panel)
        {
            Card[] cards = new Card[2];
            Card topCard = null;
            Card lastCard = null;
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                if ((int)((panel.Controls[i] as Card).Side) == 1)
                {
                    topCard = panel.Controls[i] as Card;
                    break;
                }
            }
            lastCard = (panel.Controls.Count != 0) ? panel.Controls[0] as Card : null;

            cards[0] = topCard;
            cards[1] = lastCard;
            return cards;
        }

        /// <summary>method:IsSortedFrom6ToAWithSameSuit
        /// for discriminating IsSortedFrom6ToAWithSameSuit in the sets
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public static bool IsSortedFrom6ToAWithSameSuit(Panel panel)
        {
            bool result = true;

            ControlCollection controls = panel.Controls;
            if (controls.Count != 9) return false;

            if ((int)((controls[controls.Count - 1] as Card).Face) != 14 // update for topA
                 || ((controls[controls.Count - 2] as Card).Suit != (controls[controls.Count - 1] as Card).Suit)) return false;// update for topA

            for (int i = 0; i < controls.Count-2; i++)
            {
                if (((int)((controls[i] as Card).Face) != (int)((controls[i + 1] as Card).Face) + 1)
                    || ((controls[i] as Card).Suit != (controls[i + 1] as Card).Suit)) return false;
            }

            return result;
        }
            
    }
}
