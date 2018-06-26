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
    public partial class ThanhToan : Form
    {
        string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";

        
        String mahoadon = "";

        public ThanhToan(string a)
        {
            InitializeComponent();
            mahoadon = a;

            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd2;
            connection.Open();

            cmd2 = connection.CreateCommand();
            cmd2.CommandText = "SELECT HoTen,DienThoai,TongTien,TienDaTra,TienNoCu,ThanhTien FROM hoadon WHERE hoadon.MaHD = @MaHD";
            cmd2.Parameters.AddWithValue("@MaHD", mahoadon);

            // dataset hoadon
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd2);
            DataSet a2 = new DataSet();
            adap.Fill(a2, "DataTable1");

            string temp = a2.Tables[0].Rows[0][0].ToString();
            label1.Text = label1.Text + temp;
            label3.Text = label3.Text + a2.Tables[0].Rows[0][1].ToString();
            Int64 tongtien = Int64.Parse(a2.Tables[0].Rows[0][2].ToString(), NumberStyles.AllowThousands);
            Int64 tiendatra = Int64.Parse(a2.Tables[0].Rows[0][3].ToString(), NumberStyles.AllowThousands);
            Int64 tienNoCu = Int64.Parse(a2.Tables[0].Rows[0][4].ToString(), NumberStyles.AllowThousands);

            label2.Text = label2.Text + a2.Tables[0].Rows[0][5].ToString(); //thanh tien
            label4.Text = label4.Text + a2.Tables[0].Rows[0][3].ToString();// tien da tra
            label6.Text = label6.Text + a2.Tables[0].Rows[0][4].ToString();//lable tien no cu
            label7.Text = label7.Text + a2.Tables[0].Rows[0][2].ToString();// lable tong tien
            Int64 tienconlai = tongtien - tiendatra;
            label5.Text = label5.Text + tienconlai.ToString("#,##0");

        }
        //public ThanhToan(string ma)
        //{
        //    InitializeComponent();

        //    kh = k;
        //    mahoadon = ma;


        //    MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
        //    MySqlCommand cmd2;
        //    connection.Open();

        //    // lay ho ten dia chi khach hang
        //    cmd2 = connection.CreateCommand();
        //    cmd2.CommandText = "SELECT TongTien,TienDaTra FROM hoadon WHERE hoadon.MaHD = @MaHD";
        //    cmd2.Parameters.AddWithValue("@MaHD", mahoadon);

        //    // dataset hoadon
        //    MySqlDataAdapter adap = new MySqlDataAdapter(cmd2);
        //    DataSet a = new DataSet();
        //    adap.Fill(a, "DataTable1");


        //    label1.Text = label1.Text + kh.HoTen;
        //    label3.Text = label3.Text + kh.DienThoai;
        //    Int64 tongtien = Int64.Parse(a.Tables[0].Rows[0][0].ToString(), NumberStyles.AllowThousands);
        //    Int64 tiendatra = Int64.Parse(a.Tables[0].Rows[0][1].ToString(), NumberStyles.AllowThousands);
        //    label2.Text = label2.Text + tongtien.ToString("#,##0");
        //    label4.Text = label4.Text + tiendatra.ToString("#,##0");

        //    Int64 tienconlai = tongtien - tiendatra;
        //    label5.Text = label5.Text + tienconlai.ToString("#,##0");
        //}

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd,cmd2;
            connection.Open();
            //Lấy phân loại
            try
            {
                cmd2 = connection.CreateCommand();
                cmd2.CommandText = @"SELECT TongTien FROM hoadon WHERE MaHD = @MA";
                cmd2.Parameters.AddWithValue("@MA", mahoadon);

                string s = cmd2.ExecuteScalar().ToString();

                cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE hoadon SET TrangThai = @trangthai,TienDaTra = @tongtien WHERE MaHD = @MA";
                cmd.Parameters.AddWithValue("@MA", mahoadon);
                cmd.Parameters.AddWithValue("@tongtien", s);
                cmd.Parameters.AddWithValue("@trangthai", "Hoàn tất");
                cmd.ExecuteNonQuery();
                this.Close();

            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                //throw;
            }
            finally
            {

                connection.Close();
            }
            HienHoaDon f = new HienHoaDon(mahoadon);

            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TienTraTruoc f4 = new TienTraTruoc();
            var result =  f4.ShowDialog();
            Int64 tientratruoc = 0;

            if (result == DialogResult.OK)
            {
                tientratruoc = f4.ReturnValue1;
            }

            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd, cmd2;
            connection.Open();
            //Lấy phân loại
            try
            {
                cmd2 = connection.CreateCommand();
                cmd2.CommandText = @"SELECT TienDaTra FROM hoadon WHERE MaHD = @MA";
                cmd2.Parameters.AddWithValue("@MA", mahoadon);

                Int64 res = Int64.Parse(cmd2.ExecuteScalar().ToString(), NumberStyles.AllowThousands);
                

                cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE hoadon SET TrangThai = @trangthai,TienDaTra = @datra WHERE MaHD = @MA";
                cmd.Parameters.AddWithValue("@MA", mahoadon);
                cmd.Parameters.AddWithValue("@datra", (tientratruoc + res).ToString("#,##0"));
                cmd.Parameters.AddWithValue("@trangthai", "Nợ");
                cmd.ExecuteNonQuery();

            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                //throw;
            }
            finally
            {

                connection.Close();
            }

            HienHoaDon f = new HienHoaDon(mahoadon);
            f.ShowDialog();

            this.Close();
        }
    }
}
