﻿namespace HotelSimulator
{
    partial class HotelSimulator
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
            this.screenPB = new System.Windows.Forms.PictureBox();
            this.loadlayoutBTN = new System.Windows.Forms.Button();
            this.settingsBTN = new System.Windows.Forms.Button();
            this.warningLBL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).BeginInit();
            this.SuspendLayout();
            // 
            // screenPB
            // 
            this.screenPB.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.screenPB.BackgroundImage = global::WindowsFormsApplication5.Properties.Resources.SimulatorBG;
            this.screenPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.screenPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenPB.Location = new System.Drawing.Point(-2, 0);
            this.screenPB.Name = "screenPB";
            this.screenPB.Size = new System.Drawing.Size(1175, 540);
            this.screenPB.TabIndex = 0;
            this.screenPB.TabStop = false;
            this.screenPB.Paint += new System.Windows.Forms.PaintEventHandler(this.screenPB_Paint);
            this.screenPB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseClick);
            this.screenPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseDown);
            this.screenPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseMove);
            this.screenPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseUp);
            // 
            // loadlayoutBTN
            // 
            this.loadlayoutBTN.Location = new System.Drawing.Point(1030, 554);
            this.loadlayoutBTN.Name = "loadlayoutBTN";
            this.loadlayoutBTN.Size = new System.Drawing.Size(114, 44);
            this.loadlayoutBTN.TabIndex = 1;
            this.loadlayoutBTN.Text = "Load layout";
            this.loadlayoutBTN.UseVisualStyleBackColor = true;
            this.loadlayoutBTN.Click += new System.EventHandler(this.loadlayoutBTN_Click);
            // 
            // settingsBTN
            // 
            this.settingsBTN.Location = new System.Drawing.Point(903, 554);
            this.settingsBTN.Name = "settingsBTN";
            this.settingsBTN.Size = new System.Drawing.Size(109, 43);
            this.settingsBTN.TabIndex = 2;
            this.settingsBTN.Text = "Settings";
            this.settingsBTN.UseVisualStyleBackColor = true;
            this.settingsBTN.Click += new System.EventHandler(this.settingsBTN_Click);
            // 
            // warningLBL
            // 
            this.warningLBL.AutoSize = true;
            this.warningLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.warningLBL.Location = new System.Drawing.Point(362, 565);
            this.warningLBL.Name = "warningLBL";
            this.warningLBL.Size = new System.Drawing.Size(524, 20);
            this.warningLBL.TabIndex = 3;
            this.warningLBL.Text = "(NOTE: Settings will only take effect if you configure before loading hotel!)";
            this.warningLBL.Click += new System.EventHandler(this.warningLBL_Click);
            // 
            // HotelSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 614);
            this.Controls.Add(this.warningLBL);
            this.Controls.Add(this.settingsBTN);
            this.Controls.Add(this.loadlayoutBTN);
            this.Controls.Add(this.screenPB);
            this.Name = "HotelSimulator";
            this.Text = "Hotel Simulator 2016";
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox screenPB;
        private System.Windows.Forms.Button loadlayoutBTN;
        private System.Windows.Forms.Button settingsBTN;
        private System.Windows.Forms.Label warningLBL;
    }
}

