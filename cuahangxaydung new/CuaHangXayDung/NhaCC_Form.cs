using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CuaHangXayDung
{
    public partial class NhaCC_Form : Form
    {
        string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";

        public NhaCC_Form()
        {
            InitializeComponent();

            if (Form1.id_update_NhaCC != -1)
            {
                btn_CapNhat.Text = "Cập nhật";
                Load_NhaCC();
            }
            else
                btn_CapNhat.Text = "Thêm";

        }

        private void Load_NhaCC()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd;
            connection.Open();
            //Lấy phân loại
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM nhacc WHERE ID = @ID AND Del = 1";
                cmd.Parameters.AddWithValue("@ID", Form1.id_update_NhaCC);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                tbx_MaNhaCC.Text = ds.Tables[0].Rows[0][1].ToString(); //ma loai
                tbx_TenNhaCC.Text = ds.Tables[0].Rows[0][2].ToString(); //ma loai
                tbx_DiaChiNhaCC.Text = ds.Tables[0].Rows[0][4].ToString(); //ma loai
                tbx_SDT.Text = ds.Tables[0].Rows[0][5].ToString(); //ma loai
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                this.Close();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maNhaCC = tbx_MaNhaCC.Text;
            string tenNhaCC = tbx_TenNhaCC.Text;
            string diaChiNhaCC = tbx_DiaChiNhaCC.Text;
            string SoDT = tbx_SDT.Text;

            if (maNhaCC == "" || tenNhaCC == "")
            {
                MessageBox.Show("Mã nhà CC và Tên nhà CC không được để trống.");
                return;
            }

            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd;
            cmd = connection.CreateCommand();
            connection.Open();
            //Lấy phân loại
            try
            {
                //cmd.CommandText = "SELECT MaHang, Ten, Gia, Hinh FROM sanpham"; 
                if (Form1.id_update_NhaCC == -1)
                {
                    cmd.CommandText = @"INSERT INTO nhacc(ID, MaNhaCC, TenNhaCC, DiaChi, DienThoai, del)
                VALUES(@ID, @MaNhaCC, @TenNhaCC, @DiaChi, @DienThoai, @del)";
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@MaNhaCC", maNhaCC);
                    cmd.Parameters.AddWithValue("@TenNhaCC", tenNhaCC);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChiNhaCC);
                    cmd.Parameters.AddWithValue("@DienThoai", SoDT);
                    cmd.Parameters.AddWithValue("@Del", "1");
                }
                else
                {
                    cmd.CommandText = @"UPDATE nhacc SET TenNhaCC = @TenNhaCC, DiaChi = @DiaChi, DienThoai = @DienThoai WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", Form1.id_update_NhaCC);
                    cmd.Parameters.AddWithValue("@TenNhaCC", tenNhaCC);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChiNhaCC);
                    cmd.Parameters.AddWithValue("@DienThoai", SoDT);
                }


                cmd.ExecuteNonQuery();
            }
            catch (Exception v)
            {
                if (v.Message.ToString().IndexOf("TenHang") > 0)
                    MessageBox.Show("Tên hàng bị trùng");
                if (v.Message.ToString().IndexOf("MaHang") > 0)
                    MessageBox.Show("Mã hàng bị trùng");
                if (v.Message.ToString().IndexOf("MaLoai") > 0)
                    MessageBox.Show("Loại hàng bị lỗi");
                else{
                     DialogResult re = MessageBox.Show("Nhà cung cấp đã từng tồn tại, bạn có muốn khôi phục", "Cảnh báo", MessageBoxButtons.YesNo);
                     if (re == DialogResult.Yes)
                     {
                         cmd.Parameters.Clear();
                         cmd.CommandText = @"UPDATE nhacc SET Del = 1, TenNhaCC = @TenNhaCC, DiaChi = @DiaChi, DienThoai = @DienThoai WHERE MaNhaCC = @MaNhaCC";
                         cmd.Parameters.AddWithValue("@MaNhaCC", maNhaCC);
                         cmd.Parameters.AddWithValue("@TenNhaCC", tenNhaCC);
                         cmd.Parameters.AddWithValue("@DiaChi", diaChiNhaCC);
                         cmd.Parameters.AddWithValue("@DienThoai", SoDT);
                         cmd.ExecuteNonQuery();
                     }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    this.Close();
                    connection.Close();
                }
            }
            

        }
    }
}
