namespace Card1
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.undeal = new System.Windows.Forms.Panel();
            this.set1 = new System.Windows.Forms.Panel();
            this.slot1 = new System.Windows.Forms.Panel();
            this.slot4 = new System.Windows.Forms.Panel();
            this.set3 = new System.Windows.Forms.Panel();
            this.slot6 = new System.Windows.Forms.Panel();
            this.unuse = new System.Windows.Forms.Panel();
            this.slot2 = new System.Windows.Forms.Panel();
            this.slot3 = new System.Windows.Forms.Panel();
            this.set2 = new System.Windows.Forms.Panel();
            this.slot5 = new System.Windows.Forms.Panel();
            this.set4 = new System.Windows.Forms.Panel();
            this.slot7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentStep = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tt1 = new System.Windows.Forms.ToolTip(this.components);
            this.tt2 = new System.Windows.Forms.ToolTip(this.components);
            this.btnReload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.customedTimer1 = new CustomedTimer.CustomedTimer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Helvetica", 16F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(1157, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.closeMainForm_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Helvetica", 14F);
            this.btnReset.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReset.Location = new System.Drawing.Point(23, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 30);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "New";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.Transparent;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Helvetica", 14F);
            this.btnUndo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUndo.Location = new System.Drawing.Point(271, 5);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(83, 30);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.BackColor = System.Drawing.Color.Transparent;
            this.btnReplay.FlatAppearance.BorderSize = 0;
            this.btnReplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnReplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnReplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplay.Font = new System.Drawing.Font("Helvetica", 14F);
            this.btnReplay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReplay.Location = new System.Drawing.Point(357, 4);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(128, 30);
            this.btnReplay.TabIndex = 3;
            this.btnReplay.Text = "AutoReplay";
            this.tt1.SetToolTip(this.btnReplay, "Please click after completing the game.");
            this.btnReplay.UseVisualStyleBackColor = false;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // undeal
            // 
            this.undeal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("undeal.BackgroundImage")));
            this.undeal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.undeal.Location = new System.Drawing.Point(15, 12);
            this.undeal.Name = "undeal";
            this.undeal.Size = new System.Drawing.Size(120, 150);
            this.undeal.TabIndex = 0;
            // 
            // set1
            // 
            this.set1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.set1.Location = new System.Drawing.Point(521, 12);
            this.set1.Name = "set1";
            this.set1.Size = new System.Drawing.Size(150, 200);
            this.set1.TabIndex = 0;
            // 
            // slot1
            // 
            this.slot1.Location = new System.Drawing.Point(15, 243);
            this.slot1.Name = "slot1";
            this.slot1.Size = new System.Drawing.Size(150, 465);
            this.slot1.TabIndex = 0;
            // 
            // slot4
            // 
            this.slot4.Location = new System.Drawing.Point(521, 243);
            this.slot4.Name = "slot4";
            this.slot4.Size = new System.Drawing.Size(150, 465);
            this.slot4.TabIndex = 0;
            // 
            // set3
            // 
            this.set3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.set3.Location = new System.Drawing.Point(838, 12);
            this.set3.Name = "set3";
            this.set3.Size = new System.Drawing.Size(150, 200);
            this.set3.TabIndex = 0;
            // 
            // slot6
            // 
            this.slot6.Location = new System.Drawing.Point(838, 243);
            this.slot6.Name = "slot6";
            this.slot6.Size = new System.Drawing.Size(150, 465);
            this.slot6.TabIndex = 0;
            // 
            // unuse
            // 
            this.unuse.Location = new System.Drawing.Point(172, 12);
            this.unuse.Name = "unuse";
            this.unuse.Size = new System.Drawing.Size(120, 150);
            this.unuse.TabIndex = 0;
            // 
            // slot2
            // 
            this.slot2.Location = new System.Drawing.Point(185, 243);
            this.slot2.Name = "slot2";
            this.slot2.Size = new System.Drawing.Size(150, 465);
            this.slot2.TabIndex = 0;
            // 
            // slot3
            // 
            this.slot3.Location = new System.Drawing.Point(353, 243);
            this.slot3.Name = "slot3";
            this.slot3.Size = new System.Drawing.Size(150, 465);
            this.slot3.TabIndex = 0;
            // 
            // set2
            // 
            this.set2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.set2.Location = new System.Drawing.Point(679, 12);
            this.set2.Name = "set2";
            this.set2.Size = new System.Drawing.Size(150, 200);
            this.set2.TabIndex = 0;
            // 
            // slot5
            // 
            this.slot5.Location = new System.Drawing.Point(679, 243);
            this.slot5.Name = "slot5";
            this.slot5.Size = new System.Drawing.Size(150, 465);
            this.slot5.TabIndex = 0;
            // 
            // set4
            // 
            this.set4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.set4.Location = new System.Drawing.Point(997, 12);
            this.set4.Name = "set4";
            this.set4.Size = new System.Drawing.Size(150, 200);
            this.set4.TabIndex = 0;
            // 
            // slot7
            // 
            this.slot7.Location = new System.Drawing.Point(997, 243);
            this.slot7.Name = "slot7";
            this.slot7.Size = new System.Drawing.Size(150, 465);
            this.slot7.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.slot7);
            this.panel1.Controls.Add(this.set4);
            this.panel1.Controls.Add(this.slot5);
            this.panel1.Controls.Add(this.set2);
            this.panel1.Controls.Add(this.slot3);
            this.panel1.Controls.Add(this.slot2);
            this.panel1.Controls.Add(this.unuse);
            this.panel1.Controls.Add(this.slot6);
            this.panel1.Controls.Add(this.set3);
            this.panel1.Controls.Add(this.slot4);
            this.panel1.Controls.Add(this.slot1);
            this.panel1.Controls.Add(this.set1);
            this.panel1.Controls.Add(this.undeal);
            this.panel1.Location = new System.Drawing.Point(23, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 750);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Helvetica", 16F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(663, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current Step: ";
            // 
            // lblCurrentStep
            // 
            this.lblCurrentStep.AutoSize = true;
            this.lblCurrentStep.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentStep.Font = new System.Drawing.Font("Helvetica", 16F);
            this.lblCurrentStep.ForeColor = System.Drawing.Color.White;
            this.lblCurrentStep.Location = new System.Drawing.Point(800, 9);
            this.lblCurrentStep.Name = "lblCurrentStep";
            this.lblCurrentStep.Size = new System.Drawing.Size(24, 25);
            this.lblCurrentStep.TabIndex = 5;
            this.lblCurrentStep.Text = "0";
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Helvetica", 14F);
            this.btnHelp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHelp.Location = new System.Drawing.Point(488, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(83, 30);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Help";
            this.tt2.SetToolTip(this.btnHelp, "Here you can find the game description");
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.Transparent;
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnReload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Helvetica", 14F);
            this.btnReload.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReload.Location = new System.Drawing.Point(93, 5);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(128, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Helvetica", 16F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(869, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time:";
            // 
            // customedTimer1
            // 
            this.customedTimer1.AutoSize = true;
            this.customedTimer1.BackColor = System.Drawing.Color.Transparent;
            this.customedTimer1.Font = new System.Drawing.Font("Helvetica", 14F);
            this.customedTimer1.ForeColor = System.Drawing.Color.White;
            this.customedTimer1.Interval = 1000;
            this.customedTimer1.Location = new System.Drawing.Point(933, 10);
            this.customedTimer1.Name = "customedTimer1";
            this.customedTimer1.Size = new System.Drawing.Size(86, 22);
            this.customedTimer1.TabIndex = 6;
            this.customedTimer1.Text = "00:00:00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.ControlBox = false;
            this.Controls.Add(this.customedTimer1);
            this.Controls.Add(this.lblCurrentStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnReplay;
        public System.Windows.Forms.Panel undeal;
        public System.Windows.Forms.Panel set1;
        public System.Windows.Forms.Panel slot1;
        public System.Windows.Forms.Panel slot4;
        public System.Windows.Forms.Panel set3;
        public System.Windows.Forms.Panel slot6;
        public System.Windows.Forms.Panel unuse;
        public System.Windows.Forms.Panel slot2;
        public System.Windows.Forms.Panel slot3;
        public System.Windows.Forms.Panel set2;
        public System.Windows.Forms.Panel slot5;
        public System.Windows.Forms.Panel set4;
        public System.Windows.Forms.Panel slot7;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentStep;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolTip tt1;
        private System.Windows.Forms.ToolTip tt2;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label label2;
        public CustomedTimer.CustomedTimer customedTimer1;
    }
}

