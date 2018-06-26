using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;

namespace CuaHangXayDung
{
    public partial class HienHoaDon : Form
    {
        string MyConnectionMySQL = "Server=localhost;Database=cuahangxaydung_new;Uid=root;Pwd=123456;";
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        MySqlConnection connection;
        MySqlCommand cmd,cmd2;
        string MAHD = "";
        List<String> list_MaHoaDon = new List<string>();

        public HienHoaDon(String MaHD)
        {

            MAHD = MaHD;
            connection = new MySqlConnection(MyConnectionMySQL);
            connection.Open();
            cmd = connection.CreateCommand();
            cmd2 = connection.CreateCommand();
            InitializeComponent();
        }

        public HienHoaDon(String MaHD, List<String> list_HoaDon)
        {
            MAHD = MaHD;
            list_MaHoaDon = list_HoaDon;

            connection = new MySqlConnection(MyConnectionMySQL);
            connection.Open();
            cmd = connection.CreateCommand();
            cmd2 = connection.CreateCommand();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Load_HD(string MaHoaDon)
        {
            //cmd.Commanconnection.CreateCommand();dText = "SELECT ID,MaHD,MaHang,Gia,SoLuong,ThanhTien,Del FROM chitiethd WHERE Del = 1 AND MaHD = @MaHD";
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT TenHang, DonViTinh, chitiethd.Gia, SoLuong, ThanhTien FROM chitiethd,hanghoa WHERE chitiethd.Del = 1 AND MaHD = @MaHD AND hanghoa.MaHang = chitiethd.MaHang";
            cmd.Parameters.AddWithValue("@MaHD", MaHoaDon);

            // lay ho ten dia chi khach hang
            cmd2.Parameters.Clear();
            cmd2 = connection.CreateCommand();
            cmd2.CommandText = "SELECT HoTen,DiaChi,DienThoai,TongTien,TienDaTra,GiamGia, TienNoCu, NgayLap, hoadon.ThanhTien FROM chitiethd,hoadon WHERE chitiethd.MaHD = @MaHD AND hoadon.MaHD = @MaHD";
            cmd2.Parameters.AddWithValue("@MaHD", MaHoaDon);

            // dataset hoadon
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataSet a = new DataSet();
            adap.Fill(a, "DataTable1");

            //dataset khachhang
            MySqlDataAdapter adap2 = new MySqlDataAdapter(cmd2);
            DataSet a2 = new DataSet();
            adap2.Fill(a2);

            // fill label of khachhang
            if (a2.Tables[0].Rows.Count > 0)
            {

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                if (Form1.b_BanHang == true)
                    reportParameters.Add(new ReportParameter("TenKH", "Tên Khách Hàng : " + a2.Tables[0].Rows[0][0].ToString()));
                else
                {
                    reportParameters.Add(new ReportParameter("TenKH", "Tên Nhà CC : " + a2.Tables[0].Rows[0][0].ToString()));
                }
                reportParameters.Add(new ReportParameter("SoDT", "Số Điện Thoại : " + a2.Tables[0].Rows[0][2].ToString()));
                reportParameters.Add(new ReportParameter("DiaChi", "Địa Chỉ : " + a2.Tables[0].Rows[0][1].ToString()));
                reportParameters.Add(new ReportParameter("MaHD", "Mã HD : " + MaHoaDon));
                reportParameters.Add(new ReportParameter("TongTien", string.Format("{0:#,##0.##}", Int64.Parse(a2.Tables[0].Rows[0][3].ToString()))));
                reportParameters.Add(new ReportParameter("NoCu", a2.Tables[0].Rows[0][6].ToString()));
                reportParameters.Add(new ReportParameter("TienDaTra", a2.Tables[0].Rows[0][4].ToString()));
                reportParameters.Add(new ReportParameter("TongToa", a2.Tables[0].Rows[0][8].ToString()));
                reportParameters.Add(new ReportParameter("TienConLai", (double.Parse(a2.Tables[0].Rows[0][3].ToString()) - double.Parse(a2.Tables[0].Rows[0][4].ToString())).ToString("#,##0")));
                DateTime tem = DateTime.Parse(a2.Tables[0].Rows[0][7].ToString());
                reportParameters.Add(new ReportParameter("Date", "Ngày " + tem.Day + " tháng " + tem.Month + " năm " + tem.Year));
                reportParameters.Add(new ReportParameter("GhiChu", "Ghi Chú : " + truyendulieu.ghichu_temp));
                this.reportViewer1.LocalReport.SetParameters(reportParameters);
                this.reportViewer1.RefreshReport();
            }
            //fill table hoa don and refresh
            ReportDataSource datasource = new ReportDataSource("DataSet1", a.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.RefreshReport();
        }
        private void HienHoaDon_Load(object sender, EventArgs e)
        {
            Load_HD(MAHD);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //PageSettings a = new PageSettings();
            //a.PaperSize = new PaperSize("A5", 583, 827);
            //a.Margins = new Margins(0, 0, 0, 0);
            //reportViewer1.SetPageSettings(a);
            
            reportViewer1.PrintDialog();
        }

        private void btn_backward_Click(object sender, EventArgs e)
        {
            int index = list_MaHoaDon.IndexOf(MAHD);
            if (index > 0)
            {
                MAHD = list_MaHoaDon[index - 1];
                Load_HD(MAHD);
            }
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            int index = list_MaHoaDon.IndexOf(MAHD);

            if (index < list_MaHoaDon.Count - 1)
            {
                MAHD = list_MaHoaDon[index + 1];
                Load_HD(MAHD);
            }
        }
    }
}
