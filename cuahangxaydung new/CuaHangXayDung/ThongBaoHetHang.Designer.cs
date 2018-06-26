namespace CuaHangXayDung
{
    partial class ThongBaoHetHang
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
            this.richTbx_HetHang = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTbx_HetHang
            // 
            this.richTbx_HetHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTbx_HetHang.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTbx_HetHang.Location = new System.Drawing.Point(0, 0);
            this.richTbx_HetHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTbx_HetHang.Name = "richTbx_HetHang";
            this.richTbx_HetHang.Size = new System.Drawing.Size(535, 347);
            this.richTbx_HetHang.TabIndex = 0;
            this.richTbx_HetHang.Text = "";
            // 
            // ThongBaoHetHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 347);
            this.Controls.Add(this.richTbx_HetHang);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ThongBaoHetHang";
            this.Text = "Các mặt hàng sắp hết";
            this.Load += new System.EventHandler(this.ThongBaoHetHang_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTbx_HetHang;
    }
}