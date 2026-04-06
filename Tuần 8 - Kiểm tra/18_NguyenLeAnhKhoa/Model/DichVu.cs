using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _18_NguyenLeAnhKhoa.Model
{
    internal class DichVu
    {
        private string maDV;
        private string tenDV;
        private int donGia;
        private NhomDichVu maNhom;

        public string MaDV
        { 
            get { return maDV; } 
            set { maDV = value; } 
        }
        public string TenDV
        {
            get { return  tenDV; }
            set { tenDV = value; }
        }
        public int DonGia
        {
            get { return  donGia; }
            set { donGia = value; }
        }
        public NhomDichVu MaNhom
        {
            get { return maNhom; }
            set { maNhom = value; }
        }
        public DichVu() { }
        public DichVu(string ma, string ten, int gia, NhomDichVu nhomDV)
        {
            maDV = ma;
            TenDV = ten;
            DonGia = gia;
            MaNhom = nhomDV;
        }
    }
}
