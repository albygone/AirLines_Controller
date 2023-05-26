namespace AlbyAirLines
{
    partial class frmClient
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
            this.btnManda = new System.Windows.Forms.Button();
            this.btnRoute = new System.Windows.Forms.Button();
            this.dgvRoute = new System.Windows.Forms.DataGridView();
            this.pgbAndamento = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // btnManda
            // 
            this.btnManda.Location = new System.Drawing.Point(12, 96);
            this.btnManda.Name = "btnManda";
            this.btnManda.Size = new System.Drawing.Size(190, 67);
            this.btnManda.TabIndex = 1;
            this.btnManda.Text = "Manda";
            this.btnManda.UseVisualStyleBackColor = true;
            this.btnManda.Click += new System.EventHandler(this.btnManda_Click);
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(12, 23);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(190, 67);
            this.btnRoute.TabIndex = 2;
            this.btnRoute.Text = "Genera rotta";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // dgvRoute
            // 
            this.dgvRoute.AllowUserToAddRows = false;
            this.dgvRoute.AllowUserToDeleteRows = false;
            this.dgvRoute.AllowUserToResizeColumns = false;
            this.dgvRoute.AllowUserToResizeRows = false;
            this.dgvRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRoute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRoute.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoute.Location = new System.Drawing.Point(246, 24);
            this.dgvRoute.Name = "dgvRoute";
            this.dgvRoute.ReadOnly = true;
            this.dgvRoute.RowTemplate.Height = 28;
            this.dgvRoute.Size = new System.Drawing.Size(565, 226);
            this.dgvRoute.TabIndex = 3;
            // 
            // pgbAndamento
            // 
            this.pgbAndamento.Location = new System.Drawing.Point(16, 192);
            this.pgbAndamento.Name = "pgbAndamento";
            this.pgbAndamento.Size = new System.Drawing.Size(185, 41);
            this.pgbAndamento.TabIndex = 4;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 262);
            this.Controls.Add(this.pgbAndamento);
            this.Controls.Add(this.dgvRoute);
            this.Controls.Add(this.btnRoute);
            this.Controls.Add(this.btnManda);
            this.Name = "frmClient";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.frmClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ProgressBar pgbAndamento;

        private System.Windows.Forms.DataGridView dgvRoute;

        private System.Windows.Forms.Button btnRoute;

        #endregion
        private System.Windows.Forms.Button btnManda;
    }
}