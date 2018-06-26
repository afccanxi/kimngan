using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuaHangXayDung
{
    public partial class TienTraTruoc : Form
    {
        public Int64 ReturnValue1 { get; set; }
        string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";
        Int64 tientong = 0, tientratruoc = 0;
        public TienTraTruoc()
        {

            InitializeComponent();
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd2;
            connection.Open();

            cmd2 = connection.CreateCommand();
            cmd2.CommandText = "SELECT TongTien,TienDaTra FROM hoadon WHERE hoadon.MaHD = @MaHD ";
            cmd2.Parameters.AddWithValue("@MaHD", truyendulieu.mahoadon_temp);

            MySqlDataAdapter adap = new MySqlDataAdapter(cmd2);
            DataSet a = new DataSet();
            adap.Fill(a);

            tientong = Int64.Parse(a.Tables[0].Rows[0][0].ToString(),NumberStyles.AllowThousands );
            tientratruoc = Int64.Parse(a.Tables[0].Rows[0][1].ToString(), NumberStyles.AllowThousands);
            label3.Text += tientong.ToString("#,##0");
            label2.Text += tientratruoc.ToString("#,##0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ReturnValue1 = Int64.Parse(textBox1.Text);
            if(ReturnValue1 > (tientong - tientratruoc))
            {
                MessageBox.Show("Số tiền bạn nhập không đúng");
            }
            else{
            this.DialogResult = DialogResult.OK;
            this.Close();
            }
        }

    }
}
