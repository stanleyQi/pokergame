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
using static Card1.Setting;

namespace Card1
{
    public partial class HelpForm : Form
    {
        /// <summary>method:HelpForm
        /// constructor
        /// </summary>
        public HelpForm()
        {
            InitializeComponent();
        }

        /// <summary>method:HelpForm_Load
        /// load the instruction in page displaying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpForm_Load(object sender, EventArgs e)
        {
            this.rtxHelp.Multiline = true;
            this.rtxHelp.ScrollBars = RichTextBoxScrollBars.Vertical;

            StringBuilder sb = new StringBuilder();
            sb.Append(HELP_TEXT_COMPLETED).Append("\r\n")
                .Append("\r\n")
                .Append(HELP_TEXT_FAILURE).Append("\r\n")
                .Append("---------------------------------------------").Append("\r\n")
                .Append(HELP_TEXT_1).Append("\r\n")
                .Append(HELP_TEXT_2).Append("\r\n")
                .Append(HELP_TEXT_3).Append("\r\n")
                .Append(HELP_TEXT_4).Append("\r\n")
                .Append(HELP_TEXT_5).Append("\r\n")
                .Append(HELP_TEXT_6).Append("\r\n")
                .Append(HELP_TEXT_7).Append("\r\n")
                .Append(HELP_TEXT_8).Append("\r\n")
                .Append(HELP_TEXT_9).Append("\r\n")
                .Append(HELP_TEXT_10).Append("\r\n");

            this.rtxHelp.Text = sb.ToString();
        }

        /// <summary>method:btnClose_Click
        /// close self
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
