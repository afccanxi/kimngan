namespace CuaHangXayDung
{
    partial class NhaCC_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_MaNhaCC = new System.Windows.Forms.TextBox();
            this.tbx_TenNhaCC = new System.Windows.Forms.TextBox();
            this.tbx_DiaChiNhaCC = new System.Windows.Forms.TextBox();
            this.tbx_SDT = new System.Windows.Forms.TextBox();
            this.btn_CapNhat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Nhà CC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên Nhà CC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Địa Chỉ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số ĐT";
            // 
            // tbx_MaNhaCC
            // 
            this.tbx_MaNhaCC.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_MaNhaCC.Location = new System.Drawing.Point(165, 47);
            this.tbx_MaNhaCC.Name = "tbx_MaNhaCC";
            this.tbx_MaNhaCC.Size = new System.Drawing.Size(231, 27);
            this.tbx_MaNhaCC.TabIndex = 4;
            // 
            // tbx_TenNhaCC
            // 
            this.tbx_TenNhaCC.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_TenNhaCC.Location = new System.Drawing.Point(165, 78);
            this.tbx_TenNhaCC.Name = "tbx_TenNhaCC";
            this.tbx_TenNhaCC.Size = new System.Drawing.Size(231, 27);
            this.tbx_TenNhaCC.TabIndex = 5;
            // 
            // tbx_DiaChiNhaCC
            // 
            this.tbx_DiaChiNhaCC.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_DiaChiNhaCC.Location = new System.Drawing.Point(165, 109);
            this.tbx_DiaChiNhaCC.Name = "tbx_DiaChiNhaCC";
            this.tbx_DiaChiNhaCC.Size = new System.Drawing.Size(231, 27);
            this.tbx_DiaChiNhaCC.TabIndex = 6;
            // 
            // tbx_SDT
            // 
            this.tbx_SDT.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_SDT.Location = new System.Drawing.Point(165, 140);
            this.tbx_SDT.Name = "tbx_SDT";
            this.tbx_SDT.Size = new System.Drawing.Size(231, 27);
            this.tbx_SDT.TabIndex = 7;
            // 
            // btn_CapNhat
            // 
            this.btn_CapNhat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CapNhat.Location = new System.Drawing.Point(181, 212);
            this.btn_CapNhat.Name = "btn_CapNhat";
            this.btn_CapNhat.Size = new System.Drawing.Size(114, 36);
            this.btn_CapNhat.TabIndex = 8;
            this.btn_CapNhat.Text = "Cập nhập";
            this.btn_CapNhat.UseVisualStyleBackColor = true;
            this.btn_CapNhat.Click += new System.EventHandler(this.button1_Click);
            // 
            // NhaCC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 285);
            this.Controls.Add(this.btn_CapNhat);
            this.Controls.Add(this.tbx_SDT);
            this.Controls.Add(this.tbx_DiaChiNhaCC);
            this.Controls.Add(this.tbx_TenNhaCC);
            this.Controls.Add(this.tbx_MaNhaCC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NhaCC_Form";
            this.Text = "Nhà Cung Cấp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_MaNhaCC;
        private System.Windows.Forms.TextBox tbx_TenNhaCC;
        private System.Windows.Forms.TextBox tbx_DiaChiNhaCC;
        private System.Windows.Forms.TextBox tbx_SDT;
        private System.Windows.Forms.Button btn_CapNhat;
    }
}