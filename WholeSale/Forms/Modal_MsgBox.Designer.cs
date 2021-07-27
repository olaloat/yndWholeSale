namespace WholeSale.Forms
{
    partial class Modal_MsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Modal_MsgBox));
            this.pnClose = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbPOS = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tblYesNoCancel = new System.Windows.Forms.TableLayoutPanel();
            this.btnYesNoCancel_yes = new System.Windows.Forms.Button();
            this.btnYesNoCancel_cancel = new System.Windows.Forms.Button();
            this.btnYesNoCancel_No = new System.Windows.Forms.Button();
            this.tblYesNo = new System.Windows.Forms.TableLayoutPanel();
            this.btnYesNo_No = new System.Windows.Forms.Button();
            this.btnYesNo_yes = new System.Windows.Forms.Button();
            this.tblOk = new System.Windows.Forms.TableLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.pcbIcon = new System.Windows.Forms.PictureBox();
            this.pnClose.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tblYesNoCancel.SuspendLayout();
            this.tblYesNo.SuspendLayout();
            this.tblOk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnClose
            // 
            this.pnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.pnClose.Controls.Add(this.button10);
            this.pnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnClose.Location = new System.Drawing.Point(609, 0);
            this.pnClose.Name = "pnClose";
            this.pnClose.Size = new System.Drawing.Size(73, 399);
            this.pnClose.TabIndex = 33;
            // 
            // button10
            // 
            this.button10.Dock = System.Windows.Forms.DockStyle.Top;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Image = ((System.Drawing.Image)(resources.GetObject("button10.Image")));
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(73, 64);
            this.button10.TabIndex = 2;
            this.button10.TabStop = false;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lbHeader.Location = new System.Drawing.Point(103, 19);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(0, 25);
            this.lbHeader.TabIndex = 35;
            this.lbHeader.Click += new System.EventHandler(this.lbHeader_Click);
            // 
            // lbPOS
            // 
            this.lbPOS.AutoSize = true;
            this.lbPOS.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPOS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lbPOS.Location = new System.Drawing.Point(515, 0);
            this.lbPOS.Name = "lbPOS";
            this.lbPOS.Size = new System.Drawing.Size(83, 36);
            this.lbPOS.TabIndex = 36;
            this.lbPOS.Text = "POS";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label3.Location = new System.Drawing.Point(1, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(305, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Copyrights 2021. All rights reserved By BA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 19);
            this.label4.TabIndex = 38;
            this.label4.Text = "Message Information";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(7, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 4);
            this.panel3.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.tblYesNoCancel);
            this.panel2.Controls.Add(this.tblYesNo);
            this.panel2.Controls.Add(this.tblOk);
            this.panel2.Controls.Add(this.tbMessage);
            this.panel2.Location = new System.Drawing.Point(12, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 278);
            this.panel2.TabIndex = 40;
            // 
            // tblYesNoCancel
            // 
            this.tblYesNoCancel.ColumnCount = 3;
            this.tblYesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNoCancel.Controls.Add(this.btnYesNoCancel_yes, 0, 0);
            this.tblYesNoCancel.Controls.Add(this.btnYesNoCancel_cancel, 2, 0);
            this.tblYesNoCancel.Controls.Add(this.btnYesNoCancel_No, 1, 0);
            this.tblYesNoCancel.Location = new System.Drawing.Point(3, 210);
            this.tblYesNoCancel.Name = "tblYesNoCancel";
            this.tblYesNoCancel.RowCount = 1;
            this.tblYesNoCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblYesNoCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblYesNoCancel.Size = new System.Drawing.Size(577, 62);
            this.tblYesNoCancel.TabIndex = 3;
            this.tblYesNoCancel.Visible = false;
            // 
            // btnYesNoCancel_yes
            // 
            this.btnYesNoCancel_yes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYesNoCancel_yes.Location = new System.Drawing.Point(3, 3);
            this.btnYesNoCancel_yes.Name = "btnYesNoCancel_yes";
            this.btnYesNoCancel_yes.Size = new System.Drawing.Size(186, 56);
            this.btnYesNoCancel_yes.TabIndex = 6;
            this.btnYesNoCancel_yes.Text = "Yes";
            this.btnYesNoCancel_yes.UseVisualStyleBackColor = true;
            this.btnYesNoCancel_yes.Click += new System.EventHandler(this.btnYesNoCancel_yes_Click);
            // 
            // btnYesNoCancel_cancel
            // 
            this.btnYesNoCancel_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYesNoCancel_cancel.Location = new System.Drawing.Point(387, 3);
            this.btnYesNoCancel_cancel.Name = "btnYesNoCancel_cancel";
            this.btnYesNoCancel_cancel.Size = new System.Drawing.Size(187, 56);
            this.btnYesNoCancel_cancel.TabIndex = 8;
            this.btnYesNoCancel_cancel.Text = "Cancel";
            this.btnYesNoCancel_cancel.UseVisualStyleBackColor = true;
            this.btnYesNoCancel_cancel.Click += new System.EventHandler(this.btnYesNoCancel_cancel_Click);
            // 
            // btnYesNoCancel_No
            // 
            this.btnYesNoCancel_No.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYesNoCancel_No.Location = new System.Drawing.Point(195, 3);
            this.btnYesNoCancel_No.Name = "btnYesNoCancel_No";
            this.btnYesNoCancel_No.Size = new System.Drawing.Size(186, 56);
            this.btnYesNoCancel_No.TabIndex = 7;
            this.btnYesNoCancel_No.Text = "No";
            this.btnYesNoCancel_No.UseVisualStyleBackColor = true;
            this.btnYesNoCancel_No.Click += new System.EventHandler(this.btnYesNoCancel_No_Click);
            // 
            // tblYesNo
            // 
            this.tblYesNo.ColumnCount = 3;
            this.tblYesNo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblYesNo.Controls.Add(this.btnYesNo_No, 2, 0);
            this.tblYesNo.Controls.Add(this.btnYesNo_yes, 1, 0);
            this.tblYesNo.Location = new System.Drawing.Point(6, 190);
            this.tblYesNo.Name = "tblYesNo";
            this.tblYesNo.RowCount = 1;
            this.tblYesNo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblYesNo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblYesNo.Size = new System.Drawing.Size(577, 62);
            this.tblYesNo.TabIndex = 2;
            this.tblYesNo.Visible = false;
            // 
            // btnYesNo_No
            // 
            this.btnYesNo_No.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYesNo_No.Location = new System.Drawing.Point(387, 3);
            this.btnYesNo_No.Name = "btnYesNo_No";
            this.btnYesNo_No.Size = new System.Drawing.Size(187, 56);
            this.btnYesNo_No.TabIndex = 8;
            this.btnYesNo_No.Text = "No";
            this.btnYesNo_No.UseVisualStyleBackColor = true;
            this.btnYesNo_No.Click += new System.EventHandler(this.btnYesNo_No_Click);
            // 
            // btnYesNo_yes
            // 
            this.btnYesNo_yes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYesNo_yes.Location = new System.Drawing.Point(195, 3);
            this.btnYesNo_yes.Name = "btnYesNo_yes";
            this.btnYesNo_yes.Size = new System.Drawing.Size(186, 56);
            this.btnYesNo_yes.TabIndex = 7;
            this.btnYesNo_yes.Text = "Yes";
            this.btnYesNo_yes.UseVisualStyleBackColor = true;
            this.btnYesNo_yes.Click += new System.EventHandler(this.btnYesNo_yes_Click);
            // 
            // tblOk
            // 
            this.tblOk.ColumnCount = 3;
            this.tblOk.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblOk.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblOk.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblOk.Controls.Add(this.btnOk, 1, 0);
            this.tblOk.Location = new System.Drawing.Point(4, 213);
            this.tblOk.Name = "tblOk";
            this.tblOk.RowCount = 1;
            this.tblOk.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOk.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblOk.Size = new System.Drawing.Size(577, 62);
            this.tblOk.TabIndex = 0;
            this.tblOk.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Location = new System.Drawing.Point(195, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(186, 56);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.BackColor = System.Drawing.Color.White;
            this.tbMessage.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessage.Location = new System.Drawing.Point(0, 0);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.Size = new System.Drawing.Size(584, 155);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.TabStop = false;
            this.tbMessage.Text = "ข้อความอะไรก็ว่าไป";
            // 
            // pcbIcon
            // 
            this.pcbIcon.Location = new System.Drawing.Point(16, 3);
            this.pcbIcon.Name = "pcbIcon";
            this.pcbIcon.Size = new System.Drawing.Size(55, 55);
            this.pcbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbIcon.TabIndex = 4;
            this.pcbIcon.TabStop = false;
            this.pcbIcon.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Modal_MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(682, 399);
            this.Controls.Add(this.pcbIcon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbHeader);
            this.Controls.Add(this.lbPOS);
            this.Controls.Add(this.pnClose);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Modal_MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modal_MsgBox";
            this.pnClose.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tblYesNoCancel.ResumeLayout(false);
            this.tblYesNo.ResumeLayout(false);
            this.tblOk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnClose;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbPOS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TableLayoutPanel tblOk;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tblYesNoCancel;
        private System.Windows.Forms.Button btnYesNoCancel_yes;
        private System.Windows.Forms.Button btnYesNoCancel_cancel;
        private System.Windows.Forms.Button btnYesNoCancel_No;
        private System.Windows.Forms.TableLayoutPanel tblYesNo;
        private System.Windows.Forms.Button btnYesNo_No;
        private System.Windows.Forms.Button btnYesNo_yes;
        private System.Windows.Forms.PictureBox pcbIcon;
    }
}