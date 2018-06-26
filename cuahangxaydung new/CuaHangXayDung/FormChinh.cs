using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;


namespace CuaHangXayDung
{
    public partial class Form1 : Form
    {
        List<LoaiSanPham> list_LoaiSanPham;
        List<SanPham> list_Show_SanPham_BanHang;

        static public List<NhaCC> list_Show_NhaCC;
        //List<KhachHang> list_Show_KhachHang_BanHang;

        int row = 0;
        string text_name = "";
        string mahoadon_cu_xoa = "";
        Int64 TienDaTraLanTruoc = 0;
        string TenNhaCC_double_Click = "";

        public string mahoadon_quanly = "";
        static public int id_update_hanghoa;
        static public int id_update_NhaCC;
        static public bool b_BanHang = true;

        int listBox_NhaCC_select_index = 0;
        private bool tabPage2_hadLoad = false;
        //int id_makhachhang;

        //bool checksoluong = true;
        string id_MaLoai_Combobox_Show_SanPham;
        static public AutoCompleteStringCollection auto2 = new AutoCompleteStringCollection();

        static string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(Keys key);
        MySqlConnection connection = new MySqlConnection(MyConnectionMySQL);
        MySqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
            connection.Open();
            cmd = connection.CreateCommand();
            //import_table();

            radioBtn_BanHang.Checked = true;
            //
            list_LoaiSanPham = new List<LoaiSanPham>();
            list_Show_SanPham_BanHang = new List<SanPham>();

            list_Show_NhaCC = new List<NhaCC>();
            //list_Show_KhachHang_BanHang = new List<KhachHang>();
            id_MaLoai_Combobox_Show_SanPham = "TatCa";

            textBox_TenKhachHang.Text = "";

            //load loại sản phẩm
            Load_Loai_SanPham();
            //Load_Show_SanPham("");

            //Load_Show_HangHoa("");
            comboBox_TrangThai_DoanhThu.SelectedIndex = 0;

            //load bàn
            dateTimePicker_TuNgay.Format = DateTimePickerFormat.Custom;
            dateTimePicker_TuNgay.CustomFormat = "dd-MM-yyyy";
            dateTimePicker_DenNgay.Format = DateTimePickerFormat.Custom;
            dateTimePicker_DenNgay.CustomFormat = "dd-MM-yyyy";

            if (DateTime.Now.Month - 3 <= 0)
            {
                DateTime date = new DateTime(DateTime.Now.Year - 1, 10, DateTime.Now.Day);
                dateTimePicker_TuNgay.Value = date;
            }
            else
            {
                int month = DateTime.Now.Month - 3;
                int dayy = DateTime.Now.Day;
                if (month == 2)
                    dayy = 27;
                DateTime date = new DateTime(DateTime.Now.Year, month, dayy);
                dateTimePicker_TuNgay.Value = date;
            }

            Khoi_Tao_Bien();
            Load_NhaCungCap();
            LoadDataToCollection();
        }

        private void import_table()
        {
            int idd = 1;
            string maloai = "TatCa";
            string teen = "Tất cả";

            //// import loai
            //cmd.Parameters.Clear();
            //cmd.CommandText = "INSERT INTO loai(ID, MaLoai, ten) VALUES(@id, @MaLoai, @ten)";
            //cmd.Parameters.AddWithValue("@ID", idd);
            //cmd.Parameters.AddWithValue("@MaLoai", maloai);
            //cmd.Parameters.AddWithValue("@ten", teen);
            //cmd.ExecuteNonQuery();

            //// import hanghoa
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\USB\\Desktop\\123.txt");
            int i =2000;
            while ((line = file.ReadLine()) != null)
            {

                string[] arr = line.Split('\t');
                if (arr[0] == "" || arr[1] == "" || arr[2] == "")
                    continue;
                if (arr[3] == "" || arr[3] == "\t")
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO hanghoa(ID, MaHang, TenHang, DonViTinh, GiaBan, MaLoai, SoLuongTon) VALUES(@ID, @MaHang, @TenHang, @DonViTinh, @GiaBan, @MaLoai,@SoLuongTon)";
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@MaHang", "Tatca_"+ i.ToString());
                    cmd.Parameters.AddWithValue("@TenHang", arr[0]);
                    cmd.Parameters.AddWithValue("@DonViTinh", arr[1]);
                    //cmd.Parameters.AddWithValue("@GiaNhap", Int64.Parse(arr[3]).ToString("#,##0"));
                    cmd.Parameters.AddWithValue("@GiaBan", Int64.Parse(arr[2]).ToString("#,##0"));
                    cmd.Parameters.AddWithValue("@MaLoai", maloai);
                    cmd.Parameters.AddWithValue("@SoLuongTon", 1000);

                    cmd.ExecuteNonQuery();
                }
                else if (arr[3] != "")
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO hanghoa(ID, MaHang, TenHang, DonViTinh, GiaNhap, GiaBan ,MaLoai, SoLuongTon) VALUES(@ID, @MaHang, @TenHang, @DonViTinh, @GiaNhap,@GiaBan, @MaLoai,@SoLuongTon)";
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@MaHang", "Tatca_" + i.ToString());
                    cmd.Parameters.AddWithValue("@TenHang", arr[0]);
                    cmd.Parameters.AddWithValue("@DonViTinh", arr[1]);
                    cmd.Parameters.AddWithValue("@GiaNhap", Int64.Parse(arr[3]).ToString("#,##0"));
                    cmd.Parameters.AddWithValue("@GiaBan", Int64.Parse(arr[2]).ToString("#,##0"));
                    cmd.Parameters.AddWithValue("@MaLoai", maloai);
                    cmd.Parameters.AddWithValue("@SoLuongTon", 1000);

                    cmd.ExecuteNonQuery();
                }
                i++;
            }

            file.Close();
        }

        private void Khoi_Tao_Bien()
        {
            textBox_DiaChi_KhachHang.Text = "";
            textBox_SDT_KhachHang.Text = "";
            textBox_TenKhachHang.Text = "";
            //textBox_CMND_KhachHang.Text = "";
            label_tongcong.Text = "0";
            label_thanhtien.Text = "0";
            textBox_tratruoc.Text = "0";
            label_DaTraLanTruoc.Text = "";
            tbx_NoCu.Text = "0";
            radioButton_trahet.Checked = true;
            textBox_tratruoc.Enabled = false;
            truyendulieu.mahoadon_temp = "";
            truyendulieu.nocu_temp = "0";
            TienDaTraLanTruoc = 0;
        }
        private void Load_Loai_SanPham()
        {

            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM loai";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                //Load loại sản phẩm

                ComboboxItem item = new ComboboxItem();
                //item.Text = "Tất Cả";
                //comboBox_LoaiHang_BanHang.Items.Add(item);
                //comboBox_LoaiHangHoa_Quanly.Items.Add(item);

                //LoaiSanPham tempLoaiSanPham = new LoaiSanPham(9999999, "TatCa", "Tất Cả");
                //list_LoaiSanPham.Add(tempLoaiSanPham);
                // dad
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    int ID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string MaLoai = ds.Tables[0].Rows[i][1].ToString();
                    string TenLoai = ds.Tables[0].Rows[i][2].ToString();
                    LoaiSanPham tempLoaiSanPham = new LoaiSanPham(ID, MaLoai, TenLoai);
                    list_LoaiSanPham.Add(tempLoaiSanPham);

                    item = new ComboboxItem();
                    item.Text = TenLoai;

                    //item.Value = ds.Tables[0].Rows[i][1].ToString();
                    comboBox_LoaiHang_BanHang.Items.Add(item);
                    comboBox_LoaiHangHoa_Quanly.Items.Add(item);
                }
                comboBox_LoaiHang_BanHang.SelectedIndex = 0;
                comboBox_LoaiHangHoa_Quanly.SelectedIndex = 0;
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }

        private void Load_NhaCungCap()
        {
            list_Show_NhaCC.Clear();
            listBox_NhaCC.Items.Clear();

            cmd.Parameters.Clear();
            cmd.CommandText = "Select * from NhaCC where del = 1";

            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds);

            int count = ds.Tables[0].Rows.Count;
            NhaCC temp;
            for (int i = 0; i < count; i++)
            {
                int ID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                string MaNhaCC = ds.Tables[0].Rows[i][1].ToString();
                string TenNhaCC = ds.Tables[0].Rows[i][2].ToString();
                string diaChi = ds.Tables[0].Rows[i][4].ToString();
                string dienThoai = ds.Tables[0].Rows[i][5].ToString(); 
                int del = Convert.ToInt32(ds.Tables[0].Rows[i][3]);

                temp = new NhaCC(ID, MaNhaCC, TenNhaCC, diaChi, dienThoai, del);

                list_Show_NhaCC.Add(temp);
                listBox_NhaCC.Items.Add(temp.TenNhaCC);
            }
        }

        private void Load_Show_SanPham(string string_search_ten)
        {
            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                if (id_MaLoai_Combobox_Show_SanPham == "TatCa")
                    cmd.CommandText = "SELECT * FROM hanghoa WHERE del = 1";
                else
                {
                    cmd.CommandText = "SELECT * FROM hanghoa WHERE del = 1 AND MaLoai = @MaLoai";
                    cmd.Parameters.AddWithValue("@MaLoai", id_MaLoai_Combobox_Show_SanPham);
                }
                if (string_search_ten.Length > 0)
                    cmd.CommandText = cmd.CommandText + " AND TenHang LIKE \'%" + string_search_ten + "%\'";


                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                //Load loại sản phẩm
                listBox_Show_BanHang.Items.Clear();
                list_Show_SanPham_BanHang.Clear();

                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    int ID =  Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string Masp = ds.Tables[0].Rows[i][1].ToString();
                        string TenSP = ds.Tables[0].Rows[i][2].ToString();
                            string DVT =  ds.Tables[0].Rows[i][3].ToString();
                                string giaNhap = ds.Tables[0].Rows[i][5].ToString();
                                    string giaBan = ds.Tables[0].Rows[i][4].ToString();
                                        string maLoai = ds.Tables[0].Rows[i][6].ToString(); 
                    int Del = Convert.ToInt32(ds.Tables[0].Rows[i][7]);
                    double soLuongTon = Convert.ToDouble(ds.Tables[0].Rows[i][8]); 
                    string chietKhau = ds.Tables[0].Rows[i][11].ToString();
                        string maNCC = ds.Tables[0].Rows[i][12].ToString();
                        SanPham tempSanPham = new SanPham(ID, Masp, TenSP, DVT, giaNhap, giaBan, maLoai, Del, soLuongTon, chietKhau, maNCC);

                    list_Show_SanPham_BanHang.Add(tempSanPham);
                    listBox_Show_BanHang.Items.Add(ds.Tables[0].Rows[i][2].ToString());
                }

            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }
        private void Load_Show_HangHoa()
        {
            string string_search_ten = textBox_Search_HangHoa_QuanLy.Text;

            string maNhaCC = "";
            for (int j = 0; j < list_Show_NhaCC.Count; j++)
                if (list_Show_NhaCC[j].TenNhaCC.Equals(cbx_NhaCC_quanLy.Text))
                {
                    maNhaCC = list_Show_NhaCC[j].MaNhaCC;
                    break;
                }

            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                if (id_MaLoai_Combobox_Show_SanPham == "TatCa")
                   // cmd.CommandText = "SELECT hanghoa.ID, MaHang, TenHang, DonViTinh, SoLuongTon, GiaNhap, GiaBan, ChietKhau, Kho, ManhaCC, GhiChu FROM hanghoa WHERE hanghoa.del = 1";
                    cmd.CommandText = "SELECT hanghoa.ID, MaHang, TenHang, DonViTinh, SoLuongTon, GiaNhap, GiaBan, ChietKhau, Kho, TenNhaCC, GhiChu FROM hanghoa left join NhaCC on hanghoa.MaNhaCC = NhaCC.MaNhaCC WHERE hanghoa.del = 1";
                else
                {
                   // cmd.CommandText = "SELECT hanghoa.ID, MaHang, TenHang, DonViTinh, SoLuongTon, GiaNhap, GiaBan, ChietKhau, Kho, TenNhaCC, GhiChu FROM hanghoa left join NhaCC on hanghoa.MaNhaCC = NhaCC.MaNhaCC WHERE hanghoa.del = 1 AND hanghoa.MaLoai = @MaLoai ";
                    cmd.CommandText = "SELECT hanghoa.ID, MaHang, TenHang, DonViTinh, SoLuongTon, GiaNhap, GiaBan, ChietKhau, Kho, TenNhaCC, GhiChu FROM hanghoa left join NhaCC on hanghoa.MaNhaCC = NhaCC.MaNhaCC WHERE hanghoa.del = 1 AND hanghoa.MaLoai = @MaLoai";
                    cmd.Parameters.AddWithValue("@MaLoai", id_MaLoai_Combobox_Show_SanPham);
                }

                if (string_search_ten.Length > 0)
                    cmd.CommandText = cmd.CommandText + " AND TenHang LIKE \'%" + string_search_ten + "%\'";

                if (maNhaCC != "")
                {
                    cmd.CommandText = cmd.CommandText + " AND hanghoa.MaNhaCC = @MaNhaCC";
                    cmd.Parameters.AddWithValue("@MaNhaCC", maNhaCC);
                }


                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                //Load loại sản phẩm

                dataGridView_HangHoa_QuanLy.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }

        private void comboBox_LoaiHang_BanHang_TextChanged(object sender, EventArgs e)
        {
            int count = list_LoaiSanPham.Count;
            for (int i = 0; i < count; i++)
            {
                if (comboBox_LoaiHang_BanHang.Text == list_LoaiSanPham[i].Ten)
                {
                    id_MaLoai_Combobox_Show_SanPham = list_LoaiSanPham[i].MaLoai;
                    Load_Show_SanPham(textBox_Search_BanHang.Text);
                    return;
                }
            }
        }


        private void textBox_Search_BanHang_TextChanged(object sender, EventArgs e)
        {
            Load_Show_SanPham(textBox_Search_BanHang.Text);
        }


        private void listBox_Show_BanHang_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int SelectedIndex = listBox_Show_BanHang.SelectedIndex;
                string MaHang = list_Show_SanPham_BanHang[SelectedIndex].MaHang;
                string TenHang = list_Show_SanPham_BanHang[SelectedIndex].TenHang;
                string DonViTinh = list_Show_SanPham_BanHang[SelectedIndex].DonViTinh;
                string GiaBan = list_Show_SanPham_BanHang[SelectedIndex].GiaBan;
                string SoLuong = "1";
                string ThanhTien = GiaBan;

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView_HoaDon, dataGridView_HoaDon.Rows.Count + 1, MaHang, TenHang, DonViTinh, GiaBan, SoLuong, ThanhTien);
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                row.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                dataGridView_HoaDon.Rows.Add(row);
                Int64 temp = tinhTongTien();
                label_tongcong.Text = (Int64.Parse(tbx_NoCu.Text, NumberStyles.AllowThousands) + temp).ToString("#,##0");
                label_thanhtien.Text = temp.ToString("#,##0");

            }
            catch (Exception)
            {
            }
        }

        private void textBox_TenKhachHang_TextChanged(object sender, EventArgs e)
        {
            text_name = textBox_TenKhachHang.Text;
        }


        public int countHoaDon(string date)
        {
            int count;

            //Lấy phân loại
            cmd.Parameters.Clear();
            //cmd.CommandText = "SELECT MaHang, Ten, Gia, Hinh FROM sanpham";               
            cmd.CommandText = "SELECT count(id) FROM hoadon WHERE MaHD LIKE \'" + date + "%\'";

            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds);                //Add combobox                
            count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return count;
        }

        public int countKhachHang()
        {
            int count;
            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT count(id) FROM khachhang";

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);                //Add combobox                
                count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                return 0;
                //   throw;
            }
            return count;
        }

        private KhachHang getByCMND(string cmnd)
        {
            KhachHang kh = null;
            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                //cmd.CommandText = "SELECT MaHang, Ten, Gia, Hinh FROM sanpham";               
                cmd.CommandText = "SELECT * FROM khachhang WHERE CMND = @CMND";
                cmd.Parameters.AddWithValue("@CMND", cmnd);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);                //Add combobox    
                if (ds.Tables[0].Rows.Count > 0)
                {
                    kh = new KhachHang(Convert.ToInt32(ds.Tables[0].Rows[0][0]),
                            ds.Tables[0].Rows[0][1].ToString(), ds.Tables[0].Rows[0][2].ToString(),
                            ds.Tables[0].Rows[0][3].ToString(), ds.Tables[0].Rows[0][4].ToString(),
                            ds.Tables[0].Rows[0][5].ToString());
                }
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                //   throw;
            }
            return kh;
        }

        private void button_ThanhToan_BanHang_Click(object sender, EventArgs e)
        {
           
            string mahoadon = "";
            truyendulieu.nocu_temp = tbx_NoCu.Text;

            //if (textBox_TenKhachHang.Text.Length == 0)
            //{
            //    MessageBox.Show("Chưa nhập thông tin khách.");
            //    return;
            //}
            Int64 tongtien = tinhTongTien(); //Int64.Parse(label_tongcong.Text, NumberStyles.AllowThousands).ToString("#,##0")
            if (tongtien == 0)
            {
                MessageBox.Show("Chưa nhập hàng hóa. ");
                return;
            }

            try
            {

                string mydate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                int count = countHoaDon(mydate) + 1;
                mahoadon = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "STT" + count;
                truyendulieu.mahoadon_temp = mahoadon;

                HienHoaDon f = new HienHoaDon(mahoadon);

                //string makhachhang;

                //makhachhang = "KH" + (countKhachHang() + 1).ToString();
                //Lấy san pham
                try
                {
      
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO hoadon(ID, MaHD, NgayLap, TongTien, TrangThai, TienDaTra, Del, ChietKhau, GiamGia, ThanhTien, TienNoCu, HoTen, DienThoai, DiaChi, BanHang) VALUES(@id, @MaHD, @NgayLap, @TongTien, @TrangThai, @TienDaTra, @Del,@ChietKhau,@GiamGia,@ThanhTien, @TienNoCu, @HoTen, @DienThoai, @DiaChi, @BanHang)";
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@MaHD", mahoadon);
                    //// cmd.Parameters.AddWithValue("@MaKH", mahoadon);
                    cmd.Parameters.AddWithValue("@NgayLap", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@TongTien", Int64.Parse(label_tongcong.Text, NumberStyles.AllowThousands));
                    cmd.Parameters.AddWithValue("@TienNoCu", Int64.Parse(tbx_NoCu.Text, NumberStyles.AllowThousands).ToString("#,##0"));
                    cmd.Parameters.AddWithValue("@GiamGia", 0);
                    cmd.Parameters.AddWithValue("@ThanhTien", label_thanhtien.Text);
                    cmd.Parameters.AddWithValue("@ChietKhau", 0);
                    cmd.Parameters.AddWithValue("@HoTen", textBox_TenKhachHang.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", textBox_SDT_KhachHang.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", textBox_DiaChi_KhachHang.Text);
                    if (radioButton_trahet.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", "Hoàn tất");
                        cmd.Parameters.AddWithValue("@TienDaTra", label_tongcong.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TienDaTra", (Int64.Parse(textBox_tratruoc.Text, NumberStyles.AllowThousands) + TienDaTraLanTruoc).ToString("#,##0"));
                        cmd.Parameters.AddWithValue("@TrangThai", "Nợ");
                    }

                    if (radioBtn_BanHang.Checked == true)
                        cmd.Parameters.AddWithValue("@BanHang", 1);
                    else
                        cmd.Parameters.AddWithValue("@BanHang", 0);

                    cmd.Parameters.AddWithValue("@Del", "1");
                    cmd.ExecuteNonQuery();

                    int count2 = dataGridView_HoaDon.Rows.Count;
   
                    for (int i = 0; i < count2; i++)
                    {
                        Double soluong = 0;
                        if (Double.TryParse(dataGridView_HoaDon.Rows[i].Cells[5].Value.ToString(), out soluong) == false)
                        {
                            dataGridView_HoaDon.CurrentRow.Cells[5].Value = "0";
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO chitiethd(ID, MaChiTiet, MaHD, MaHang, Gia, SoLuong, ThanhTien, Del) VALUES(@ID, @MaChiTiet, @MaHD, @MaHang, @Gia, @SoLuong, @ThanhTien, @Del)";
                        cmd.Parameters.AddWithValue("@ID", 0);
                        cmd.Parameters.AddWithValue("@MaChiTiet", mahoadon + "_" + dataGridView_HoaDon.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@MaHD", mahoadon);
                        cmd.Parameters.AddWithValue("@MaHang", dataGridView_HoaDon.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@Gia", dataGridView_HoaDon.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@SoLuong", soluong);
                        cmd.Parameters.AddWithValue("@ThanhTien", dataGridView_HoaDon.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@Del", "1");
                        //cmd.Parameters.AddWithValue("@ngaymua", dataGridView_HoaDon.Rows[i].Cells[1].Value);
                        //cmd.Parameters.AddWithValue("@ChietKhau", dataGridView_HoaDon.Rows[i].Cells[7].Value);
                        cmd.ExecuteNonQuery();

                        int count3 = list_Show_SanPham_BanHang.Count;
                        for (int j = 0; j < count3; j++)
                        {
                            if (list_Show_SanPham_BanHang[j].MaHang == dataGridView_HoaDon.Rows[i].Cells[1].Value.ToString())
                            {
                                list_Show_SanPham_BanHang[j].SoLuongTon -= soluong;
                                UpdateSoLuong(list_Show_SanPham_BanHang[j].SoLuongTon, dataGridView_HoaDon.Rows[i].Cells[1].Value.ToString());
                                break;
                            }
                        }              
                    }

        
                    f.ShowDialog();
                    Khoi_Tao_Bien();

                    dataGridView_HoaDon.Rows.Clear();

                    //Load_KhachHang(text_name, 1, 200);
                    // textBox_TenKhachHang.AutoCompleteCustomSource = auto2;
                    //Load_Show_HangHoa("");

                    // xoa hoa don cu
         
                    if (mahoadon_cu_xoa != "")
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "UPDATE chitiethd set Del = 0 where MaHD = @mahoadon;";
                        cmd.Parameters.AddWithValue("@mahoadon", mahoadon_cu_xoa);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "UPDATE hoadon set Del = 0 where MaHD = @mahoadon;";
                        cmd.Parameters.AddWithValue("@mahoadon", mahoadon_cu_xoa);
                        cmd.ExecuteNonQuery();

                        mahoadon_cu_xoa = "";
                    }
          
                    KiemTraSoLuongHangTonKho();
                }
                catch (Exception v)
                {
                    MessageBox.Show(v.ToString());
                }

            }
            catch (Exception c)
            { }
        }

        private Int64 tinhTongTien()
        {

            Int64 tt = 0;
            int count = dataGridView_HoaDon.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                tt = tt + Int64.Parse(dataGridView_HoaDon.Rows[i].Cells[6].Value.ToString(), NumberStyles.AllowThousands);
            }
            return tt;
        }

        private void dataGridView_HoaDon_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int count = dataGridView_HoaDon.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                dataGridView_HoaDon.Rows[i].Cells[0].Value = (i).ToString();
            }
        }
        private void Load_show_hoadon()
        {
            //Lấy sản phẩm
            try
            {
                cmd.Parameters.Clear();
                DateTime tungay;
                DateTime denngay;
                tungay = new DateTime(dateTimePicker_TuNgay.Value.Year, dateTimePicker_TuNgay.Value.Month, dateTimePicker_TuNgay.Value.Day, 0, 0, 0);
                denngay = new DateTime(dateTimePicker_DenNgay.Value.Year, dateTimePicker_DenNgay.Value.Month, dateTimePicker_DenNgay.Value.Day, 23, 59, 59);

                //cmd.CommandText = "SELECT MaHang, Ten, Gia, Hinh FROM sanpham";                
                cmd.CommandText = "SELECT MaHD, NgayLap, HoTen, DienThoai, TrangThai, ThanhTien,ChietKhau,TongTien, TienNoCu, TienDaTra FROM hoadon WHERE Del = 1 AND NgayLap >= @TuNgay AND NgayLap <= @DenNgay AND BanHang = 1";

                if (textBox_TenKhachHang_DoanhThu.Text != "")
                    cmd.CommandText = cmd.CommandText + " AND HoTen LIKE \'%" + textBox_TenKhachHang_DoanhThu.Text + "%\'";

                if (comboBox_TrangThai_DoanhThu.Text == "Hoàn tất")
                    cmd.CommandText = cmd.CommandText + " AND TrangThai = \'Hoàn tất\'";

                if (comboBox_TrangThai_DoanhThu.Text == "Nợ")
                    cmd.CommandText = cmd.CommandText + " AND TrangThai = \'Nợ\'";
                cmd.Parameters.AddWithValue("@TuNgay", tungay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@DenNgay", denngay.ToString("yyyy-MM-dd"));
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                dataGridView1.CurrentCell = dataGridView1.Rows[row3].Cells[2];
                mahoadon_quanly = dataGridView1.Rows[0].Cells[0].Value.ToString();
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Load_show_hoadon();
        }

        private void comboBox_TrangThai_DoanhThu_TextChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    if (comboBox_LoaiHang_BanHang.Text == list_LoaiSanPham[i].Ten)
            //    {
            //        id_MaLoai_Combobox_Show_SanPham = list_LoaiSanPham[i].MaLoai;
            //        Load_Show_SanPham(textBox_Search_BanHang.Text);
            //        return;
            //    }
            //}
            Load_show_hoadon();
        }


        private void dataGridView_HangHoa_QuanLy_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int stt = Convert.ToInt32(dataGridView_HangHoa_QuanLy.CurrentRow.Cells[0].Value);

            //Lấy phân loại
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = @"UPDATE hanghoa SET Del = 0 WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", stt);
                cmd.ExecuteNonQuery();
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
                //throw;
            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                mahoadon_quanly = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            { }
        }

        int row3 = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (mahoadon_quanly == "")
            {
                if (dataGridView1.RowCount == 0)
                { MessageBox.Show("Chưa tìm hóa đơn cần thanh toán"); }
                else
                {
                    row3 = 0;
                    truyendulieu.mahoadon_temp = mahoadon_quanly;
                    ThanhToan f2 = new ThanhToan(mahoadon_quanly);
                    f2.ShowDialog();
                }
            }
            else
            {
                row3 = dataGridView1.CurrentRow.Index;
                truyendulieu.mahoadon_temp = mahoadon_quanly;
                ThanhToan f2 = new ThanhToan(mahoadon_quanly);
                f2.ShowDialog();
            }
            button1_Click(sender, e);
        }

        private void dataGridView_HoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int64 gia = 0, ck = 0;
                Double soluong = 0;

                if (Int64.TryParse(dataGridView_HoaDon.CurrentRow.Cells[4].Value.ToString(), NumberStyles.AllowThousands, null, out gia) == false)
                {

                    dataGridView_HoaDon.CurrentRow.Cells[4].Value = "0";
                    return;
                }

                if (Double.TryParse(dataGridView_HoaDon.CurrentRow.Cells[5].Value.ToString(), out soluong) == false)
                {
                    dataGridView_HoaDon.CurrentRow.Cells[5].Value = "0";
                    return;
                }

                dataGridView_HoaDon.CurrentRow.Cells[4].Value = gia.ToString("#,##0");
                dataGridView_HoaDon.CurrentRow.Cells[6].Value = (soluong * gia * (100 - ck) / 100).ToString("#,##0");

                Int64 TienNoCu = 0;
                if (Int64.TryParse(tbx_NoCu.Text, out TienNoCu) == false)
                {
                    TienNoCu = 0;
                }

                Int64 temp123 = tinhTongTien() + TienNoCu;
                label_thanhtien.Text = temp123.ToString("#,##0");
                label_tongcong.Text = temp123.ToString("#,##0");
            }
            catch (Exception c)
            { }// MessageBox.Show(c.ToString()); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                try
                {
                    int row2 = dataGridView_HangHoa_QuanLy.CurrentRow.Index;
                    int stt = Convert.ToInt32(dataGridView_HangHoa_QuanLy.CurrentRow.Cells[0].Value);
                    //Lấy phân loại
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE hanghoa SET Del = 0 WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", stt);
                        cmd.ExecuteNonQuery();
                        Load_Show_HangHoa();
                        dataGridView_HangHoa_QuanLy.CurrentCell = dataGridView_HangHoa_QuanLy.Rows[row2].Cells[2];

                        Load_Show_SanPham("");
                    }
                    catch (Exception v)
                    {
                        MessageBox.Show(v.Message.ToString());
                    }
                }
                catch (Exception c)
                { }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            truyendulieu.ghichu_temp = richTextBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load_KhachHang(text_name, 1, 200);

            //textBox_TenKhachHang.AutoCompleteCustomSource = auto2;
        }

        private void radioButton_trahet_Checked(object sender, EventArgs e)
        {
            textBox_tratruoc.Enabled = false;
        }

        private void radioButton_tratruoc_Checked(object sender, EventArgs e)
        {
            textBox_tratruoc.Enabled = true;
        }

        private void btn_NhapTiep_Click(object sender, EventArgs e)
        {
            if (mahoadon_quanly == "")
            {
                if (dataGridView1.RowCount == 0)
                { MessageBox.Show("Chưa tìm hóa đơn cần thanh toán"); }
                else
                {
                    row3 = 0;
                    truyendulieu.mahoadon_temp = mahoadon_quanly;

                    mahoadon_cu_xoa = mahoadon_quanly;
                    //Lấy danh sách các hàng hóa đã mua
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"select chitiethd.*, hoadon.HoTen, hoadon.DienThoai, hoadon.DiaChi, hoadon.tiendatra, hoadon.TienNoCu, hoadon.thanhtien, hoadon.tongtien from chitiethd inner join hoadon on chitiethd.mahd = hoadon.mahd where hoadon.MaHD = @mahoadon";
                        cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                        MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);

                        //load tiền nợ cũ
                        tbx_NoCu.Text = ds.Tables[0].Rows[0][14].ToString();

                        //load tiền đã trả
                        label_DaTraLanTruoc.Text = "Đã trả lần trước: " + ds.Tables[0].Rows[0][13].ToString();
                        radioButton_trahet.Select();
                        TienDaTraLanTruoc = Int64.Parse(ds.Tables[0].Rows[0][13].ToString(), NumberStyles.AllowThousands);

                        // load khách hàng
                        textBox_TenKhachHang.Text = ds.Tables[0].Rows[0][10].ToString();
                        textBox_DiaChi_KhachHang.Text = ds.Tables[0].Rows[0][12].ToString();
                        textBox_SDT_KhachHang.Text = ds.Tables[0].Rows[0][11].ToString();

                        // load tổng tiền
                        label_thanhtien.Text = ds.Tables[0].Rows[0][15].ToString();
                        label_tongcong.Text = ds.Tables[0].Rows[0][16].ToString();

                        //Load loại sản phẩm
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string MaHang = row[3].ToString();
                            string TenHang = "";
                            string DonViTinh = "";
                            string GiaBan = "";
                            string ThanhTien = row[6].ToString();
                            foreach (SanPham sp in list_Show_SanPham_BanHang)
                            {
                                if (sp.MaHang.CompareTo(MaHang) == 0)
                                {
                                    TenHang = sp.TenHang;
                                    DonViTinh = sp.DonViTinh;

                                    break;
                                }
                            }
                            GiaBan = row[4].ToString();
                            string SoLuong = row[5].ToString();
                            string ChietKhau = row[9].ToString();
                            string ngaymua = row[8].ToString();

                            DataGridViewRow row_Add = new DataGridViewRow();

                            row_Add.CreateCells(dataGridView_HoaDon, dataGridView_HoaDon.Rows.Count + 1, MaHang, TenHang, DonViTinh, GiaBan, SoLuong, ThanhTien);
                            row_Add.DefaultCellStyle.BackColor = Color.LightGreen;
                            row_Add.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                            dataGridView_HoaDon.Rows.Add(row_Add);
                            //Int64 temp = tinhTongTien();
                            //label_thanhtien.Text = temp.ToString("#,##0");
                            //label_tongcong.Text = (temp + Int64.Parse(tbx_NoCu.Text, NumberStyles.AllowThousands)).ToString("#,##0");

                        }

                        tabControl1.SelectedIndex = 0;

                    }
                    catch (Exception v)
                    {
                        MessageBox.Show(v.Message.ToString());
                    }
                }
            }
            else
            {
                row3 = dataGridView1.CurrentRow.Index;
                truyendulieu.mahoadon_temp = mahoadon_quanly;

                mahoadon_cu_xoa = mahoadon_quanly;
                //Lấy danh sách các hàng hóa đã mua
                try
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"select chitiethd.*, hoadon.HoTen, hoadon.DienThoai, hoadon.DiaChi, hoadon.tiendatra, hoadon.TienNoCu, hoadon.thanhtien, hoadon.tongtien from chitiethd inner join hoadon on chitiethd.mahd = hoadon.mahd where hoadon.MaHD = @mahoadon";
                    cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);

                    //load tiền nợ cũ
                    tbx_NoCu.Text = ds.Tables[0].Rows[0][14].ToString();

                    //load tiền đã trả
                    label_DaTraLanTruoc.Text = "Đã trả lần trước: " + ds.Tables[0].Rows[0][13].ToString();
                    radioButton_trahet.Select();
                    TienDaTraLanTruoc = Int64.Parse(ds.Tables[0].Rows[0][13].ToString(), NumberStyles.AllowThousands);

                    // load khách hàng
                    textBox_TenKhachHang.Text = ds.Tables[0].Rows[0][10].ToString();
                    textBox_DiaChi_KhachHang.Text = ds.Tables[0].Rows[0][12].ToString();
                    textBox_SDT_KhachHang.Text = ds.Tables[0].Rows[0][11].ToString();

                    // load tổng tiền
                    label_thanhtien.Text = ds.Tables[0].Rows[0][15].ToString();
                    label_tongcong.Text = ds.Tables[0].Rows[0][16].ToString();

                    //Load loại sản phẩm
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string MaHang = row[3].ToString();
                        string TenHang = "";
                        string DonViTinh = "";
                        string GiaBan = "";
                        string ThanhTien = row[6].ToString();
                        foreach (SanPham sp in list_Show_SanPham_BanHang)
                        {
                            if (sp.MaHang.CompareTo(MaHang) == 0)
                            {
                                TenHang = sp.TenHang;
                                DonViTinh = sp.DonViTinh;

                                break;
                            }
                        }
                        GiaBan = row[4].ToString();
                        string SoLuong = row[5].ToString();
                        string ChietKhau = row[9].ToString();
                        string ngaymua = row[8].ToString();

                        DataGridViewRow row_Add = new DataGridViewRow();

                        row_Add.CreateCells(dataGridView_HoaDon, dataGridView_HoaDon.Rows.Count + 1, MaHang, TenHang, DonViTinh, GiaBan, SoLuong, ThanhTien);
                        row_Add.DefaultCellStyle.BackColor = Color.LightGreen;
                        row_Add.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                        dataGridView_HoaDon.Rows.Add(row_Add);
                        //Int64 temp = tinhTongTien();
                        //label_thanhtien.Text = temp.ToString("#,##0");
                        //label_tongcong.Text = (temp + Int64.Parse(tbx_NoCu.Text, NumberStyles.AllowThousands)).ToString("#,##0");

                    }

                    tabControl1.SelectedIndex = 0;
                }

                catch (Exception v)
                {
                    MessageBox.Show(v.Message.ToString());
                }
            }

        }

        private void tbx_NoCu_TextChanged(object sender, EventArgs e)
        {
            Int64 temp2;
            if (Int64.TryParse(tbx_NoCu.Text, NumberStyles.AllowThousands, null, out temp2))
            {
                truyendulieu.nocu_temp = tbx_NoCu.Text;
                label_tongcong.Text = (Int64.Parse(label_thanhtien.Text, NumberStyles.AllowThousands) + temp2).ToString("#,##0");
            }
            else
                label_tongcong.Text = label_thanhtien.Text;

        }

        private void textBox_tratruoc_Leave(object sender, EventArgs e)
        {
            if (textBox_tratruoc.Enabled == true)
            {
                Int64 temp;
                if (Int64.TryParse(textBox_tratruoc.Text, NumberStyles.AllowThousands, null, out temp))
                {
                    textBox_tratruoc.Text = temp.ToString("#,##0");
                }
            }
        }

        private void tbx_NoCu_Leave(object sender, EventArgs e)
        {
            Int64 temp;
            if (Int64.TryParse(tbx_NoCu.Text, NumberStyles.AllowThousands, null, out temp))
            {
                tbx_NoCu.Text = temp.ToString("#,##0");
            }
        }

        public void KiemTraSoLuongHangTonKho()
        {
            string thongBao = "";
            int count = list_Show_SanPham_BanHang.Count;
            for (int i = 0; i < count; i++)
            {
                if (list_Show_SanPham_BanHang[i].SoLuongTon < 5)
                {
                    thongBao = thongBao + list_Show_SanPham_BanHang[i].TenHang + "\n";
                }
            }

            if (thongBao != "")
            {
                ThongBaoHetHang f2 = new ThongBaoHetHang(thongBao);
                f2.ShowDialog();
            }
        }
        public void UpdateSoLuong(double soluong, string mahang)
        {
            //Lấy phân loại

            cmd.Parameters.Clear();
            cmd.CommandText = @"UPDATE hanghoa SET SoLuongTon = @SoLuong WHERE MaHang = @ID";
            cmd.Parameters.AddWithValue("@ID", mahang);
            cmd.Parameters.AddWithValue("@SoLuong", soluong);
            cmd.ExecuteNonQuery();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            Load_Show_SanPham("");
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            if (tabPage2_hadLoad == false)
            {
                Load_Show_HangHoa();
                Load_NhaCungCap();
                tabPage2_hadLoad = true;
            }
            cbx_NhaCC_quanLy.Items.Clear();
            for (int i = 0; i < list_Show_NhaCC.Count; i++)
                cbx_NhaCC_quanLy.Items.Add(list_Show_NhaCC[i].TenNhaCC);
        }

        private void dataGridView_HoaDon_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Int64 temp = tinhTongTien();
            label_tongcong.Text = (Int64.Parse(tbx_NoCu.Text, NumberStyles.AllowThousands) + temp).ToString("#,##0");
            label_thanhtien.Text = temp.ToString("#,##0");
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (mahoadon_quanly == "")
            {
                MessageBox.Show("Chưa chọn hóa đơn để xóa");
                row3 = 0;
            }
            else
            {
                row3 = dataGridView1.CurrentRow.Index;
                DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này", "Cảnh báo", MessageBoxButtons.YesNo);
                if (re == DialogResult.Yes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE chitiethd set Del = 0 where MaHD = @mahoadon;";
                    cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE hoadon set Del = 0 where MaHD = @mahoadon;";
                    cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                    cmd.ExecuteNonQuery();
                }
                button1_Click(sender, e);
            }
        }

        private void btn_XemHD_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            List<string> list_maHoaDon = new List<string>();

            for (int i = 0; i < count-1; i++)
            {
                list_maHoaDon.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());    
            }
            if (mahoadon_quanly == "")
            {
                MessageBox.Show("Chưa chọn hóa đơn để xem");
                row3 = 0;
            }
            else
            {
                HienHoaDon f = new HienHoaDon(mahoadon_quanly, list_maHoaDon);
                f.ShowDialog();
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            Load_NhaCungCap();
        }

        private void listBox_NhaCC_DoubleClick(object sender, EventArgs e)
        {
            //Lấy sản phẩm
            try
            {
                if (listBox_NhaCC.SelectedIndex < 0)
                {
                    listBox_NhaCC_select_index = 0;
                    return;
                }
                listBox_NhaCC_select_index = listBox_NhaCC.SelectedIndex;
                TenNhaCC_double_Click = list_Show_NhaCC[listBox_NhaCC.SelectedIndex].TenNhaCC;

                cmd.Parameters.Clear();

                cmd.CommandText = "SELECT MaHD, NgayLap, HoTen, DienThoai, TrangThai, ThanhTien,ChietKhau,TongTien, TienDaTra FROM hoadon WHERE Del = 1 AND HoTen = @TenNhaCC AND BanHang = 0";

                cmd.Parameters.AddWithValue("@TenNhaCC", TenNhaCC_double_Click);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                dataGridView2.DataSource = ds.Tables[0].DefaultView;
                dataGridView2.CurrentCell = dataGridView2.Rows[row3].Cells[2];
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }

        private void radioBtn_BanHang_CheckedChanged(object sender, EventArgs e)
        {
            label_ThongTin.Text = "Thông Tin Khách Hàng";
            tbx_NoCu.Enabled = true;
            b_BanHang = true;
        }

        private void radioBtn_MuaHang_CheckedChanged(object sender, EventArgs e)
        {
            label_ThongTin.Text = "Thông Tin Nhà Cung Cấp";
            //tbx_NoCu.Enabled = false;
            b_BanHang = false;
        }

        private void btn_ThemNhaCC_Click(object sender, EventArgs e)
        {
            id_update_NhaCC = -1;
            NhaCC_Form f = new NhaCC_Form();
            f.ShowDialog();

            list_Show_NhaCC.Clear();
            Load_NhaCungCap();
            LoadDataToCollection();
        }

        private void listBox_NhaCC_Click(object sender, EventArgs e)
        {
            if (listBox_NhaCC.SelectedIndex >= 0)
            {
                id_update_NhaCC = list_Show_NhaCC[listBox_NhaCC.SelectedIndex].ID;
            }

        }

        private void btn_SuaNhaCC_Click(object sender, EventArgs e)
        {
            if (id_update_NhaCC >= 0)
            {
                NhaCC_Form f = new NhaCC_Form();
                f.ShowDialog();
                id_update_NhaCC = -1;

                list_Show_NhaCC.Clear();
                Load_NhaCungCap();
                LoadDataToCollection();
            }
        }

        private void button_ThemHang_Quanly_Click_1(object sender, EventArgs e)
        {

            if (dataGridView_HangHoa_QuanLy.CurrentRow != null)
                row = dataGridView_HangHoa_QuanLy.CurrentRow.Index;
            if (row >= 0)
            {
                id_update_hanghoa = -1;
                HangHoa f = new HangHoa();
                f.ShowDialog();

                Load_Show_HangHoa();
                dataGridView_HangHoa_QuanLy.CurrentCell = dataGridView_HangHoa_QuanLy.Rows[row].Cells[2];
                Load_Show_SanPham("");
            }
        }

        private void button_Sua_Quanly_Click_1(object sender, EventArgs e)
        {
            try
            {
                row = dataGridView_HangHoa_QuanLy.CurrentRow.Index;
                int stt = Convert.ToInt32(dataGridView_HangHoa_QuanLy.CurrentRow.Cells[0].Value);
                if (stt >= 0)
                {
                    id_update_hanghoa = stt;
                    HangHoa f = new HangHoa();
                    f.ShowDialog();
                    id_update_hanghoa = -1;
                }
            }
            catch (Exception) { }
            finally
            {
                Load_Show_HangHoa();
                dataGridView_HangHoa_QuanLy.CurrentCell = dataGridView_HangHoa_QuanLy.Rows[row].Cells[2];

                Load_Show_SanPham("");
            }
        }

        private void btn_XoaNhaCC_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                //Lấy phân loại
                if (id_update_NhaCC >= 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE nhacc SET Del = 0 WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id_update_NhaCC);
                    cmd.ExecuteNonQuery();

                    Load_NhaCungCap();
                    LoadDataToCollection();
                }
            }
        }

        private void LoadDataToCollection()
        {
            auto2.Clear();

            int count = list_Show_NhaCC.Count;
            for (int i = 0; i < count; i++)
            {
                auto2.Add(list_Show_NhaCC[i].TenNhaCC);
            }

            textBox_TenKhachHang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox_TenKhachHang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox_TenKhachHang.AutoCompleteCustomSource = auto2;

        }

        private void textBox_DiaChi_KhachHang_DoubleClick(object sender, EventArgs e)
        {
            int count = list_Show_NhaCC.Count;

            for (int i = 0; i < count; i++)
            {
                if (list_Show_NhaCC[i].TenNhaCC.Equals(textBox_TenKhachHang.Text))
                {
                    textBox_DiaChi_KhachHang.Text = list_Show_NhaCC[i].DiaChi;
                    textBox_SDT_KhachHang.Text = list_Show_NhaCC[i].DienThoai;
                }
            }
        }

        private void textBox_SDT_KhachHang_DoubleClick(object sender, EventArgs e)
        {
            int count = list_Show_NhaCC.Count;

            for (int i = 0; i < count; i++)
            {
                if (list_Show_NhaCC[i].TenNhaCC.Equals(textBox_TenKhachHang.Text))
                {
                    textBox_DiaChi_KhachHang.Text = list_Show_NhaCC[i].DiaChi;
                    textBox_SDT_KhachHang.Text = list_Show_NhaCC[i].DienThoai;
                }
            }
        }

        private void btn_XenHD_NhaCC_Click(object sender, EventArgs e)
        {
            int count = dataGridView2.Rows.Count;
            List<string> list_maHoaDon = new List<string>();

            for (int i = 0; i < count - 1; i++)
            {
                list_maHoaDon.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
            }

            if (mahoadon_quanly == "")
            {
                MessageBox.Show("Chưa chọn hóa đơn để xem");
                row3 = 0;
            }
            else
            {
                HienHoaDon f = new HienHoaDon(mahoadon_quanly, list_maHoaDon);
                f.ShowDialog();
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                mahoadon_quanly = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string TenNhaCC = list_Show_NhaCC[listBox_NhaCC_select_index].TenNhaCC;

                cmd.Parameters.Clear();
                DateTime tungay;
                DateTime denngay;
                tungay = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 0, 0, 0);
                denngay = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59);

                cmd.CommandText = "SELECT MaHD, NgayLap, HoTen, DienThoai, TrangThai, ThanhTien,ChietKhau,TongTien, TienDaTra FROM hoadon WHERE Del = 1 AND HoTen = @TenNhaCC AND NgayLap >= @TuNgay AND NgayLap <= @DenNgay And BanHang=0";
                cmd.Parameters.AddWithValue("@TenNhaCC", TenNhaCC);
                cmd.Parameters.AddWithValue("@TuNgay", tungay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@DenNgay", denngay.ToString("yyyy-MM-dd"));

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                dataGridView2.DataSource = ds.Tables[0].DefaultView;
                dataGridView2.CurrentCell = dataGridView2.Rows[row3].Cells[2];
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mahoadon_quanly == "")
            {
                MessageBox.Show("Chưa chọn hóa đơn để xóa");
                row3 = 0;
            }
            else
            {
                row3 = dataGridView2.CurrentRow.Index;
                DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này", "Cảnh báo", MessageBoxButtons.YesNo);
                if (re == DialogResult.Yes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE chitiethd set Del = 0 where MaHD = @mahoadon;";
                    cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE hoadon set Del = 0 where MaHD = @mahoadon;";
                    cmd.Parameters.AddWithValue("@mahoadon", mahoadon_quanly);
                    cmd.ExecuteNonQuery();
                }
                listBox_NhaCC_DoubleClick(sender, e);
            }
        }

        private void btn_Tong_Thu_Click(object sender, EventArgs e)
        {
            DateTime tungay;
            DateTime denngay;
            tungay = new DateTime(dateTimePicker_TuNgay.Value.Year, dateTimePicker_TuNgay.Value.Month, dateTimePicker_TuNgay.Value.Day, 0, 0, 0);
            denngay = new DateTime(dateTimePicker_DenNgay.Value.Year, dateTimePicker_DenNgay.Value.Month, dateTimePicker_DenNgay.Value.Day, 23, 59, 59);


            cmd.Parameters.Clear();
            cmd.CommandText = "select sum(tongtien) from hoadon where Del = 1 AND NgayLap >= @TuNgay AND NgayLap <= @DenNgay AND BanHang = 1";

            if (textBox_TenKhachHang_DoanhThu.Text != "")
                cmd.CommandText = cmd.CommandText + " AND HoTen LIKE \'%" + textBox_TenKhachHang_DoanhThu.Text + "%\'";

            if (comboBox_TrangThai_DoanhThu.Text == "Hoàn tất")
                cmd.CommandText = cmd.CommandText + " AND TrangThai = \'Hoàn tất\'";

            if (comboBox_TrangThai_DoanhThu.Text == "Nợ")
                cmd.CommandText = cmd.CommandText + " AND TrangThai = \'Nợ\'";

            cmd.Parameters.AddWithValue("@TuNgay", tungay.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@DenNgay", denngay.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();

            string tongThu = string.Format("{0:#,##0.##}", cmd.ExecuteScalar());
            MessageBox.Show(tongThu, "Tổng Thu");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime tungay;
            DateTime denngay;
            tungay = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 0, 0, 0);
            denngay = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59);


            cmd.Parameters.Clear();
            cmd.CommandText = "select sum(tongtien) from hoadon where Del = 1 AND NgayLap >= @TuNgay AND NgayLap <= @DenNgay AND BanHang = 0";

            if (TenNhaCC_double_Click != "")
                cmd.CommandText = cmd.CommandText + " AND HoTen LIKE \'%" + TenNhaCC_double_Click + "%\'";

            cmd.Parameters.AddWithValue("@TuNgay", tungay.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@DenNgay", denngay.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();

            string tongThu = string.Format("{0:#,##0.##}", cmd.ExecuteScalar());
            MessageBox.Show(tongThu, "Tổng Chi");
        }

        private void comboBox_LoaiHangHoa_Quanly_TextChanged(object sender, EventArgs e)
        {
            int count = list_LoaiSanPham.Count;
            for (int i = 0; i < count; i++)
            {
                if (comboBox_LoaiHangHoa_Quanly.Text.Equals(list_LoaiSanPham[i].Ten))
                {
                    id_MaLoai_Combobox_Show_SanPham = list_LoaiSanPham[i].MaLoai;
                    Load_Show_SanPham(textBox_Search_BanHang.Text);
                    Load_Show_HangHoa();
                    return;
                }
            }
            
        }

        private void textBox_Search_HangHoa_QuanLy_TextChanged(object sender, EventArgs e)
        {
           // Load_Show_HangHoa();
           BindingSource bs = new BindingSource();
           bs.DataSource = dataGridView_HangHoa_QuanLy.DataSource;
           bs.Filter = "TenHang like '%" + textBox_Search_HangHoa_QuanLy.Text + "%'";
           dataGridView1.DataSource = bs;
        }

        private void cbx_NhaCC_quanLy_TextChanged(object sender, EventArgs e)
        {
            //Load_Show_HangHoa();
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView_HangHoa_QuanLy.DataSource;
            bs.Filter = "TenNhaCC like '%" + cbx_NhaCC_quanLy.Text + "%'";
            dataGridView1.DataSource = bs;
        }

        private void textBox_TenKhachHang_DoanhThu_TextChanged(object sender, EventArgs e)
        {
            Load_show_hoadon();
        }

        private void dateTimePicker_TuNgay_ValueChanged(object sender, EventArgs e)
        {
            Load_show_hoadon();
        }

        private void dateTimePicker_DenNgay_ValueChanged(object sender, EventArgs e)
        {
            Load_show_hoadon();
        }

    }
}
