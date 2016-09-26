namespace HotelSimulator
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
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).BeginInit();
            this.SuspendLayout();
            // 
            // screenPB
            // 
            this.screenPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.screenPB.Location = new System.Drawing.Point(12, 12);
            this.screenPB.Name = "screenPB";
            this.screenPB.Size = new System.Drawing.Size(1148, 432);
            this.screenPB.TabIndex = 0;
            this.screenPB.TabStop = false;
            this.screenPB.Paint += new System.Windows.Forms.PaintEventHandler(this.screenPB_Paint);
            this.screenPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseDown);
            this.screenPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseMove);
            this.screenPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screenPB_MouseUp);
            // 
            // HotelSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 556);
            this.Controls.Add(this.screenPB);
            this.Name = "HotelSimulator";
            this.Text = "Hotel Simulator 2016";
            this.Load += new System.EventHandler(this.HotelSimulator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox screenPB;
    }
}

