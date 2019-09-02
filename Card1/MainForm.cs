using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Card1.GameManager;
using static Card1.RecordMove;
using static Card1.AutoReplay;

namespace Card1
{
    ///    The MainForm class can :
    ///1.	Have private global references for the Card being dragged and the Place that is the target of the drop.
    ///2.	Have a[Reset] button that creates 36 Card objects randomly placed on a table, and then position some of Cards in the starting position.
    ///3.	Check if a move can start when the user does a MouseDown on a card label and give an error message if it cannot.
    ///4.	Check if a drop can happen on a Place and only show the AllowDrop cursor when the mouse is over a Place if a drop is valid.
    ///5.	Display a count of the moves made and show the moves made in a textbox, one line per move.
    public partial class MainForm : Form
    {
        public bool isAutoReplaying = false;
        public bool isReload = false;
        public static UndoForm undoForm;
        public static HelpForm helpForm;
        public static TestForm testFrom;

        public MainForm()
        {
            InitializeComponent();

            // style mainform
            stylingMainForm();
        }

        ///<summary>method:stylingMainForm
        ///styling main from and set form dragble 
        /// </summary>
        private void stylingMainForm()
        {
            this.Location = new Point(0, 0);

            int x = 0;
            int y = 0; // for mouse point
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(1200, 800);
            this.MouseDown += (sender, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    x = e.X;
                    y = e.Y;
                }
            };
            this.MouseMove += (sender, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.Location = new Point(this.Location.X + (e.X - x), this.Location.Y + (e.Y - y));
                }
            };
        }

        ///<summary>method:closeMainForm_Click
        ///close form and save game
        /// </summary>
        private void closeMainForm_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save game?", "close and save", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // save game:store steps into json file
                SaveGame();
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //close window
                this.Close();
            }
        }

        ///<summary>method:MainForm_Load
        ///mainform load
        ///About the reference: customedTimer1 is a customed component, it is created in the other project.
        ///In here, it makes use of customedTimer1 by importing .dll file.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // clean data existing in the previrse game
            AutoReplay.isAutoReplaying = false;

            // start game(include creating  static undo object)
            startGame(this,isReload);

            // initiate the undo form
            undoForm = new UndoForm(this, RecordMove.cms);

            // time start
            customedTimer1.Interval = 1000;
            customedTimer1.Start();
        }

        ///<summary>method:btnUndo_Click
        ///undo click handler
        /// </summary>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            undoForm.ShowDialog();
        }

        /// <summary>method:btnReset_Click
        ///reset click handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            AutoReplay.isAutoReplaying = false;

            // start a new game
            this.Hide();  
            MainForm formNew = new MainForm(); 
            formNew.ShowDialog();
            this.Close();
        }

        /// <summary>method:btnReplay_Click
        /// replay click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnReplay_Click(object sender, EventArgs e)
        {
            ReplayGame(RecordMove.cms, this);
        }

        /// <summary>method:MainForm_KeyDown
        /// show undo form shorcut key controlling
        /// About composition of Z and control ref:https://stackoverflow.com/questions/1434867/how-to-use-multiple-modifier-keys-in-c-sharp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
            {
                undoForm.ShowDialog();
            }
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                if (testFrom == null || testFrom.IsDisposed)
                {
                    testFrom = new TestForm(ref cms, this);
                    testFrom.Show();
                }
                else
                {
                    testFrom.Activate();
                }
            }
        }

        /// <summary>method:btnHelp_Click
        /// show help form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            helpForm = new HelpForm();
            helpForm.Show();
        }

        /// <summary>method:btnReload_Click
        /// eventhandler for reloading the privious game saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            // clean data existing in the previrse game
            AutoReplay.isAutoReplaying = false;
            isReload = true;

            // start game(include creating  static undo object)
            startGame(this,isReload);

            // initiate the undo form
            undoForm = new UndoForm(this, RecordMove.cms);
            lblCurrentStep.Text = RecordMove.cms.moves.Count.ToString();
            isReload = false;
        }
    }
}
