using System;
using System.Collections.ObjectModel;
using System.Linq;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model
{
    internal class ChiTietHoaDon : BaseViewModel
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

        private int soLuong;
        public int SoLuong
        {
            get { return soLuong; }
            set
            {
                soLuong = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ThanhTien));
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
                OnPropertyChanged(nameof(ThanhTien));
            }
        }

        public int ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        public ChiTietHoaDon()
        {
        }

        public ChiTietHoaDon(string tenMonAn, int soLuongMon, int gia)
        {
            TenMon = tenMonAn;
            SoLuong = soLuongMon;
            DonGia = gia;
        }
    }

    internal class HoaDon : BaseViewModel
    {
        private string maHoaDon;
        public string MaHoaDon
        {
            get { return maHoaDon; }
            set
            {
                maHoaDon = value;
                OnPropertyChanged();
            }
        }

        private DateTime ngayLap;
        public DateTime NgayLap
        {
            get { return ngayLap; }
            set
            {
                ngayLap = value;
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

        private string ban;
        public string Ban
        {
            get { return ban; }
            set
            {
                ban = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChiTietHoaDon> DanhSachChiTiet { get; set; }

        private int tongTamTinh;
        public int TongTamTinh
        {
            get { return tongTamTinh; }
            set
            {
                tongTamTinh = value;
                OnPropertyChanged();
            }
        }

        private int giamGia;
        public int GiamGia
        {
            get { return giamGia; }
            set
            {
                giamGia = value;
                OnPropertyChanged();
            }
        }

        private int thanhToan;
        public int ThanhToan
        {
            get { return thanhToan; }
            set
            {
                thanhToan = value;
                OnPropertyChanged();
            }
        }

        public HoaDon()
        {
            DanhSachChiTiet = new ObservableCollection<ChiTietHoaDon>();
        }

        public HoaDon(string ma, DateTime ngay, KhachHang khach, string banAn, ObservableCollection<ChiTietHoaDon> chiTiet)
        {
            MaHoaDon = ma;
            NgayLap = ngay;
            ThongTinKhach = khach;
            Ban = banAn;
            DanhSachChiTiet = chiTiet ?? new ObservableCollection<ChiTietHoaDon>();
            TongTamTinh = DanhSachChiTiet.Sum(x => x.ThanhTien);
            GiamGia = ThongTinKhach != null && ThongTinKhach.LaSinhVien ? (TongTamTinh * 20) / 100 : 0;
            ThanhToan = TongTamTinh - GiamGia;
        }
    }
}
