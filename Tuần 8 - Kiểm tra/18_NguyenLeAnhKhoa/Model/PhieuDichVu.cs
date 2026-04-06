using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_NguyenLeAnhKhoa.Model
{
    internal class PhieuDichVu
    {
        private string hoTenKH;
        private string sdt;
        private string diaChi;
        private int soPhong;
        private ObservableCollection<ChiTietDichVu> dsDV;
        private int tongTien;

        public string HoTenKH
        {
            get { return hoTenKH; }
            set { hoTenKH = value; }
        }
        public string SDT
        {
            get { return sdt; }
            set {  sdt = value; }
        }
        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }
        public int SoPhong
        {
            get { return soPhong; }
            set { soPhong = value;}
        }
        public ObservableCollection<ChiTietDichVu> DSDV
        {
            get { return dsDV; }
            set {  dsDV = value; }
        }
        public int TongTien
        {
            set { tongTien = DSDV.Sum(o => o.ThanhTien); }
        }
        public PhieuDichVu() { }
        public PhieuDichVu(string hoTen, string sdt, string diachi, int sophong, ObservableCollection<ChiTietDichVu> DS)
        {
            HoTenKH = hoTen;
            SDT = sdt;
            DiaChi = diachi;
            SoPhong = sophong;
            DSDV = DS;
        }
    }

}
