using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuaHangXayDung
{
    public class KhachHang
    {
        public int ID { get; set; }
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string DiaCHi { get; set; }
        public string DienThoai { get; set; }
        public string CMND { get; set; }

        public KhachHang(int a, string b, string c, string d, string e, string g)
        {
            ID = a;
            MaKH = b;
            HoTen = c;
            DiaCHi = d;
            DienThoai = e;
            CMND = g;
        }
    }
}
