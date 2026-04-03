using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model
{
    internal class MonAnNuocUong : BaseViewModel
    {
        private string tenMon;
        public string TenMon
        {
            get { return tenMon; }
            set
            {
                tenMon = value;
                OnPropertyChanged();
            }
        }

        private int donGia;
        public int DonGia
        {
            get { return donGia; }
            set
            {
                donGia = value;
                OnPropertyChanged();
            }
        }

        private string loai;
        public string Loai
        {
            get { return loai; }
            set
            {
                loai = value;
                OnPropertyChanged();
            }
        }

        public MonAnNuocUong()
        {
        }

        public MonAnNuocUong(string ten, int gia, string loaiMon)
        {
            TenMon = ten;
            DonGia = gia;
            Loai = loaiMon;
        }
    }
}
