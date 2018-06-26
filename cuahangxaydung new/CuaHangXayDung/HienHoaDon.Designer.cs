namespace CuaHangXayDung
{
    partial class HienHoaDon
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet2 = new CuaHangXayDung.DataSet2();
            this.dataSet1 = new System.Data.DataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button_In = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_backward = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.DataSet2;
            // 
            // DataSet2
            // 
            this.DataSet2.DataSetName = "DataSet2";
            this.DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 73;
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.DataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CuaHangXayDung.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowParameterPrompts = false;
            this.reportViewer1.ShowRefreshButton = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.Size = new System.Drawing.Size(734, 429);
            this.reportViewer1.TabIndex = 0;
            // 
            // button_In
            // 
            this.button_In.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_In.Location = new System.Drawing.Point(0, 0);
            this.button_In.Name = "button_In";
            this.button_In.Size = new System.Drawing.Size(91, 124);
            this.button_In.TabIndex = 3;
            this.button_In.Text = "In Hóa Đơn";
            this.button_In.UseVisualStyleBackColor = true;
            this.button_In.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_forward);
            this.splitContainer1.Panel1.Controls.Add(this.btn_backward);
            this.splitContainer1.Panel1.Controls.Add(this.button_In);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(829, 429);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 2;
            // 
            // btn_backward
            // 
            this.btn_backward.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_backward.Location = new System.Drawing.Point(0, 124);
            this.btn_backward.Name = "btn_backward";
            this.btn_backward.Size = new System.Drawing.Size(91, 124);
            this.btn_backward.TabIndex = 1;
            this.btn_backward.Text = "Hóa đơn trước";
            this.btn_backward.UseVisualStyleBackColor = true;
            this.btn_backward.Click += new System.EventHandler(this.btn_backward_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_forward.Location = new System.Drawing.Point(0, 248);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(91, 146);
            this.btn_forward.TabIndex = 2;
            this.btn_forward.Text = "Hóa đơn sau";
            this.btn_forward.UseVisualStyleBackColor = true;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // HienHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 429);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HienHoaDon";
            this.Text = "Hiển thị hóa đơn";
            this.Load += new System.EventHandler(this.HienHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private DataSet2 DataSet2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button button_In;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.Button btn_backward;
    }
}