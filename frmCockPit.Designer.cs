namespace AlbyAirLines
{
    partial class frmCockPit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCockPit));
            this.txtIp = new System.Windows.Forms.TextBox();
            this.cmbStart = new System.Windows.Forms.ComboBox();
            this.trbDelta = new System.Windows.Forms.TrackBar();
            this.tmrSend = new System.Windows.Forms.Timer(this.components);
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pcbRocket = new System.Windows.Forms.PictureBox();
            this.pcbStart = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pcbCockPit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRocket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCockPit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(77)))), ((int)(((byte)(89)))));
            this.txtIp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIp.ForeColor = System.Drawing.Color.White;
            this.txtIp.Location = new System.Drawing.Point(595, 398);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(132, 19);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "127.0.0.1";
            // 
            // cmbStart
            // 
            this.cmbStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(77)))), ((int)(((byte)(89)))));
            this.cmbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStart.ForeColor = System.Drawing.Color.White;
            this.cmbStart.FormattingEnabled = true;
            this.cmbStart.Location = new System.Drawing.Point(733, 395);
            this.cmbStart.Name = "cmbStart";
            this.cmbStart.Size = new System.Drawing.Size(126, 28);
            this.cmbStart.TabIndex = 3;
            // 
            // trbDelta
            // 
            this.trbDelta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(99)))), ((int)(((byte)(72)))));
            this.trbDelta.LargeChange = 1;
            this.trbDelta.Location = new System.Drawing.Point(614, 654);
            this.trbDelta.Maximum = 100;
            this.trbDelta.Name = "trbDelta";
            this.trbDelta.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbDelta.Size = new System.Drawing.Size(69, 82);
            this.trbDelta.TabIndex = 4;
            this.trbDelta.Value = 15;
            // 
            // tmrSend
            // 
            this.tmrSend.Interval = 500;
            this.tmrSend.Tick += new System.EventHandler(this.tmrSend_Tick);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 25;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AlbyAirLines.Properties.Resources.stopButton;
            this.pictureBox2.Location = new System.Drawing.Point(581, 454);
            this.pictureBox2.Name = "pcbStop";
            this.pictureBox2.Size = new System.Drawing.Size(31, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pcbStop_Click);
            // 
            // pcbRocket
            // 
            this.pcbRocket.Image = global::AlbyAirLines.Properties.Resources.rocket;
            this.pcbRocket.Location = new System.Drawing.Point(581, 526);
            this.pcbRocket.Name = "pcbRocket";
            this.pcbRocket.Size = new System.Drawing.Size(31, 30);
            this.pcbRocket.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbRocket.TabIndex = 9;
            this.pcbRocket.TabStop = false;
            this.pcbRocket.Click += new System.EventHandler(this.pcbRocket_Click);
            // 
            // pcbStart
            // 
            this.pcbStart.Image = global::AlbyAirLines.Properties.Resources.startButton;
            this.pcbStart.Location = new System.Drawing.Point(581, 490);
            this.pcbStart.Name = "pcbStart";
            this.pcbStart.Size = new System.Drawing.Size(31, 30);
            this.pcbStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbStart.TabIndex = 2;
            this.pcbStart.TabStop = false;
            this.pcbStart.Click += new System.EventHandler(this.pcbStart_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(67)))), ((int)(((byte)(46)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(353, 512);
            this.textBox1.Name = "txtDirection";
            this.textBox1.Size = new System.Drawing.Size(34, 19);
            this.textBox1.TabIndex = 12;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDirection_KeyPress);
            // 
            // pcbCockPit
            // 
            this.pcbCockPit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbCockPit.BackColor = System.Drawing.Color.Transparent;
            this.pcbCockPit.Image = ((System.Drawing.Image)(resources.GetObject("pcbCockPit.Image")));
            this.pcbCockPit.Location = new System.Drawing.Point(12, 12);
            this.pcbCockPit.Name = "pcbCockPit";
            this.pcbCockPit.Size = new System.Drawing.Size(1300, 867);
            this.pcbCockPit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbCockPit.TabIndex = 0;
            this.pcbCockPit.TabStop = false;
            // 
            // frmCockPit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 885);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pcbRocket);
            this.Controls.Add(this.trbDelta);
            this.Controls.Add(this.cmbStart);
            this.Controls.Add(this.pcbStart);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.pcbCockPit);
            this.Name = "frmCockPit";
            this.Text = "Cock pit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCockPit_FormClosing);
            this.Load += new System.EventHandler(this.frmCockPit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRocket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCockPit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.PictureBox pcbStart;
        private System.Windows.Forms.ComboBox cmbStart;
        private System.Windows.Forms.TrackBar trbDelta;
        private System.Windows.Forms.Timer tmrSend;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.PictureBox pcbRocket;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pcbCockPit;
    }
}