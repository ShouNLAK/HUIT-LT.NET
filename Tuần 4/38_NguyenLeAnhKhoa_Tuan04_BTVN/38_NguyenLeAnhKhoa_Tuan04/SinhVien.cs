using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    public class SinhVien
    {
        private string mssv;
        private string ten;
        private DateTime ngaySinh;
        private string gioiTinh;
        private List<String> soThich;
        private string lop;

        public string MSSV
        {
            get { return mssv; }
            set { mssv = value; }
        }
        public string Ten
        { 
            get { return ten; } 
            set { ten = value; } 
        }
        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }
        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }
        public string Lop
        {
            get { return lop; }
            set { lop = value; }
        }
        public List<String> SoThich
        {
            get { return soThich; }
            set { soThich = value; }
        }
        public SinhVien(string mssv, string name, DateTime date, string phai, List<string> hobby, string Class)
        {
            MSSV = mssv;
            Ten = name;
            NgaySinh = date;
            GioiTinh = phai;
            SoThich = hobby;
            Lop = Class;
        }
    }
}
