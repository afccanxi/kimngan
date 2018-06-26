using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuaHangXayDung
{
    public partial class ThongBaoHetHang : Form
    {
        string list_Hang_Show = "";
        public ThongBaoHetHang(string danhSachHang)
        {
            InitializeComponent();
            list_Hang_Show = danhSachHang;
        }

        private void ThongBaoHetHang_Load(object sender, EventArgs e)
        {
            richTbx_HetHang.Text = list_Hang_Show;
        }
    }
}
