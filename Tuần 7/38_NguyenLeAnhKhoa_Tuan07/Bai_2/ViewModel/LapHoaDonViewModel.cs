using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.ViewModel
{
    internal class LapHoaDonViewModel : BaseViewModel
    {
        private readonly ObservableCollection<HoaDon> hoaDonDaThanhToan;
        private readonly Action sauKhiThanhToan;

        public ObservableCollection<MonDuocChon> DanhSachNuocUong { get; set; }
        public ObservableCollection<MonDuocChon> DanhSachThucAn { get; set; }
        public ObservableCollection<ChiTietHoaDon> ChiTietHoaDonDangLap { get; set; }

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

        private string soKhach;
        public string SoKhach
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
                CapNhatThongTinHoaDon();
            }
        }

        private bool isBan01;
        public bool IsBan01
        {
            get { return isBan01; }
            set
            {
                if (isBan01 == value) return;
                isBan01 = value;
                if (value) ChonBan("Bàn 01");
                else NeuBoChonBan("Bàn 01");
                OnPropertyChanged();
            }
        }

        private bool isBan02;
        public bool IsBan02
        {
            get { return isBan02; }
            set
            {
                if (isBan02 == value) return;
                isBan02 = value;
                if (value) ChonBan("Bàn 02");
                else NeuBoChonBan("Bàn 02");
                OnPropertyChanged();
            }
        }

        private bool isBan03;
        public bool IsBan03
        {
            get { return isBan03; }
            set
            {
                if (isBan03 == value) return;
                isBan03 = value;
                if (value) ChonBan("Bàn 03");
                else NeuBoChonBan("Bàn 03");
                OnPropertyChanged();
            }
        }

        private bool isBan04;
        public bool IsBan04
        {
            get { return isBan04; }
            set
            {
                if (isBan04 == value) return;
                isBan04 = value;
                if (value) ChonBan("Bàn 04");
                else NeuBoChonBan("Bàn 04");
                OnPropertyChanged();
            }
        }

        private string banDangChon;
        public string BanDangChon
        {
            get { return banDangChon; }
            set
            {
                banDangChon = value;
                OnPropertyChanged();
            }
        }

        private string thongBao;
        public string ThongBao
        {
            get { return thongBao; }
            set
            {
                thongBao = value;
                OnPropertyChanged();
            }
        }

        private string maHoaDonTam;
        public string MaHoaDonTam
        {
            get { return maHoaDonTam; }
            set
            {
                maHoaDonTam = value;
                OnPropertyChanged();
            }
        }

        private int tongTamTinh;
        public int TongTamTinh
        {
            get { return tongTamTinh; }
            set
            {
                tongTamTinh = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongTamTinhText));
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
                OnPropertyChanged(nameof(GiamGiaText));
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
                OnPropertyChanged(nameof(ThanhToanText));
            }
        }

        public string TongTamTinhText
        {
            get { return TongTamTinh.ToString("N0"); }
        }

        public string GiamGiaText
        {
            get { return GiamGia.ToString("N0"); }
        }

        public string ThanhToanText
        {
            get { return ThanhToan.ToString("N0"); }
        }

        public RelayCommand TaoHoaDonCommand { get; set; }
        public RelayCommand NhapLaiCommand { get; set; }
        public RelayCommand ThanhToanCommand { get; set; }
        public RelayCommand ThoatCommand { get; set; }

        public LapHoaDonViewModel(ObservableCollection<HoaDon> danhSachHoaDon, Action callbackSauThanhToan)
        {
            hoaDonDaThanhToan = danhSachHoaDon;
            sauKhiThanhToan = callbackSauThanhToan;

            DanhSachNuocUong = new ObservableCollection<MonDuocChon>
            {
                new MonDuocChon(new MonAnNuocUong("Cafe đen", 20000, "Nước uống"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Cafe sữa", 25000, "Nước uống"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Cafe đá", 22000, "Nước uống"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Cafe kem", 30000, "Nước uống"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Cafe sữa đá", 28000, "Nước uống"), CapNhatThongTinHoaDon)
            };

            DanhSachThucAn = new ObservableCollection<MonDuocChon>
            {
                new MonDuocChon(new MonAnNuocUong("Bánh mỳ trứng", 15000, "Thức ăn"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Bánh mỳ cá", 15000, "Thức ăn"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Mỳ tôm trứng", 20000, "Thức ăn"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Mỳ xào bò", 40000, "Thức ăn"), CapNhatThongTinHoaDon),
                new MonDuocChon(new MonAnNuocUong("Mỳ cay", 50000, "Thức ăn"), CapNhatThongTinHoaDon)
            };

            ChiTietHoaDonDangLap = new ObservableCollection<ChiTietHoaDon>();

            TaoHoaDonCommand = new RelayCommand(o => TaoHoaDonTam(), o => true);
            NhapLaiCommand = new RelayCommand(o => NhapLai(), o => true);
            ThanhToanCommand = new RelayCommand(o => ThanhToanHoaDon(), o => true);
            ThoatCommand = new RelayCommand(o => Thoat(), o => true);

            NhapLai();
        }

        private void ChonBan(string tenBan)
        {
            BanDangChon = tenBan;
            if (tenBan != "Bàn 01") isBan01 = false;
            if (tenBan != "Bàn 02") isBan02 = false;
            if (tenBan != "Bàn 03") isBan03 = false;
            if (tenBan != "Bàn 04") isBan04 = false;
            OnPropertyChanged(nameof(IsBan01));
            OnPropertyChanged(nameof(IsBan02));
            OnPropertyChanged(nameof(IsBan03));
            OnPropertyChanged(nameof(IsBan04));
            CapNhatThongTinHoaDon();
        }

        private void NeuBoChonBan(string tenBan)
        {
            if (BanDangChon == tenBan)
            {
                BanDangChon = "";
                CapNhatThongTinHoaDon();
            }
        }

        private bool ValidateThongTin(out string loi, out List<ChiTietHoaDon> danhSachMon)
        {
            loi = "";
            danhSachMon = new List<ChiTietHoaDon>();

            int soKhachHang;

            if (string.IsNullOrWhiteSpace(TenKhachHang))
            {
                loi = "Tên khách hàng không được trống";
                return false;
            }

            if (string.IsNullOrWhiteSpace(SoDienThoai))
            {
                loi = "Số điện thoại không được trống";
                return false;
            }

            if (!int.TryParse(SoKhach, out soKhachHang) || soKhachHang <= 0)
            {
                loi = "Số khách phải là số nguyên > 0";
                return false;
            }

            if (string.IsNullOrWhiteSpace(BanDangChon))
            {
                loi = "Vui lòng chọn 1 bàn";
                return false;
            }

            if (DanhSachNuocUong == null) DanhSachNuocUong = new ObservableCollection<MonDuocChon>();
            if (DanhSachThucAn == null) DanhSachThucAn = new ObservableCollection<MonDuocChon>();

            ThemMonHopLe(DanhSachNuocUong, danhSachMon, ref loi);
            if (!string.IsNullOrWhiteSpace(loi)) return false;

            ThemMonHopLe(DanhSachThucAn, danhSachMon, ref loi);
            if (!string.IsNullOrWhiteSpace(loi)) return false;

            if (danhSachMon.Count == 0)
            {
                loi = "Vui lòng chọn tối thiểu 1 món ăn hoặc 1 nước uống";
                return false;
            }

            return true;
        }

        private void ThemMonHopLe(IEnumerable<MonDuocChon> danhSachNguon, ICollection<ChiTietHoaDon> dich, ref string loi)
        {
            foreach (var item in danhSachNguon)
            {
                if (!item.IsChon) continue;
                if (item.SoLuongHopLe <= 0)
                {
                    loi = "Số lượng của món được chọn phải > 0";
                    return;
                }

                dich.Add(new ChiTietHoaDon(item.Mon.TenMon, item.SoLuongHopLe, item.Mon.DonGia));
            }
        }

        private void TaoHoaDonTam()
        {
            string loi;
            List<ChiTietHoaDon> danhSachMon;
            if (!ValidateThongTin(out loi, out danhSachMon))
            {
                MessageBox.Show(loi);
                ThongBao = loi;
                return;
            }

            var khach = new KhachHang(TenKhachHang.Trim(), SoDienThoai.Trim(), int.Parse(SoKhach), LaSinhVien);
            var hoaDon = new HoaDon("TẠM", DateTime.Now, khach, BanDangChon, new ObservableCollection<ChiTietHoaDon>(danhSachMon));

            ChiTietHoaDonDangLap.Clear();
            foreach (var item in hoaDon.DanhSachChiTiet)
            {
                ChiTietHoaDonDangLap.Add(item);
            }

            TongTamTinh = hoaDon.TongTamTinh;
            GiamGia = hoaDon.GiamGia;
            ThanhToan = hoaDon.ThanhToan;
            MaHoaDonTam = "TẠM";
            ThongBao = "Đã tạo hóa đơn tạm";
        }

        private void ThanhToanHoaDon()
        {
            string loi;
            List<ChiTietHoaDon> danhSachMon;
            if (!ValidateThongTin(out loi, out danhSachMon))
            {
                MessageBox.Show(loi);
                ThongBao = loi;
                return;
            }

            var khach = new KhachHang(TenKhachHang.Trim(), SoDienThoai.Trim(), int.Parse(SoKhach), LaSinhVien);
            string maHoaDon = TaoMaHoaDon();
            var hoaDon = new HoaDon(maHoaDon, DateTime.Now, khach, BanDangChon, new ObservableCollection<ChiTietHoaDon>(danhSachMon));

            hoaDonDaThanhToan.Add(hoaDon);
            ThongBao = "Thanh toán thành công";
            MessageBox.Show("Thanh toán thành công");
            NhapLai();
            sauKhiThanhToan?.Invoke();
        }

        private string TaoMaHoaDon()
        {
            return "HD" + DateTime.Now.ToString("yyyyMMddHHmmss") + (hoaDonDaThanhToan.Count + 1).ToString("000");
        }

        private void NhapLai()
        {
            TenKhachHang = "";
            SoDienThoai = "";
            SoKhach = "";
            LaSinhVien = false;

            isBan01 = false;
            isBan02 = false;
            isBan03 = false;
            isBan04 = false;
            BanDangChon = "";
            OnPropertyChanged(nameof(IsBan01));
            OnPropertyChanged(nameof(IsBan02));
            OnPropertyChanged(nameof(IsBan03));
            OnPropertyChanged(nameof(IsBan04));

            foreach (var item in DanhSachNuocUong)
            {
                item.IsChon = false;
                item.SoLuong = "";
            }

            foreach (var item in DanhSachThucAn)
            {
                item.IsChon = false;
                item.SoLuong = "";
            }

            ChiTietHoaDonDangLap.Clear();
            TongTamTinh = 0;
            GiamGia = 0;
            ThanhToan = 0;
            MaHoaDonTam = "";
            ThongBao = "Sẵn sàng lập hóa đơn";
        }

        private void Thoat()
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void CapNhatThongTinHoaDon()
        {
            if (DanhSachNuocUong == null) DanhSachNuocUong = new ObservableCollection<MonDuocChon>();
            if (DanhSachThucAn == null) DanhSachThucAn = new ObservableCollection<MonDuocChon>();

            int tong = 0;
            foreach (var item in DanhSachNuocUong)
            {
                if (item.IsChon && item.SoLuongHopLe > 0)
                {
                    tong += item.SoLuongHopLe * item.Mon.DonGia;
                }
            }

            foreach (var item in DanhSachThucAn)
            {
                if (item.IsChon && item.SoLuongHopLe > 0)
                {
                    tong += item.SoLuongHopLe * item.Mon.DonGia;
                }
            }

            TongTamTinh = tong;
            GiamGia = LaSinhVien ? (tong * 20) / 100 : 0;
            ThanhToan = TongTamTinh - GiamGia;
        }
    }

    internal class MonDuocChon : BaseViewModel
    {
        private readonly Action callback;

        public MonAnNuocUong Mon { get; set; }

        private bool isChon;
        public bool IsChon
        {
            get { return isChon; }
            set
            {
                isChon = value;
                OnPropertyChanged();
                if (!isChon) SoLuong = "";
                callback?.Invoke();
            }
        }

        private string soLuong;
        public string SoLuong
        {
            get { return soLuong; }
            set
            {
                soLuong = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SoLuongHopLe));
                callback?.Invoke();
            }
        }

        public int SoLuongHopLe
        {
            get
            {
                int so;
                if (!int.TryParse(SoLuong, out so) || so < 0) return 0;
                return so;
            }
        }

        public string TenMonHienThi
        {
            get { return Mon.TenMon + " - " + Mon.DonGia.ToString("N0"); }
        }

        public MonDuocChon(MonAnNuocUong mon, Action callbackCapNhat)
        {
            Mon = mon;
            callback = callbackCapNhat;
            SoLuong = "";
        }
    }
}
