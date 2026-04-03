using System;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model
{
    internal class TaiKhoan : BaseViewModel
    {
        private string soTK;
        private string chuTK;
        private int soDu;
        private string loaiTK;
        private string trangThai;

        public string SoTK
        {
            get { return soTK; }
            set
            {
                soTK = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HienThi));
            }
        }
        public string ChuTK
        {
            get { return chuTK; }
            set
            {
                chuTK = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HienThi));
            }
        }
        public int SoDu
        {
            get { return soDu; }
            set
            {
                soDu = value;
                OnPropertyChanged();
            }
        }
        public string LoaiTK
        {
            get { return loaiTK; }
            set
            {
                loaiTK = value;
                OnPropertyChanged();
            }
        }
        public string TrangThai
        {
            get { return trangThai; }
            set
            {
                trangThai = value;
                OnPropertyChanged();
            }
        }
        public string HienThi
        {
            get { return SoTK + " - " + ChuTK; }
        }

        public TaiKhoan()
        {
        }

        public TaiKhoan(string so, string chu, int sodu,string loai,string trangthai)
        {
            SoTK = so;
            ChuTK = chu;
            SoDu = sodu;
            LoaiTK = loai;
            TrangThai = trangthai;
        }
    }
}
