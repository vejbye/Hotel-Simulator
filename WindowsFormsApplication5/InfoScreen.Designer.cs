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
            this.guestsDG = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.facilitiesDG = new System.Windows.Forms.DataGridView();
            this.elevatorDG = new System.Windows.Forms.DataGridView();
            this.infoTAB.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guestsDG)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.facilitiesDG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorDG)).BeginInit();
            this.SuspendLayout();
            // 
            // infoTAB
            // 
            this.infoTAB.Controls.Add(this.tabPage1);
            this.infoTAB.Controls.Add(this.tabPage2);
            this.infoTAB.Controls.Add(this.tabPage3);
            this.infoTAB.Location = new System.Drawing.Point(5, 3);
            this.infoTAB.Name = "infoTAB";
            this.infoTAB.SelectedIndex = 0;
            this.infoTAB.Size = new System.Drawing.Size(472, 257);
            this.infoTAB.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.guestsDG);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 231);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Guests";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // guestsDG
            // 
            this.guestsDG.AllowUserToAddRows = false;
            this.guestsDG.AllowUserToDeleteRows = false;
            this.guestsDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guestsDG.Location = new System.Drawing.Point(0, 0);
            this.guestsDG.Name = "guestsDG";
            this.guestsDG.ReadOnly = true;
            this.guestsDG.Size = new System.Drawing.Size(601, 231);
            this.guestsDG.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.facilitiesDG);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(464, 231);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Facilities";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.elevatorDG);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(464, 231);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Elevator";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // facilitiesDG
            // 
            this.facilitiesDG.AllowUserToAddRows = false;
            this.facilitiesDG.AllowUserToDeleteRows = false;
            this.facilitiesDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.facilitiesDG.Location = new System.Drawing.Point(0, 0);
            this.facilitiesDG.Name = "facilitiesDG";
            this.facilitiesDG.ReadOnly = true;
            this.facilitiesDG.Size = new System.Drawing.Size(465, 231);
            this.facilitiesDG.TabIndex = 1;
            // 
            // elevatorDG
            // 
            this.elevatorDG.AllowUserToAddRows = false;
            this.elevatorDG.AllowUserToDeleteRows = false;
            this.elevatorDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.elevatorDG.Location = new System.Drawing.Point(0, 0);
            this.elevatorDG.Name = "elevatorDG";
            this.elevatorDG.ReadOnly = true;
            this.elevatorDG.Size = new System.Drawing.Size(465, 231);
            this.elevatorDG.TabIndex = 2;
            // 
            // InfoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 264);
            this.Controls.Add(this.infoTAB);
            this.Name = "InfoScreen";
            this.Text = "InfoScreen";
            this.infoTAB.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guestsDG)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.facilitiesDG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorDG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl infoTAB;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView guestsDG;
        private System.Windows.Forms.DataGridView facilitiesDG;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView elevatorDG;
    }
}