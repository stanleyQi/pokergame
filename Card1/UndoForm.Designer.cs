namespace Card1
{
    partial class UndoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstUndo = new System.Windows.Forms.ListBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUndo
            // 
            this.lstUndo.BackColor = System.Drawing.Color.Green;
            this.lstUndo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstUndo.Font = new System.Drawing.Font("Helvetica", 12F);
            this.lstUndo.ForeColor = System.Drawing.Color.White;
            this.lstUndo.FormattingEnabled = true;
            this.lstUndo.ItemHeight = 18;
            this.lstUndo.Location = new System.Drawing.Point(12, 12);
            this.lstUndo.Name = "lstUndo";
            this.lstUndo.Size = new System.Drawing.Size(679, 344);
            this.lstUndo.TabIndex = 0;
            this.lstUndo.SelectedIndexChanged += new System.EventHandler(this.lstUndo_SelectedIndexChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 2;
            this.btnConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Helvetica", 12F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(12, 362);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(138, 33);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Undo";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.FlatAppearance.BorderSize = 2;
            this.btnReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Helvetica", 12F);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(560, 362);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(131, 33);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.FlatAppearance.BorderSize = 2;
            this.btnRedo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnRedo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedo.Font = new System.Drawing.Font("Helvetica", 12F);
            this.btnRedo.ForeColor = System.Drawing.Color.White;
            this.btnRedo.Location = new System.Drawing.Point(165, 362);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(138, 33);
            this.btnRedo.TabIndex = 1;
            this.btnRedo.Text = "Redo the game";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // UndoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(702, 405);
            this.ControlBox = false;
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lstUndo);
            this.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UndoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Undo";
            this.Load += new System.EventHandler(this.UndoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstUndo;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnRedo;
    }
}