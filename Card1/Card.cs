using System;
using System.Drawing;
using System.Windows.Forms;

using static Card1.Setting;
using static Card1.MotionManager;

namespace Card1
{
    public enum FaceEnum
    {
        Ace = 14,
        King = 13,
        Queen = 12,
        Jack = 11,
        Ten = 10,
        Nine = 9,
        Eight = 8,
        Seven = 7,
        Six = 6
    }

    public enum BackEnum
    { Front = 1, Back = -1 }

    public enum SuitEnum
    { Hearts = 1, Spades = 2, Diamonds = 3, Clubs = 4 }

    /// <summary>
    /// A Card class to store data about the cards
    /// The card class should store the card’s face, suit, card back 
    /// and current card face and suit. 
    /// The card class should have methods that allow access to the data items.
    /// </summary>
    public class Card : Button
    {
        private FaceEnum _face;
        private SuitEnum _suit;

        private BackEnum _side;
        private string _area;
        private Point _position;

        public FaceEnum Face { get { return this._face; } set { _face = value; } }
        public SuitEnum Suit { get { return this._suit; } set { _suit = value; } }

        public BackEnum Side { get { return this._side; } set { _side = value; } }
        public string Area { get { return this._area; } set { _area = value; } }
        public Point Position { get { return this._position; } set { _position = value; } }

        /// <summary>
        /// constructor1
        /// </summary>
        /// <param name="faceParam"></param>
        /// <param name="suitParam"></param>
        public Card(int faceParam,int suitParam)
        {
            this._face = (FaceEnum)faceParam;
            this._suit = (SuitEnum)suitParam;
        }

        /// <summary>
        /// constructor2
        /// </summary>
        public Card()
        {

        }

        /// <summary>
        /// draw card at given position with styles specified
        /// About Bitmap ref: https://stackoverflow.com/questions/32207313/difference-between-bitmap-fromfilepath-and-new-bitmappath
        /// </summary>
        /// <param name="areaParam"></param>
        /// <param name="horizotalParam"></param>
        /// <param name="verticalParam"></param>
        /// <param name="sideParam"></param>
        /// <returns></returns>
        public Card CardDraw(string areaParam, int horizotalParam, int verticalParam, int sideParam)
        {
            this._area = areaParam;
            this._position = new Point(horizotalParam, verticalParam);
            this._side = (BackEnum)sideParam;

            this.Location = this._position;
            this.Size = new Size(CARD_WIDTH, CARD_HEIGHT);
            if ( _side == BackEnum.Back )
            {
                this.Image = (Image)(new Bitmap(Image.FromFile(@"..\..\images\back.png"), new Size(CARD_WIDTH, CARD_HEIGHT)));
            }
            else
            {
                this.Image = (Image)(new Bitmap(Image.FromFile(@"..\..\images\" + (int)this._face + (SuitEnum)this._suit + ".png"), new Size(CARD_WIDTH, CARD_HEIGHT)));
                this.MouseDown += card_MouseDown;
            }
            return this;
        }

        /// <summary>
        /// draw card at given position
        /// </summary>
        /// <param name="horizotalParam"></param>
        /// <param name="verticalParam"></param>
        /// <returns></returns>
        public Card CardDraw( int horizotalParam, int verticalParam)
        {
            this._position = new Point(horizotalParam, verticalParam);
            this.Location = this._position;
            return this;
        }

        /// <summary>
        /// draw card with side(face or back) specified
        /// </summary>
        /// <param name="sideParam"></param>
        /// <returns></returns>
        public Card CardDraw(int sideParam)
        {
            this._side = (BackEnum)sideParam;

            if (_side == BackEnum.Back)
            {
                this.Image = (Image)(new Bitmap(Image.FromFile(@"..\..\images\back.png"), new Size(CARD_WIDTH, CARD_HEIGHT)));
                // add evet handler
                //this.Click += card_Click;
            }
            else
            {
                //this.Image = Image.FromFile(@"..\..\images\"+(int)this._face+(SuitEnum)this._suit+".png");
                this.Image = (Image)(new Bitmap(Image.FromFile(@"..\..\images\" + (int)this._face + (SuitEnum)this._suit + ".png"), new Size(CARD_WIDTH, CARD_HEIGHT)));
                // add event handler
                this.MouseDown += card_MouseDown;
            }
            return this;
        }

        /// <summary>
        /// Card identifier as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._face.ToString() +  this._suit.ToString()+this._side;
        }
    }
}
