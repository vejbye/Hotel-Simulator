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
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).BeginInit();
            this.SuspendLayout();
            // 
            // screenPB
            // 
            this.screenPB.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.screenPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.screenPB.Location = new System.Drawing.Point(-2, 0);
            this.screenPB.Name = "screenPB";
            this.screenPB.Size = new System.Drawing.Size(1175, 540);
            this.screenPB.TabIndex = 0;
            this.screenPB.TabStop = false;
            this.screenPB.Paint += new System.Windows.Forms.PaintEventHandler(this.screenPB_Paint);
            this.screenPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseDown);
            this.screenPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseMove);
            this.screenPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseUp);
            // 
            // loadlayoutBTN
            // 
            this.loadlayoutBTN.Location = new System.Drawing.Point(1030, 562);
            this.loadlayoutBTN.Name = "loadlayoutBTN";
            this.loadlayoutBTN.Size = new System.Drawing.Size(114, 28);
            this.loadlayoutBTN.TabIndex = 1;
            this.loadlayoutBTN.Text = "Load layout";
            this.loadlayoutBTN.UseVisualStyleBackColor = true;
            this.loadlayoutBTN.Click += new System.EventHandler(this.loadlayoutBTN_Click);
            // 
            // HotelSimulator
            // 
            //this.AutoScaledimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 614);
            this.Controls.Add(this.loadlayoutBTN);
            this.Controls.Add(this.screenPB);
            this.Name = "HotelSimulator";
            this.Text = "Hotel Simulator 2016";
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox screenPB;
        private System.Windows.Forms.Button loadlayoutBTN;
    }
}
