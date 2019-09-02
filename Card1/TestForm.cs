using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using static Card1.AutoReplay;

namespace Card1
{
    public partial class TestForm : Form
    {
        private static CardMoveStore cmsForTest;
        private static MainForm mainFormForTest;

        private static string testScriptName = "storage-1.json";
        public TestForm()
        {
            InitializeComponent();
        }

        public TestForm(ref CardMoveStore cms, MainForm form)
        {
            InitializeComponent();
            cmsForTest = cms;
            mainFormForTest = form;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            List<string> testList = new List<string>();
            testList.Add("Test1:The program displays the message “You have successfully completed the game” when all of the cards have been transferred to available places");
            testList.Add("Test2:Interface is displayed correctly when the program runs");
            testList.Add("Test3:Valid moves are stored");
            testList.Add("Test4:Uses 36 cards and four Places and lets the user drag and drop cards from table to one of four Place.");
            testList.Add("Test5:Keeps count of the number of moves");
            testList.Add("Test6:The program displays the message “You have been unable to successfully complete the game” when it becomes impossible to transfer all of the cards to the four places.");
            testList.Add("Test7:Enforces the rules");
            testList.Add("Test8:Try moving more than one card at a time");
            testList.Add("Test9:Try moving a card at the bottom of a pile");
            testList.Add("Test10:Try moving a card second from top of a pile");
            testList.Add("Test11:Try putting a larger card on top of a smaller card");
            testList.Add("Test12:Begin a new game after a game has been started");
            testList.Add("Test13:Number of moves set to zero");
            testList.Add("Test14:Cards moved back to starting positions");
            testList.Add("Test15:Load an incomplete stored game and finish it");
            testList.Add("Test16:Play a completed game from a list of stored moves using animation controlled by a timer");

            lstTest.DataSource = testList;
            //lstTest.DisplayMember = "Move";
            //lstTest.ValueMember = "Step";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            int testID = lstTest.SelectedIndex + 1;
            testScriptName = "storage-" + testID + ".json";
        }

        private void btnPerform_Click(object sender, EventArgs e)
        {
            //
            if (testScriptName == "storage-3.json" || testScriptName == "storage-7.json")
            {
                MessageBox.Show("Please test this requirement manually by referring the assotiated testcase.");
                return;
            }

            string stringJson;

            string jsonfile = System.Windows.Forms.Application.StartupPath + @"../../../" + @"/test/" + testScriptName;
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    stringJson = JToken.ReadFrom(reader).ToString().Replace("\r\n","") ;
                }
            }

            // set RecordMove.cms
            cmsForTest = JsonConvert.DeserializeObject<CardMoveStore>(stringJson);
            foreach ( MoveNode move in cmsForTest.moves)
            {
                for (int i = 0; i < 13; i++)
                {
                    Array.Reverse(move.screenshot[i]);
                }
                
            }
            foreach (int[] area in cmsForTest.game.init)
            {
                    Array.Reverse(area);
            }


            // perfrom the test
            ReplayGame(cmsForTest, mainFormForTest);

            this.WindowState = FormWindowState.Minimized;
        }
    }
}
