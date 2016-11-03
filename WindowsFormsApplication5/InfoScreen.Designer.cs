namespace WindowsFormsApplication5
{
    partial class InfoScreen
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
            this.infoTAB = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.guestsDG = new System.Windows.Forms.DataGridView();
            this.GuestColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoTAB.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guestsDG)).BeginInit();
            this.SuspendLayout();
            // 
            // infoTAB
            // 
            this.infoTAB.Controls.Add(this.tabPage1);
            this.infoTAB.Controls.Add(this.tabPage2);
            this.infoTAB.Location = new System.Drawing.Point(12, 12);
            this.infoTAB.Name = "infoTAB";
            this.infoTAB.SelectedIndex = 0;
            this.infoTAB.Size = new System.Drawing.Size(309, 257);
            this.infoTAB.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.guestsDG);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(301, 231);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Guests";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(301, 231);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Special Rooms";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // guestsDG
            // 
            this.guestsDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guestsDG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuestColumn,
            this.DestinationColumn});
            this.guestsDG.Location = new System.Drawing.Point(0, 0);
            this.guestsDG.Name = "guestsDG";
            this.guestsDG.Size = new System.Drawing.Size(298, 231);
            this.guestsDG.TabIndex = 0;
            // 
            // GuestColumn
            // 
            this.GuestColumn.HeaderText = "Guest";
            this.GuestColumn.Name = "GuestColumn";
            // 
            // DestinationColumn
            // 
            this.DestinationColumn.HeaderText = "Destination";
            this.DestinationColumn.Name = "DestinationColumn";
            // 
            // InfoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 281);
            this.Controls.Add(this.infoTAB);
            this.Name = "InfoScreen";
            this.Text = "InfoScreen";
            this.Load += new System.EventHandler(this.InfoScreen_Load);
            this.infoTAB.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guestsDG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl infoTAB;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView guestsDG;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuestColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationColumn;
    }
}