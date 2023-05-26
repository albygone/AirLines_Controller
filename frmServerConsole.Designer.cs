namespace AlbyAirLines
{
    partial class frmServerConsole
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStop = new System.Windows.Forms.Button();
            this.dgvProva = new System.Windows.Forms.DataGridView();
            this.tmrFetch = new System.Timers.Timer();
            this.btnOpenControlRoom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrFetch)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(88, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(188, 63);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Apri client";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dgvProva
            // 
            this.dgvProva.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProva.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProva.Location = new System.Drawing.Point(377, 54);
            this.dgvProva.Name = "dgvProva";
            this.dgvProva.RowTemplate.Height = 28;
            this.dgvProva.Size = new System.Drawing.Size(1143, 743);
            this.dgvProva.TabIndex = 2;
            // 
            // tmrFetch
            // 
            this.tmrFetch.Enabled = true;
            this.tmrFetch.SynchronizingObject = this;
            this.tmrFetch.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrFetch_Elapsed);
            // 
            // btnOpenControlRoom
            // 
            this.btnOpenControlRoom.Location = new System.Drawing.Point(88, 156);
            this.btnOpenControlRoom.Name = "btnOpenControlRoom";
            this.btnOpenControlRoom.Size = new System.Drawing.Size(188, 63);
            this.btnOpenControlRoom.TabIndex = 3;
            this.btnOpenControlRoom.Text = "Apri control room";
            this.btnOpenControlRoom.UseVisualStyleBackColor = true;
            this.btnOpenControlRoom.Click += new System.EventHandler(this.btnOpenControlRoom_Click);
            // 
            // frmServerConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 822);
            this.Controls.Add(this.btnOpenControlRoom);
            this.Controls.Add(this.dgvProva);
            this.Controls.Add(this.btnStop);
            this.Name = "frmServerConsole";
            this.Text = "Server console";
            this.Load += new System.EventHandler(this.frmServerConsole_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrFetch)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnOpenControlRoom;

        private System.Timers.Timer tmrFetch;

        private System.Windows.Forms.DataGridView dgvProva;

        #endregion

        private System.Windows.Forms.Button btnStop;
    }
}

