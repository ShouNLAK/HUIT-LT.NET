using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NguyenLeAnhKhoa_2001240206_KT2.Helper;
using NguyenLeAnhKhoa_2001240206_KT2.Model;

namespace NguyenLeAnhKhoa_2001240206_KT2.ViewModel
{
    // Lớp phụ: chứa thông tin chi tiết phụ thu đã chọn
    public class ChiTietPhuThuVM
    {
        public string MaPhuThu { get; set; }
        public string TenPhuThu { get; set; }
        public decimal GiaPhuThu { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get { return GiaPhuThu * SoLuong; } }
    }

    public class BookingViewModel : BaseViewModel
    {
        private QL_KaraokeEntities db = new QL_KaraokeEntities();

        // Ngày đặt phòng
        private DateTime ngayDatPhong = DateTime.Now;
        public DateTime NgayDatPhong
        {
            get { return ngayDatPhong; }
            set
            {
                ngayDatPhong = value;
                OnPropertyChanged(nameof(NgayDatPhong));
            }
        }

        // Danh sách phòng
        public ObservableCollection<PHONG> DanhSachPhong { get; set; }

        private PHONG selectedPhong;
        public PHONG SelectedPhong
        {
            get { return selectedPhong; }
            set
            {
                selectedPhong = value;
                OnPropertyChanged(nameof(SelectedPhong));
                if (selectedPhong != null)
                {
                    GiaPhong = selectedPhong.GiaPhong != null ? selectedPhong.GiaPhong.Value.ToString() : "0";
                    SucChua = selectedPhong.SucChua != null ? selectedPhong.SucChua.Value.ToString() + " người" : string.Empty;
                }
                TinhTongTien();
            }
        }

        private string giaPhong;
        public string GiaPhong
        {
            get { return giaPhong; }
            set
            {
                giaPhong = value;
                OnPropertyChanged(nameof(GiaPhong));
            }
        }

        private string sucChua;
        public string SucChua
        {
            get { return sucChua; }
            set
            {
                sucChua = value;
                OnPropertyChanged(nameof(SucChua));
            }
        }

        // Danh sách khách hàng
        public ObservableCollection<KHACHHANG> DanhSachKhachHang { get; set; }

        private KHACHHANG selectedKhachHang;
        public KHACHHANG SelectedKhachHang
        {
            get { return selectedKhachHang; }
            set
            {
                selectedKhachHang = value;
                OnPropertyChanged(nameof(SelectedKhachHang));
            }
        }

        // Giờ vào / ra
        private string gioVao = "13:00";
        public string GioVao
        {
            get { return gioVao; }
            set
            {
                gioVao = value;
                OnPropertyChanged(nameof(GioVao));
                TinhTongTien();
            }
        }

        private string gioRa = "15:00";
        public string GioRa
        {
            get { return gioRa; }
            set
            {
                gioRa = value;
                OnPropertyChanged(nameof(GioRa));
                TinhTongTien();
            }
        }

        // Phụ thu
        public ObservableCollection<PHUTHU> DanhSachPhuThu { get; set; }

        private PHUTHU selectedPhuThu;
        public PHUTHU SelectedPhuThu
        {
            get { return selectedPhuThu; }
            set
            {
                selectedPhuThu = value;
                OnPropertyChanged(nameof(SelectedPhuThu));
                if (selectedPhuThu != null)
                    GiaPhuThu = selectedPhuThu.GiaPT != null ? selectedPhuThu.GiaPT.Value.ToString() : "0";
            }
        }

        private string giaPhuThu;
        public string GiaPhuThu
        {
            get { return giaPhuThu; }
            set
            {
                giaPhuThu = value;
                OnPropertyChanged(nameof(GiaPhuThu));
            }
        }

        private string soLuongPhuThu = "1";
        public string SoLuongPhuThu
        {
            get { return soLuongPhuThu; }
            set
            {
                soLuongPhuThu = value;
                OnPropertyChanged(nameof(SoLuongPhuThu));
            }
        }

        // Chi tiết phụ thu đã chọn
        public ObservableCollection<ChiTietPhuThuVM> DanhSachChiTiet { get; set; }

        // Tổng tiền tạm tính
        private decimal tongTienTamTinh;
        public decimal TongTienTamTinh
        {
            get { return tongTienTamTinh; }
            set
            {
                tongTienTamTinh = value;
                OnPropertyChanged(nameof(TongTienTamTinh));
            }
        }

        public RelayCommand ThemPhuThuCommand { get; set; }
        public RelayCommand DatPhongCommand { get; set; }

        public BookingViewModel()
        {
            LoadDuLieu();
            DanhSachChiTiet = new ObservableCollection<ChiTietPhuThuVM>();

            ThemPhuThuCommand = new RelayCommand(o => ThemPhuThu());
            DatPhongCommand = new RelayCommand(o => DatPhong());
        }

        private void LoadDuLieu()
        {
            DanhSachPhong = new ObservableCollection<PHONG>(db.PHONGs.ToList());
            DanhSachKhachHang = new ObservableCollection<KHACHHANG>(db.KHACHHANGs.ToList());
            DanhSachPhuThu = new ObservableCollection<PHUTHU>(db.PHUTHUs.ToList());
            OnPropertyChanged(nameof(DanhSachPhong));
            OnPropertyChanged(nameof(DanhSachKhachHang));
            OnPropertyChanged(nameof(DanhSachPhuThu));
        }

        private void ThemPhuThu()
        {
            if (SelectedPhuThu == null)
            {
                MessageBox.Show("Chọn phụ thu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SoLuongPhuThu, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải > 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ChiTietPhuThuVM item = new ChiTietPhuThuVM();
            item.MaPhuThu = SelectedPhuThu.MaPhuThu;
            item.TenPhuThu = SelectedPhuThu.TenPhuThu;
            item.GiaPhuThu = SelectedPhuThu.GiaPT != null ? SelectedPhuThu.GiaPT.Value : 0;
            item.SoLuong = sl;
            DanhSachChiTiet.Add(item);

            TinhTongTien();
        }

        private void DatPhong()
        {
            if (SelectedPhong == null)
            {
                MessageBox.Show("Chọn phòng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedKhachHang == null)
            {
                MessageBox.Show("Chọn khách hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!TimeSpan.TryParse(GioVao, out TimeSpan gv) || !TimeSpan.TryParse(GioRa, out TimeSpan gr))
            {
                MessageBox.Show("Giờ vào/ra không hợp lệ (định dạng HH:mm).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (gr <= gv)
            {
                MessageBox.Show("Giờ ra phải sau giờ vào.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Tạo mã đặt phòng tự động (tăng dần)
                string maDatPhong = TaoMaDatPhong();

                DATPHONG datPhong = new DATPHONG();
                datPhong.MaDatPhong = maDatPhong;
                datPhong.MaPh = SelectedPhong.MaPhong;
                datPhong.MaKH = SelectedKhachHang.MaKhachHang;
                datPhong.NgayDat = NgayDatPhong.Date.Add(gv);
                datPhong.NgayTra = NgayDatPhong.Date.Add(gr);
                db.DATPHONGs.Add(datPhong);

                // Thêm chi tiết phụ thu (mã CT tự động tăng dần từ DB)
                int soTT = TaoSoThuTuCT();
                foreach (var ct in DanhSachChiTiet)
                {
                    CHITIETDATPHONG chiTiet = new CHITIETDATPHONG();
                    chiTiet.MaCT = "CT" + soTT.ToString("D3");
                    chiTiet.MaDP = maDatPhong;
                    chiTiet.MaPT = ct.MaPhuThu;
                    chiTiet.SL = ct.SoLuong;
                    db.CHITIETDATPHONGs.Add(chiTiet);
                    soTT++;
                }

                db.SaveChanges();
                MessageBox.Show("Đặt phòng thành công! Mã đặt phòng: " + maDatPhong, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset sau khi đặt phòng
                DanhSachChiTiet.Clear();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt phòng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TinhTongTien()
        {
            decimal tongPhuThu = 0;
            foreach (var ct in DanhSachChiTiet)
            {
                tongPhuThu += ct.GiaPhuThu * ct.SoLuong;
            }

            decimal tienHat = 0;
            if (SelectedPhong != null &&
                TimeSpan.TryParse(GioVao, out TimeSpan gv) &&
                TimeSpan.TryParse(GioRa, out TimeSpan gr) &&
                gr > gv)
            {
                decimal soGio = (decimal)(gr - gv).TotalHours;
                decimal giaPhongVal = SelectedPhong.GiaPhong != null ? SelectedPhong.GiaPhong.Value : 0;
                tienHat = soGio * giaPhongVal;
            }

            TongTienTamTinh = tongPhuThu + tienHat;
        }

        private string TaoMaDatPhong()
        {
            int maxSo = 0;
            var dsMa = db.DATPHONGs.Select(x => x.MaDatPhong).ToList();
            foreach (var ma in dsMa)
            {
                if (string.IsNullOrWhiteSpace(ma) || ma.Length < 3 || !ma.StartsWith("DP"))
                    continue;
                string so = ma.Substring(2);
                if (int.TryParse(so, out int soHienTai) && soHienTai > maxSo)
                    maxSo = soHienTai;
            }
            return "DP" + (maxSo + 1).ToString("D3");
        }

        private int TaoSoThuTuCT()
        {
            int maxSo = 0;
            var dsMa = db.CHITIETDATPHONGs.Select(x => x.MaCT).ToList();
            foreach (var ma in dsMa)
            {
                if (string.IsNullOrWhiteSpace(ma) || ma.Length < 3 || !ma.StartsWith("CT"))
                    continue;
                string so = ma.Substring(2);
                if (int.TryParse(so, out int soHienTai) && soHienTai > maxSo)
                    maxSo = soHienTai;
            }
            return maxSo + 1;
        }
    }
}
