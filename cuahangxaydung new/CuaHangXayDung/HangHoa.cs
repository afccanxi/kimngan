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
    public partial class HangHoa : Form
    {
        string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";
        List<LoaiSanPham> list_LoaiSanPham;

        public HangHoa()
        {
            InitializeComponent();
            list_LoaiSanPham = new List<LoaiSanPham>();


            Load_Loai_SanPham();

            if (Form1.id_update_hanghoa != -1)
            {
                Load_SanPham();
                button1.Text = "Cập nhật";
            }
            else
                button1.Text = "Thêm";

            Load_textbox_nhaCC();
        }

        private void Load_textbox_nhaCC()
        {
            tbx_NhaCC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbx_NhaCC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbx_NhaCC.AutoCompleteCustomSource = Form1.auto2;
        }
        private void Load_Loai_SanPham()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd;
            connection.Open();
            //Lấy phân loại
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM loai";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                //Load loại sản phẩm
                comboBox1.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LoaiSanPham tempLoaiSanPham = new LoaiSanPham(Convert.ToInt32(ds.Tables[0].Rows[i][0]), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    list_LoaiSanPham.Add(tempLoaiSanPham);

                    ComboboxItem item = new ComboboxItem();
                    item.Text = ds.Tables[0].Rows[i][2].ToString();
                   // item.Value = ds.Tables[0].Rows[i][1].ToString();
                    comboBox1.Items.Add(item);
                }
                comboBox1.SelectedIndex = 0;
                //--------------------
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Load_SanPham()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd;
            connection.Open();
            //Lấy phân loại
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM hanghoa WHERE ID = @ID AND Del = 1";
                cmd.Parameters.AddWithValue("@ID", Form1.id_update_hanghoa);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                comboBox1.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox1.Text = ds.Tables[0].Rows[0][1].ToString(); //ma loai
                textBox2.Text = ds.Tables[0].Rows[0][2].ToString(); // ten hang
                textBox3.Text = ds.Tables[0].Rows[0][5].ToString();// gia nhap
                textBox4.Text = ds.Tables[0].Rows[0][4].ToString(); // gia ban
                textBox5.Text = ds.Tables[0].Rows[0][3].ToString(); // don vi tinh
                textBox6.Text = ds.Tables[0].Rows[0][9].ToString(); // so luong
                textBox7.Text = ds.Tables[0].Rows[0][8].ToString(); // so luong
                textBox8.Text = ds.Tables[0].Rows[0][11].ToString(); // so luong                //--------------------
                tbx_NhaCC.Text = ds.Tables[0].Rows[0][12].ToString(); // so luong   
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
            
            string loai = comboBox1.Text;
            bool check = false;
            int count = list_LoaiSanPham.Count;
            for (int i = 0; i < count; i++)
            {
                if(loai == list_LoaiSanPham[i].Ten)
                {
                    loai = list_LoaiSanPham[i].MaLoai;
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Mã loại không tồn tại.");
                return;
            }
            string mahang = textBox1.Text;
            string tenhang = textBox2.Text;
            string soluong = textBox7.Text;
            string gianhap = "";
            if(textBox3.Text != "")
                gianhap = Int64.Parse(textBox3.Text, NumberStyles.AllowThousands).ToString("#,##0");
            string giaban = (Int64.Parse(textBox4.Text, NumberStyles.AllowThousands).ToString("#,##0"));
            string donvitinh = textBox5.Text;
           
            if (loai == "" || mahang == "" || tenhang == "" || giaban == "" || donvitinh == "" || soluong == "")
            {
                MessageBox.Show("Không được để trống.");
                return;
            }

            //tinh toan so luong 
            string[] substr = soluong.Split(new char[] { '+', '-', '*', '/' });

            double sum = 0;

            if (substr.Length > 2)
            {
                MessageBox.Show("Chỉ có thể tính với 2 số hạng");
                return;
            }
            else if (substr.Length == 2)
            {
                if (soluong.Contains('+'))
                {
                    sum = Double.Parse(substr[0]) + Double.Parse(substr[1]);
                }
                else if (soluong.Contains('-'))
                {
                    sum = Double.Parse(substr[0]) - Double.Parse(substr[1]);
                }
                else if (soluong.Contains('*'))
                {
                    sum = Double.Parse(substr[0]) * Double.Parse(substr[1]);
                }
                else
                {
                    sum = Double.Parse(substr[0]) / Double.Parse(substr[1]);
                }
            }
            else { sum = Double.Parse(substr[0]); }
            MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
            MySqlCommand cmd;
            connection.Open();
            string MaNhaCC = tbx_NhaCC.Text;
            cmd = connection.CreateCommand();
            //Lấy phân loại
            try
            {
                int coun = Form1.list_Show_NhaCC.Count;
                for (int i = 0; i < coun; i++)
                    if (Form1.list_Show_NhaCC[i].TenNhaCC.Equals(tbx_NhaCC.Text))
                    {
                        MaNhaCC = Form1.list_Show_NhaCC[i].MaNhaCC;
                        break;
                    }
                
                //cmd.CommandText = "SELECT MaHang, Ten, Gia, Hinh FROM sanpham"; 
                if (Form1.id_update_hanghoa == -1)
                {
                    cmd.CommandText = @"INSERT INTO hanghoa(ID, MaHang, TenHang, DonViTinh, SoLuongTon, GiaNhap, GiaBan,MaLoai, Del, Kho, MaNhaCC, GhiChu, ChietKhau)
                VALUES(@ID, @MaHang, @TenHang, @DonViTinh, @SoLuong, @GiaNhap, @GiaBan, @MaLoai, @Del, @Kho, @MaNhaCC, @GhiChu, @ChietKhau)";
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@MaHang", mahang);
                    cmd.Parameters.AddWithValue("@TenHang", tenhang);
                    cmd.Parameters.AddWithValue("@DonViTinh", donvitinh);
                    cmd.Parameters.AddWithValue("@SoLuong", sum.ToString());
                    cmd.Parameters.AddWithValue("@GiaNhap", gianhap);
                    cmd.Parameters.AddWithValue("@GiaBan", giaban);
                    cmd.Parameters.AddWithValue("@MaLoai", loai);
                    cmd.Parameters.AddWithValue("@Del", "1");
                    cmd.Parameters.AddWithValue("@Kho", textBox6.Text);
                    cmd.Parameters.AddWithValue("@MaNhaCC", MaNhaCC);
                    cmd.Parameters.AddWithValue("@GhiChu", DateTime.Today.ToString());
                    cmd.Parameters.AddWithValue("@ChietKhau", textBox8.Text);
                }
                else
                {
                    cmd.CommandText = @"UPDATE hanghoa SET TenHang = @TenHang, DonViTinh = @DonViTinh, SoLuongTon = @SoLuong, ChietKhau = @ChietKhau,
                                    GiaNhap = @GiaNhap, GiaBan = @GiaBan, Kho = @Kho, MaNhaCC = @MaNhaCC, GhiChu = @GhiChu WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", Form1.id_update_hanghoa);
                    //cmd.Parameters.AddWithValue("@MaHang", mahang);
                    cmd.Parameters.AddWithValue("@TenHang", tenhang);
                    cmd.Parameters.AddWithValue("@DonViTinh", donvitinh);
                    cmd.Parameters.AddWithValue("@SoLuong", sum.ToString());
                    cmd.Parameters.AddWithValue("@GiaNhap", gianhap);
                    cmd.Parameters.AddWithValue("@GiaBan", giaban);
                    //cmd.Parameters.AddWithValue("@MaLoai", loai);
                    cmd.Parameters.AddWithValue("@Kho", textBox6.Text);
                    cmd.Parameters.AddWithValue("@MaNhaCC", MaNhaCC);
                    cmd.Parameters.AddWithValue("@GhiChu", DateTime.Today.ToString());
                    cmd.Parameters.AddWithValue("@ChietKhau", textBox8.Text);
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
                else
                {
                    DialogResult re = MessageBox.Show("Mã hàng đã từng tồn tại, bạn có muốn khôi phục", "Cảnh báo", MessageBoxButtons.YesNo);
                    if (re == DialogResult.Yes)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE hanghoa SET TenHang = @TenHang, DonViTinh = @DonViTinh, SoLuongTon = @SoLuong, ChietKhau = @ChietKhau,
                                    GiaNhap = @GiaNhap, GiaBan = @GiaBan, Kho = @Kho, MaNhaCC = @MaNhaCC, GhiChu = @GhiChu, Del = 1 WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", Form1.id_update_hanghoa);
                        //cmd.Parameters.AddWithValue("@MaHang", mahang);
                        cmd.Parameters.AddWithValue("@TenHang", tenhang);
                        cmd.Parameters.AddWithValue("@DonViTinh", donvitinh);
                        cmd.Parameters.AddWithValue("@SoLuong", sum.ToString());
                        cmd.Parameters.AddWithValue("@GiaNhap", gianhap);
                        cmd.Parameters.AddWithValue("@GiaBan", giaban);
                        //cmd.Parameters.AddWithValue("@MaLoai", loai);
                        cmd.Parameters.AddWithValue("@Kho", textBox6.Text);
                        cmd.Parameters.AddWithValue("@MaNhaCC", MaNhaCC);
                        cmd.Parameters.AddWithValue("@GhiChu", DateTime.Today.ToString());
                        cmd.Parameters.AddWithValue("@ChietKhau", textBox8.Text);
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int64 temp;
                if (Int64.TryParse(textBox3.Text, NumberStyles.AllowThousands, null, out temp) == false)
                {
                    textBox3.Text = "0";
                    return;
                } 
            }
            catch (Exception c)
            { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int64 temp;
                if (Int64.TryParse(textBox4.Text, NumberStyles.AllowThousands, null, out temp) == false)
                {
                    textBox4.Text = "0";
                    return;
                }
            }
            catch (Exception c)
            { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
