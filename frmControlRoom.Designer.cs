using System.ComponentModel;

namespace AlbyAirLines
{
    partial class frmControlRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlRoom));
            this.pcbMap = new System.Windows.Forms.PictureBox();
            this.tmrFetch = new System.Windows.Forms.Timer(this.components);
            this.tmrUpdateCockPit = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMap)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbMap
            // 
            this.pcbMap.Image = ((System.Drawing.Image)(resources.GetObject("pcbMap.Image")));
            this.pcbMap.Location = new System.Drawing.Point(12, 12);
            this.pcbMap.Name = "pcbMap";
            this.pcbMap.Size = new System.Drawing.Size(1143, 971);
            this.pcbMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbMap.TabIndex = 0;
            this.pcbMap.TabStop = false;
            // 
            // tmrFetch
            // 
            this.tmrFetch.Interval = 5;
            this.tmrFetch.Tick += new System.EventHandler(this.tmrFetch_Tick);
            // 
            // tmrUpdateCockPit
            // 
            this.tmrUpdateCockPit.Tick += new System.EventHandler(this.tmrUpdateCockPit_Tick);
            // 
            // frmControlRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 992);
            this.Controls.Add(this.pcbMap);
            this.Name = "frmControlRoom";
            this.Text = "frmControlRoom";
            this.Load += new System.EventHandler(this.frmControlRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Timer tmrFetch;

        private System.Windows.Forms.PictureBox pcbMap;

        #endregion

        private System.Windows.Forms.Timer tmrUpdateCockPit;
    }
}