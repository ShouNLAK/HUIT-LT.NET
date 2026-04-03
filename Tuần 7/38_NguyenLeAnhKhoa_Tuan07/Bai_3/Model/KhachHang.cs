using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model
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

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public KhachHang()
        {
        }

        public KhachHang(string ten, string sdt, string mail)
        {
            TenKhachHang = ten;
            SoDienThoai = sdt;
            Email = mail;
        }
    }
}
