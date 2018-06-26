using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuaHangXayDung
{
    public class NhaCC
    {
        public int ID { get; set; }
        public string MaNhaCC { get; set; }
        public string TenNhaCC { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public int Del { get; set; }

        public NhaCC(int a1, string a2, string a3, string a4, string a5, int a8)
        {
            ID = a1;
            MaNhaCC = a2;
            TenNhaCC = a3;
            DiaChi = a4;
            DienThoai = a5;
            Del = a8;
        }
    }
}
