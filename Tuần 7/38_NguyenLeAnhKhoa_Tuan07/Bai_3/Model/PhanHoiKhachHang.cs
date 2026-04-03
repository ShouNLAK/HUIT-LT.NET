using System;
using System.Collections.ObjectModel;
using System.Linq;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model
{
    internal class ChiTietTraLoi : BaseViewModel
    {
        private int maCauHoi;
        public int MaCauHoi
        {
            get { return maCauHoi; }
            set
            {
                maCauHoi = value;
                OnPropertyChanged();
            }
        }

        private string noiDungCauHoi;
        public string NoiDungCauHoi
        {
            get { return noiDungCauHoi; }
            set
            {
                noiDungCauHoi = value;
                OnPropertyChanged();
            }
        }

        private string dapAn;
        public string DapAn
        {
            get { return dapAn; }
            set
            {
                dapAn = value;
                OnPropertyChanged();
            }
        }

        private int diem;
        public int Diem
        {
            get { return diem; }
            set
            {
                diem = value;
                OnPropertyChanged();
            }
        }

        public ChiTietTraLoi()
        {
        }

        public ChiTietTraLoi(int ma, string noiDung, string dapAnLuaChon, int diemSo)
        {
            MaCauHoi = ma;
            NoiDungCauHoi = noiDung;
            DapAn = dapAnLuaChon;
            Diem = diemSo;
        }
    }

    internal class PhanHoiKhachHang : BaseViewModel
    {
        private string maPhanHoi;
        public string MaPhanHoi
        {
            get { return maPhanHoi; }
            set
            {
                maPhanHoi = value;
                OnPropertyChanged();
            }
        }

        private DateTime ngayPhanHoi;
        public DateTime NgayPhanHoi
        {
            get { return ngayPhanHoi; }
            set
            {
                ngayPhanHoi = value;
                OnPropertyChanged();
            }
        }

        private KhachHang thongTinKhach;
        public KhachHang ThongTinKhach
        {
            get { return thongTinKhach; }
            set
            {
                thongTinKhach = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChiTietTraLoi> DanhSachTraLoi { get; set; }

        private string gopYThem;
        public string GopYThem
        {
            get { return gopYThem; }
            set
            {
                gopYThem = value;
                OnPropertyChanged();
            }
        }

        private float diemTrungBinh;
        public float DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set
            {
                diemTrungBinh = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DiemTrungBinhText));
            }
        }

        private bool daXuLy;
        public bool DaXuLy
        {
            get { return daXuLy; }
            set
            {
                daXuLy = value;
                OnPropertyChanged();
            }
        }

        public string DiemTrungBinhText
        {
            get { return DiemTrungBinh.ToString("0.00"); }
        }

        public PhanHoiKhachHang()
        {
            DanhSachTraLoi = new ObservableCollection<ChiTietTraLoi>();
        }

        public PhanHoiKhachHang(string ma, DateTime ngay, KhachHang khach, ObservableCollection<ChiTietTraLoi> danhSachTraLoi, string gopY, bool daXuLy)
        {
            MaPhanHoi = ma;
            NgayPhanHoi = ngay;
            ThongTinKhach = khach;
            DanhSachTraLoi = danhSachTraLoi ?? new ObservableCollection<ChiTietTraLoi>();
            GopYThem = gopY;
            DaXuLy = daXuLy;
            DiemTrungBinh = DanhSachTraLoi.Count == 0 ? 0f : (float)DanhSachTraLoi.Average(x => x.Diem);
        }
    }
}
