
namespace CuaHangXayDung
{
    class LoaiSanPham
    {
        public int ID { get; set; }
        public string MaLoai { get; set; }
        public string Ten { get; set; }

        public LoaiSanPham(int a, string b, string c)
        {
            ID = a;
            MaLoai = b;
            Ten = c;
        }

        

    }
}
