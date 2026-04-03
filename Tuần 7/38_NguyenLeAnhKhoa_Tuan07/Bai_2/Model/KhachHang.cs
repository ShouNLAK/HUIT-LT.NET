using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model
{
    internal class KhachHang : BaseViewModel
    {
        private string tenKhachHang;
        public string TenKhachHang
        {
            get { return tenKhachHang; }
            set
            {
                tenKhachHang = value;
                OnPropertyChanged();
            }
        }

        private string soDienThoai;
        public string SoDienThoai
        {
            get { return soDienThoai; }
            set
            {
                soDienThoai = value;
                OnPropertyChanged();
            }
        }

        private int soKhach;
        public int SoKhach
        {
            get { return soKhach; }
            set
            {
                soKhach = value;
                OnPropertyChanged();
            }
        }

        private bool laSinhVien;
        public bool LaSinhVien
        {
            get { return laSinhVien; }
            set
            {
                laSinhVien = value;
                OnPropertyChanged();
            }
        }

        public KhachHang()
        {
        }

        public KhachHang(string ten, string sdt, int soKhachHang, bool sinhVien)
        {
            TenKhachHang = ten;
            SoDienThoai = sdt;
            SoKhach = soKhachHang;
            LaSinhVien = sinhVien;
        }
    }
}
