using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _18_NguyenLeAnhKhoa.Model
{
    internal class ChiTietDichVu
    {
        private string maDV;
        private string tenDV;
        private int soLanSD;
        private int donGia;
        private int thanhTien;

        public string MaDV
        {
            get { return maDV; }
            set { maDV = value; }
        }
        public string TenDV
        {
            get { return tenDV; }
            set {  tenDV = value; }
        }
        public int SoLanSD
        {
            get { return soLanSD; }
            set {  soLanSD = value; }
        }
        public int DonGia
        {
            get { return donGia; }
            set {  donGia = value; }
        }
        public int ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value ;}
        }

        public ChiTietDichVu() { }
        public ChiTietDichVu(string ma, string ten, int so, int don)
        {
            MaDV = ma;
            TenDV  = ten;
            SoLanSD = so;
            DonGia = don;
            ThanhTien = SoLanSD * DonGia;
        }
    }
}
