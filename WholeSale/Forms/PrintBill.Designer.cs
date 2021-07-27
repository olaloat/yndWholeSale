
namespace WholeSale.Forms
{
    partial class PrintBill
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DocumentLineDisplayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DocumentDisplayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentLineDisplayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDisplayBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "dsList";
            reportDataSource1.Value = this.DocumentLineDisplayBindingSource;
            reportDataSource2.Name = "dsDocList";
            reportDataSource2.Value = this.BillBindingSource;
            reportDataSource3.Name = "dsHeader";
            reportDataSource3.Value = this.DocumentDisplayBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WholeSale.report.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(177, 750);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
            // 
            // DocumentLineDisplayBindingSource
            // 
            this.DocumentLineDisplayBindingSource.DataMember = "Transactions";
            this.DocumentLineDisplayBindingSource.DataSource = typeof(WholeSale.DocumentLineDisplay);
            this.DocumentLineDisplayBindingSource.CurrentChanged += new System.EventHandler(this.DocumentLineDisplayBindingSource_CurrentChanged);
            // 
            // BillBindingSource
            // 
            this.BillBindingSource.DataSource = typeof(WholeSale.Bill);
            this.BillBindingSource.CurrentChanged += new System.EventHandler(this.BillBindingSource_CurrentChanged);
            // 
            // DocumentDisplayBindingSource
            // 
            this.DocumentDisplayBindingSource.DataSource = typeof(WholeSale.DocumentDisplay);
            // 
            // PrintBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 750);
            this.Controls.Add(this.reportViewer1);
            this.Name = "PrintBill";
            this.Text = "PrintBill";
            this.Load += new System.EventHandler(this.PrintBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentLineDisplayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDisplayBindingSource)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource BillBindingSource;
        private System.Windows.Forms.BindingSource DocumentLineDisplayBindingSource;
        private System.Windows.Forms.BindingSource DocumentDisplayBindingSource;
    }
}