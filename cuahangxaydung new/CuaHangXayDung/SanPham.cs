
namespace CuaHangXayDung
{
   public class SanPham
    {
        public int ID { get; set; }
        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public string DonViTinh { get; set; }
        public string GiaNhap { get; set; }
        public string GiaBan { get; set; }
        public string MaLoai { get; set; }
        public int Del { get; set; }
        public double SoLuongTon { get; set; }
        public string ChietKhau { get; set; }
        public string MaNhaCC { get; set; }

        public SanPham(int a1, string a2, string a3, string a4, string a5, string a6, string a7, int a8, double a9, string a10, string a11)
        {
            ID = a1;
            MaHang = a2;
            TenHang = a3;
            DonViTinh = a4;
            GiaNhap = a5;
            GiaBan = a6;
            MaLoai = a7;
            Del = a8;
            SoLuongTon = a9;
            ChietKhau = a10;
            MaNhaCC = a11;
        }
    }
}
