using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _18_NguyenLeAnhKhoa.Model
{
    internal class NhomDichVu
    {
        private string maNhomDV;
        private string tenNhomDV;

        public string MaNhomDV
        {
            get { return maNhomDV; }
            set { maNhomDV = value; }
        }
        public string TenNhomDV
        {
            get { return tenNhomDV;}
            set { tenNhomDV = value; }
        }
        public NhomDichVu() { }
        public NhomDichVu(string ma, string ten)
        {
            MaNhomDV = ma;
            TenNhomDV = ten;
        }
    }
}
