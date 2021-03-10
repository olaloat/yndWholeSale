
namespace WholeSale.Forms
{
    partial class FrmTestPrint
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.productIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.createByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.documentLineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentLineBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(636, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productIdDataGridViewTextBoxColumn,
            this.productNameDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.createByDataGridViewTextBoxColumn,
            this.createTimeDataGridViewTextBoxColumn,
            this.editByDataGridViewTextBoxColumn,
            this.editTimeDataGridViewTextBoxColumn,
            this.compCodeDataGridViewTextBoxColumn,
            this.branchCodeDataGridViewTextBoxColumn,
            this.transactionsDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.productBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(607, 371);
            this.dataGridView1.TabIndex = 1;
            // 
            // productIdDataGridViewTextBoxColumn
            // 
            this.productIdDataGridViewTextBoxColumn.DataPropertyName = "productId";
            this.productIdDataGridViewTextBoxColumn.HeaderText = "productId";
            this.productIdDataGridViewTextBoxColumn.Name = "productIdDataGridViewTextBoxColumn";
            // 
            // productNameDataGridViewTextBoxColumn
            // 
            this.productNameDataGridViewTextBoxColumn.DataPropertyName = "productName";
            this.productNameDataGridViewTextBoxColumn.HeaderText = "productName";
            this.productNameDataGridViewTextBoxColumn.Name = "productNameDataGridViewTextBoxColumn";
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "isActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "isActive";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            // 
            // createByDataGridViewTextBoxColumn
            // 
            this.createByDataGridViewTextBoxColumn.DataPropertyName = "createBy";
            this.createByDataGridViewTextBoxColumn.HeaderText = "createBy";
            this.createByDataGridViewTextBoxColumn.Name = "createByDataGridViewTextBoxColumn";
            // 
            // createTimeDataGridViewTextBoxColumn
            // 
            this.createTimeDataGridViewTextBoxColumn.DataPropertyName = "createTime";
            this.createTimeDataGridViewTextBoxColumn.HeaderText = "createTime";
            this.createTimeDataGridViewTextBoxColumn.Name = "createTimeDataGridViewTextBoxColumn";
            // 
            // editByDataGridViewTextBoxColumn
            // 
            this.editByDataGridViewTextBoxColumn.DataPropertyName = "editBy";
            this.editByDataGridViewTextBoxColumn.HeaderText = "editBy";
            this.editByDataGridViewTextBoxColumn.Name = "editByDataGridViewTextBoxColumn";
            // 
            // editTimeDataGridViewTextBoxColumn
            // 
            this.editTimeDataGridViewTextBoxColumn.DataPropertyName = "editTime";
            this.editTimeDataGridViewTextBoxColumn.HeaderText = "editTime";
            this.editTimeDataGridViewTextBoxColumn.Name = "editTimeDataGridViewTextBoxColumn";
            // 
            // compCodeDataGridViewTextBoxColumn
            // 
            this.compCodeDataGridViewTextBoxColumn.DataPropertyName = "compCode";
            this.compCodeDataGridViewTextBoxColumn.HeaderText = "compCode";
            this.compCodeDataGridViewTextBoxColumn.Name = "compCodeDataGridViewTextBoxColumn";
            // 
            // branchCodeDataGridViewTextBoxColumn
            // 
            this.branchCodeDataGridViewTextBoxColumn.DataPropertyName = "branchCode";
            this.branchCodeDataGridViewTextBoxColumn.HeaderText = "branchCode";
            this.branchCodeDataGridViewTextBoxColumn.Name = "branchCodeDataGridViewTextBoxColumn";
            // 
            // transactionsDataGridViewTextBoxColumn
            // 
            this.transactionsDataGridViewTextBoxColumn.DataPropertyName = "Transactions";
            this.transactionsDataGridViewTextBoxColumn.HeaderText = "Transactions";
            this.transactionsDataGridViewTextBoxColumn.Name = "transactionsDataGridViewTextBoxColumn";
            // 
            // productBindingSource
            // 
            this.productBindingSource.DataSource = typeof(WholeSale.Product);
            // 
            // documentLineBindingSource
            // 
            this.documentLineBindingSource.DataSource = typeof(WholeSale.DocumentLine);
            // 
            // FrmTestPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "FrmTestPrint";
            this.Text = "FrmTestPrint";
            this.Load += new System.EventHandler(this.FrmTestPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentLineBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn editByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn editTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn compCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactionsDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource documentLineBindingSource;
        private System.Windows.Forms.BindingSource productBindingSource;
    }
}