namespace WindowsFormsApplication5
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.guestdurationLBL = new System.Windows.Forms.Label();
            this.hteCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cleaningCB = new System.Windows.Forms.ComboBox();
            this.dyingCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.elevatorspeedLBL = new System.Windows.Forms.Label();
            this.okBTN = new System.Windows.Forms.Button();
            this.elevatorCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // guestdurationLBL
            // 
            this.guestdurationLBL.AutoSize = true;
            this.guestdurationLBL.Location = new System.Drawing.Point(12, 17);
            this.guestdurationLBL.Name = "guestdurationLBL";
            this.guestdurationLBL.Size = new System.Drawing.Size(78, 13);
            this.guestdurationLBL.TabIndex = 0;
            this.guestdurationLBL.Text = "Guest Duration";
            this.toolTip1.SetToolTip(this.guestdurationLBL, "The hotel\'s time unit");
            // 
            // hteCB
            // 
            this.hteCB.FormattingEnabled = true;
            this.hteCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.hteCB.Location = new System.Drawing.Point(159, 14);
            this.hteCB.Name = "hteCB";
            this.hteCB.Size = new System.Drawing.Size(121, 21);
            this.hteCB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cleaning Duration";
            this.toolTip1.SetToolTip(this.label1, "How long the maid will take to clean a room");
            // 
            // cleaningCB
            // 
            this.cleaningCB.FormattingEnabled = true;
            this.cleaningCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cleaningCB.Location = new System.Drawing.Point(159, 41);
            this.cleaningCB.Name = "cleaningCB";
            this.cleaningCB.Size = new System.Drawing.Size(121, 21);
            this.cleaningCB.TabIndex = 3;
            // 
            // dyingCB
            // 
            this.dyingCB.FormattingEnabled = true;
            this.dyingCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.dyingCB.Location = new System.Drawing.Point(159, 68);
            this.dyingCB.Name = "dyingCB";
            this.dyingCB.Size = new System.Drawing.Size(121, 21);
            this.dyingCB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Dying Timer";
            this.toolTip1.SetToolTip(this.label2, "Sets a timer for dying when guest or maid waits too long in queue");
            // 
            // elevatorspeedLBL
            // 
            this.elevatorspeedLBL.AutoSize = true;
            this.elevatorspeedLBL.Location = new System.Drawing.Point(12, 100);
            this.elevatorspeedLBL.Name = "elevatorspeedLBL";
            this.elevatorspeedLBL.Size = new System.Drawing.Size(80, 13);
            this.elevatorspeedLBL.TabIndex = 7;
            this.elevatorspeedLBL.Text = "Elevator Speed";
            this.toolTip1.SetToolTip(this.elevatorspeedLBL, "Sets a timer for dying when guest or maid waits too long in queue");
            // 
            // okBTN
            // 
            this.okBTN.Location = new System.Drawing.Point(112, 225);
            this.okBTN.Name = "okBTN";
            this.okBTN.Size = new System.Drawing.Size(75, 23);
            this.okBTN.TabIndex = 6;
            this.okBTN.Text = "OK";
            this.okBTN.UseVisualStyleBackColor = true;
            this.okBTN.Click += new System.EventHandler(this.okBTN_Click);
            // 
            // elevatorCB
            // 
            this.elevatorCB.FormattingEnabled = true;
            this.elevatorCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.elevatorCB.Location = new System.Drawing.Point(159, 97);
            this.elevatorCB.Name = "elevatorCB";
            this.elevatorCB.Size = new System.Drawing.Size(121, 21);
            this.elevatorCB.TabIndex = 8;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 260);
            this.Controls.Add(this.elevatorCB);
            this.Controls.Add(this.elevatorspeedLBL);
            this.Controls.Add(this.okBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dyingCB);
            this.Controls.Add(this.cleaningCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hteCB);
            this.Controls.Add(this.guestdurationLBL);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label guestdurationLBL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dyingCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button okBTN;
        public System.Windows.Forms.ComboBox hteCB;
        private System.Windows.Forms.Label elevatorspeedLBL;
        public System.Windows.Forms.ComboBox elevatorCB;
        public System.Windows.Forms.ComboBox cleaningCB;
    }
}