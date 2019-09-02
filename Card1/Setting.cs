using System;
using System.Text;

namespace Card1
{
    /// <summary>
    /// Defining const here.
    /// </summary>
    class Setting
    {
        public static int AMOUNT_CARDS = 36;
        public static int AUTOPLAY_INTERVAL = 100;
        public static string UNDEAL_AREA = "undealArea";
        public static int CARD_WIDTH = 105; //140
        public static int CARD_HEIGHT = 135;//180
        public static string STORAGE_FILE_NAME = "storage-previous.json";

        public static int SLOT_WIDTH = 150;
        public static int SLOT_HEIGHT = 432;
        public static string HELP_TEXT_COMPLETED = "Identification completed successfully - All the cards in each one of the 4 sets are listed orderly from 6 to A and being with the same suit.";
        public static string HELP_TEXT_FAILURE = "Identification failure - When it becomes impossible to transfer all of the cards to the four places.";
        public static string HELP_TEXT_1 = "1.Shuffle - cards first fill the empty columns in Tableau, from left to right, each column has one more card than the previous column. All cards’ face is hidden.";
        public static string HELP_TEXT_2 = "2.The last card at each column will show its face.";
        public static string HELP_TEXT_3 = "3.Player can move one card from one column to another if the moving card is one point less than the card at the target column and must be in a different colour.";
        public static string HELP_TEXT_4 = "4.If one column is empty, only an “A” can be placed on it.";
        public static string HELP_TEXT_5 = "5.The smallest card is 6, the biggest card is A. so the order for an empty column from top to bottom is: A, K, Q, J, 10, 9, 8, 7, 6.";
        public static string HELP_TEXT_6 = "6.Player will try to organize the cards in the Tableau, the purpose is to sort every card into 4 columns in the Tableau, player will try to find a bridge to connect the cards and free the hidden cards in each column. If there is not any bridge, player will go to the Stock for help.";
        public static string HELP_TEXT_7 = "7.It is possible the wasted card can become useful in the later game, so if the stock is depleted, the entire waste card pile will go back to the stock with the reversed order. In short, player will find the first waste card showing in stock. This goes as a loop.";
        public static string HELP_TEXT_8 = "8.The goal of the game is to place every card from small to big into the “Foundation”, but an empty foundation only accepts the smallest card to begin with, which is 6. After 6 took place, the player can move a bigger card with exact same shape on top of it.";
        public static string HELP_TEXT_9 = "9.Player can retrieve the top card from the foundation, in order to make a bridge to free more cards from the column.";
        public static string HELP_TEXT_10 = "10.Player wins if all the cards have moved to the foundation.";
    }
}
