using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Card1.CardMoveTools;

namespace Card1
{
    /// <summary>
    /// The CardMove class can:
    ///•	Store the index of the card making the move and the new Place it is dropped on.
    ///•	Have an AsText () method that gives this information as a string, e.g. “1,2” means that 
    ///         Card 1 moved to Place 2.
    ///•	Have two constructors, one that takes two integers (for card index and Place) as parameters, 
    ///         the other takes a string in the form “2,1” as its parameter.
    /// </summary>
    class CardMove
    {
        public int index { get; set; }
        public int place { get; set; }
        
        /// <summary>method:CardMove
        /// constructor for create a CardMove Object
        /// </summary>
        /// <param name="index"></param>
        /// <param name="place"></param>
        public CardMove(int index, int place)
        {
            this.index = index;
            this.place = place;
        }

        /// <summary>method:CardMove
        /// store step info
        /// </summary>
        /// <param name="indexAndPlace"></param>
        public CardMove(string indexAndPlace)
        {
           this.index= int.Parse(indexAndPlace.Split(',')[0]);
           this.place= int.Parse(indexAndPlace.Split(',')[1]);
        }

        /// <summary>method:AsText
        /// Object identifier
        /// </summary>
        /// <returns></returns>
        public string AsText()
        {
            return this.index.ToString() + "," + this.place.ToString();
        }
    }

    /// <summary>
    /// part of undo data structure
    /// </summary>
    public class CardMoveStore
    {
        public List<MoveNode> moves { get; set; }
        public GameNode game { get; set; }
    }

    /// <summary>
    /// part of undo data structure
    /// </summary>
    public class MoveNode
    {
        public int step { get; set; }
        public int[] cards { get; set; }
        public int source { get; set; }
        public int target { get; set; }
        public int[][] screenshot { get; set; }

        /// <summary>method:AsText
        /// convert every step into string so that can be stored into List Object(for performing undo)
        /// </summary>
        /// <returns></returns>
        public string AsText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Step ").Append(this.step).Append(":  ");
            for (int i = 0; i < cards.Length; i++)
            {
                sb.Append("(").Append(new CardMove(int.Parse(cards[i].ToString().Substring(1, 2)), this.target).AsText()).Append(")  ");
                sb.Append(convertStoredInfoToCard(cards[i], "Suit")).Append("-");
                sb.Append(convertStoredInfoToCard(cards[i], "Face")).Append("; ");
            }
            sb.Append("Moved ").Append(convertStoredInfoToPosition(this.source)).Append(" -> ");
            sb.Append(convertStoredInfoToPosition(this.target));
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }

    /// <summary>
    /// part of undo data structure
    /// </summary>
    public class GameNode
    {
        public int[][] init { get; set; }
        public string signature { get; set; }
    }

    /// <summary>class:CardMoveTools
    /// tools for card moving
    /// </summary>
    public static class CardMoveTools
    {
        /// <summary>method:convertStoredInfoToCard
        /// convert infomation stored in steplist to card object
        /// About convert string to int ref:https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
        /// </summary>
        /// <param name="cardInfoStored"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        public static string convertStoredInfoToCard(int cardInfoStored,string convertType)
        {
            string result;
            int numStored = cardInfoStored;
            int intSuit = int.Parse(numStored.ToString().Substring(0,1));
            int intFace = int.Parse(numStored.ToString().Substring(1,2));
            int intSide = int.Parse(numStored.ToString().Substring(3,1));


            if ("Suit".Equals(convertType))
            {
                result = Enum.GetName(typeof(SuitEnum), intSuit);
            }
            else if ("Face".Equals(convertType))
            {
                result = Enum.GetName(typeof(FaceEnum), intFace);
            }
            else
            {
                result = "";
            }

            return result;
        }

        /// <summary>method:convertStoredInfoToPosition
        /// convert infomation stored in the steplist  to position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string convertStoredInfoToPosition(int position)
        {
            string result = "undeal";
            switch (position) {
                case 1: result = "undeal";break;
                case 2: result = "unuse";break;
                case 11: result = "slot1";break;
                case 12: result = "slot2";break;
                case 13: result = "slot3";break;
                case 14: result = "slot4";break;
                case 15: result = "slot5";break;
                case 16: result = "slot6";break;
                case 17: result = "slot7";break;
                case 21: result = "set1";break;
                case 22: result = "set2";break;
                case 23: result = "set3";break;
                case 24: result = "set4";break;
            }                 

            return result;
        }
    }
}
