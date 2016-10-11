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
            this.typeOfRoomLBL = new System.Windows.Forms.Label();
            this.unknownLBL = new System.Windows.Forms.Label();
            this.peopleLBL = new System.Windows.Forms.Label();
            this.unknownLBL2 = new System.Windows.Forms.Label();
            this.dimensionLBL = new System.Windows.Forms.Label();
            this.unknownLBL3 = new System.Windows.Forms.Label();
            this.floorLBL = new System.Windows.Forms.Label();
            this.unknownLBL4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // typeOfRoomLBL
            // 
            this.typeOfRoomLBL.AutoSize = true;
            this.typeOfRoomLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeOfRoomLBL.Location = new System.Drawing.Point(12, 9);
            this.typeOfRoomLBL.Name = "typeOfRoomLBL";
            this.typeOfRoomLBL.Size = new System.Drawing.Size(94, 13);
            this.typeOfRoomLBL.TabIndex = 0;
            this.typeOfRoomLBL.Text = "Type of Room: ";
            // 
            // unknownLBL
            // 
            this.unknownLBL.AutoSize = true;
            this.unknownLBL.Location = new System.Drawing.Point(122, 9);
            this.unknownLBL.Name = "unknownLBL";
            this.unknownLBL.Size = new System.Drawing.Size(53, 13);
            this.unknownLBL.TabIndex = 1;
            this.unknownLBL.Text = "Unknown";
            // 
            // peopleLBL
            // 
            this.peopleLBL.AutoSize = true;
            this.peopleLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peopleLBL.Location = new System.Drawing.Point(14, 41);
            this.peopleLBL.Name = "peopleLBL";
            this.peopleLBL.Size = new System.Drawing.Size(54, 13);
            this.peopleLBL.TabIndex = 2;
            this.peopleLBL.Text = "People: ";
            // 
            // unknownLBL2
            // 
            this.unknownLBL2.AutoSize = true;
            this.unknownLBL2.Location = new System.Drawing.Point(122, 41);
            this.unknownLBL2.Name = "unknownLBL2";
            this.unknownLBL2.Size = new System.Drawing.Size(53, 13);
            this.unknownLBL2.TabIndex = 3;
            this.unknownLBL2.Text = "Unknown";
            // 
            // dimensionLBL
            // 
            this.dimensionLBL.AutoSize = true;
            this.dimensionLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dimensionLBL.Location = new System.Drawing.Point(14, 74);
            this.dimensionLBL.Name = "dimensionLBL";
            this.dimensionLBL.Size = new System.Drawing.Size(73, 13);
            this.dimensionLBL.TabIndex = 4;
            this.dimensionLBL.Text = "Dimension: ";
            // 
            // unknownLBL3
            // 
            this.unknownLBL3.AutoSize = true;
            this.unknownLBL3.Location = new System.Drawing.Point(122, 74);
            this.unknownLBL3.Name = "unknownLBL3";
            this.unknownLBL3.Size = new System.Drawing.Size(53, 13);
            this.unknownLBL3.TabIndex = 5;
            this.unknownLBL3.Text = "Unknown";
            // 
            // floorLBL
            // 
            this.floorLBL.AutoSize = true;
            this.floorLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorLBL.Location = new System.Drawing.Point(14, 103);
            this.floorLBL.Name = "floorLBL";
            this.floorLBL.Size = new System.Drawing.Size(39, 13);
            this.floorLBL.TabIndex = 6;
            this.floorLBL.Text = "Floor:";
            // 
            // unknownLBL4
            // 
            this.unknownLBL4.AutoSize = true;
            this.unknownLBL4.Location = new System.Drawing.Point(122, 103);
            this.unknownLBL4.Name = "unknownLBL4";
            this.unknownLBL4.Size = new System.Drawing.Size(53, 13);
            this.unknownLBL4.TabIndex = 7;
            this.unknownLBL4.Text = "Unknown";
            // 
            // InfoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 133);
            this.Controls.Add(this.unknownLBL4);
            this.Controls.Add(this.floorLBL);
            this.Controls.Add(this.unknownLBL3);
            this.Controls.Add(this.dimensionLBL);
            this.Controls.Add(this.unknownLBL2);
            this.Controls.Add(this.peopleLBL);
            this.Controls.Add(this.unknownLBL);
            this.Controls.Add(this.typeOfRoomLBL);
            this.Name = "InfoScreen";
            this.Text = "InfoScreen";
            this.Load += new System.EventHandler(this.InfoScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label typeOfRoomLBL;
        private System.Windows.Forms.Label unknownLBL;
        private System.Windows.Forms.Label peopleLBL;
        private System.Windows.Forms.Label unknownLBL2;
        private System.Windows.Forms.Label dimensionLBL;
        private System.Windows.Forms.Label unknownLBL3;
        private System.Windows.Forms.Label floorLBL;
        private System.Windows.Forms.Label unknownLBL4;
    }
}