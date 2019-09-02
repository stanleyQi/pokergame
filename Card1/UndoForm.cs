using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;  
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Card1.RecordMove;
using static Card1.GameManager;

namespace Card1
{
    public partial class UndoForm : Form
    {
        private MainForm mainForm;

        private CardMoveStore cms;
        private List<MoveNode> mnList;
        private MoveNode undoSeletedMoveNode;

        /// <summary>method:UndoForm
        /// constructor
        /// </summary>
        public UndoForm()
        {
            InitializeComponent();
        }

        /// <summary>method:UndoForm
        /// constructor2
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="cms"></param>
        public UndoForm(MainForm mainForm, CardMoveStore cms)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.cms = cms;
        }

        /// <summary>method:UndoForm_Load
        /// styling undo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoForm_Load(object sender, EventArgs e)
        {
            // Get the move info stored and make them displaying  in the listbox
            // 1. Get the "CardMoveStore" object
            // 2. Get the nessisery data from "CardMoveStore" object, and parse them to properaty form for displaying
            // 3. Assign the parsed data to the listbox to display
            mnList = this.cms.moves;
            List<MoveNodeDisplay> mnDisplayList = new List<MoveNodeDisplay>();


            foreach (MoveNode mn in mnList)
            {
                mnDisplayList.Add(new MoveNodeDisplay { Step = mn.step, Move = mn.AsText() });
            }

            lstUndo.DataSource = mnDisplayList;
            lstUndo.DisplayMember = "Move";
            lstUndo.ValueMember = "Step";

        }

        /// <summary>method:btnReturn_Click
        /// event handler: close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            // do nothing and close itself
            Close();
        }

        /// <summary>method:lstUndo_SelectedIndexChanged
        /// event handler: for selecting the step number, so that it can be restored to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstUndo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stepSelected = (lstUndo.SelectedItem as MoveNodeDisplay).Step;
            undoSeletedMoveNode = this.mnList.Find((mn) => { return mn.step == stepSelected; });
        }

        class MoveNodeDisplay
        {
            public int Step { get; set; }
            public string Move { get; set; }
        }

        /// <summary>method:btnConfirm_Click
        /// perform undo operration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            if (lstUndo.Items.Count == 0)
            {
                MessageBox.Show("There is no history record of moves.");
                return;
            }
            if (MessageBox.Show("Are you sure you want to undo from where you selected at?", "Warning",
              MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                AutoReplay.isAutoReplaying = false;

                // compose the data in the "cms" (delete all the data existing after the selected step)
                int selectedIndex = mnList.IndexOf(undoSeletedMoveNode);
                for (int i = mnList.Count - 1; i > selectedIndex; i--)
                {
                    mnList.Remove(mnList[i]);
                }

                lblCurrentStep.Text = mnList.Count.ToString();

                // call game manager to undo the game(pass the "undoSeletedMoveNode" to mainform for perform the undo operation)
                List<List<Card>> undoCardsWithinAreaList = new List<List<Card>>();
                int[][] undoSeletedScreenshot = undoSeletedMoveNode.screenshot;
                for (int i = 0; i < undoSeletedScreenshot.Length; i++)
                {
                    List<Card> cardList = new List<Card>();
                    if (undoSeletedScreenshot[i].Length > 0) { 
                        for (int j = 0; j < undoSeletedScreenshot[i].Length; j++)
                        {
                            cardList.Add(Tools.ParseToCard(undoSeletedScreenshot[i][j],i));
                        }
                    }
                    undoCardsWithinAreaList.Add(cardList);

                }

                undoGame(this.mainForm, undoCardsWithinAreaList);

                // close itself
                Close();
             }
        }

        /// <summary>method:btnRedo_Click
        /// redo the game from initial status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (lstUndo.Items.Count == 0)
            {
                MessageBox.Show("There is no history record of moves.");
                return;
            }
            if (MessageBox.Show("The history records you have performed will be deleted for redoing the game, are you sure you want to redo the game?", "Warning",
              MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                AutoReplay.isAutoReplaying = false;

                // compose the data in the "cms" (delete all the data existing after the selected step)
                mnList.RemoveRange(0, mnList.Count);
                lblCurrentStep.Text ="0";

                // call game manager to undo the game(pass the "undoSeletedMoveNode" to mainform for perform the undo operation)
                List<List<Card>> undoCardsWithinAreaList = new List<List<Card>>();
                int[][] replayScreenshot = cms.game.init;
                for (int i = 0; i < replayScreenshot.Length; i++)
                {
                    List<Card> cardList = new List<Card>();
                    if (replayScreenshot[i].Length > 0)
                    {
                        for (int j = 0; j < replayScreenshot[i].Length; j++)
                        {
                            cardList.Add(Tools.ParseToCard(replayScreenshot[i][j], i));
                        }
                    }
                    undoCardsWithinAreaList.Add(cardList);

                }

                undoGame(this.mainForm, undoCardsWithinAreaList);

                // close itself
                Close();
            }
        }
    }
}
