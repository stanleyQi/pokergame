namespace Card1
{
    partial class TestForm
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
            this.lstTest = new System.Windows.Forms.ListBox();
            this.btnPerform = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstTest
            // 
            this.lstTest.FormattingEnabled = true;
            this.lstTest.ItemHeight = 12;
            this.lstTest.Location = new System.Drawing.Point(12, 12);
            this.lstTest.Name = "lstTest";
            this.lstTest.Size = new System.Drawing.Size(976, 244);
            this.lstTest.TabIndex = 0;
            this.lstTest.SelectedIndexChanged += new System.EventHandler(this.lstTest_SelectedIndexChanged);
            // 
            // btnPerform
            // 
            this.btnPerform.Font = new System.Drawing.Font("Helvetica", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerform.Location = new System.Drawing.Point(135, 262);
            this.btnPerform.Name = "btnPerform";
            this.btnPerform.Size = new System.Drawing.Size(703, 52);
            this.btnPerform.TabIndex = 1;
            this.btnPerform.Text = "Perform";
            this.btnPerform.UseVisualStyleBackColor = true;
            this.btnPerform.Click += new System.EventHandler(this.btnPerform_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(878, 291);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 326);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPerform);
            this.Controls.Add(this.lstTest);
            this.Name = "TestForm";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstTest;
        private System.Windows.Forms.Button btnPerform;
        private System.Windows.Forms.Button btnClose;
    }
}