using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan12.Helper;
using _38_NguyenLeAnhKhoa_Tuan12.Model;
using _38_NguyenLeAnhKhoa_Tuan12.View;

namespace _38_NguyenLeAnhKhoa_Tuan12.ViewModel
{
    internal class FormViewModel : BaseViewModel
    {
        public RelayCommand InPhieuNhapCommand { get; set; }
        private string tenNhanVien;
        public string TenNhanVien
        {
            get { return tenNhanVien; }
            set {
                tenNhanVien = value;
                OnPropertyChanged();
            }
        }

        private string vaiTro;
        public string VaiTro
        {
            get { return vaiTro; }
            set
            {
                vaiTro = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsQuanLy));
                OnPropertyChanged(nameof(IsNhanVienKho));
                OnPropertyChanged(nameof(IsNhanVienBanHang));
                OnPropertyChanged(nameof(IsNhapHangVisible));
                OnPropertyChanged(nameof(IsThongKeNhapVisible));
                OnPropertyChanged(nameof(IsThongKeTonKhoVisible));
                OnPropertyChanged(nameof(IsThongKeVisible));
            }
        }

        private NhanVien currentNhanVien;
        public NhanVien CurrentNhanVien
        {
            get { return currentNhanVien; }
            set
            {
                currentNhanVien = value;
                OnPropertyChanged(nameof(CurrentNhanVien));
            }
        }

        public bool IsQuanLy => (VaiTro ?? string.Empty).Trim() == "Quản lý";
        public bool IsNhanVienKho => (VaiTro ?? string.Empty).Trim() == "Nhân viên kho";
        public bool IsNhanVienBanHang => (VaiTro ?? string.Empty).Trim() == "Nhân viên bán hàng";

        public bool IsNhapHangVisible => IsQuanLy || IsNhanVienKho;
        public bool IsThongKeNhapVisible => IsQuanLy || IsNhanVienKho;
        public bool IsThongKeTonKhoVisible => IsQuanLy || IsNhanVienBanHang;
        public bool IsThongKeVisible => IsQuanLy || IsNhanVienKho || IsNhanVienBanHang;

        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public RelayCommand DangXuatCommand { get; set; }
        public RelayCommand MoPhieuNhapCommand { get; set; }
        public FormViewModel()
        {
            InPhieuNhapCommand = new RelayCommand(_ => InPhieuNhap());
            DangXuatCommand = new RelayCommand(p => DangXuat(p as Window));
            MoPhieuNhapCommand = new RelayCommand(p => MoPhieuNhap());
        }
        private void InPhieuNhap()
        {
            if (CurrentNhanVien == null)
            {
                MessageBox.Show("Không xác định được nhân viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UC_PhieuNhap view = CurrentView as UC_PhieuNhap;
            if (view == null || !(view.DataContext is PhieuNhapViewModel viewModel))
            {
                MessageBox.Show("Vui lòng mở phiếu nhập trước", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(viewModel.MaPhieuNhap))
            {
                MessageBox.Show("Chưa tạo phiếu nhập", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string maNCC = viewModel.SelectedNhaCungCap?.MANCC ?? string.Empty;
            if (string.IsNullOrWhiteSpace(maNCC))
            {
                MessageBox.Show("Chưa chọn nhà cung cấp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ViewInPhieuNhap frm = new ViewInPhieuNhap(viewModel.MaPhieuNhap, maNCC, viewModel.NgayNhap);
            frm.ShowDialog();
        }
        private void DangXuat(Window mainWindow)
        {
            W_DangNhap login = new W_DangNhap();
            login.Show();

            if (mainWindow != null)
                mainWindow.Close();
        }

        private void MoPhieuNhap()
        {
            UC_PhieuNhap view = new UC_PhieuNhap();
            view.DataContext = new PhieuNhapViewModel(CurrentNhanVien);
            CurrentView = view;
        }
    }

    internal class PhieuNhapViewModel : BaseViewModel
    {
        private QLHANGHOAEntities db = new QLHANGHOAEntities();

        public ObservableCollection<NHACUNGCAP> DS_NhaCungCap { get; set; }
        public ObservableCollection<SANPHAM> DS_SanPham { get; set; }
        public ObservableCollection<ChiTietPhieuNhapItem> DS_ChiTiet { get; set; }

        private NHACUNGCAP selectedNhaCungCap;
        public NHACUNGCAP SelectedNhaCungCap
        {
            get { return selectedNhaCungCap; }
            set
            {
                selectedNhaCungCap = value;
                OnPropertyChanged(nameof(SelectedNhaCungCap));
            }
        }

        private SANPHAM selectedSanPham;
        public SANPHAM SelectedSanPham
        {
            get { return selectedSanPham; }
            set
            {
                selectedSanPham = value;
                OnPropertyChanged(nameof(SelectedSanPham));
            }
        }

        private string soLuongNhap;
        public string SoLuongNhap
        {
            get { return soLuongNhap; }
            set
            {
                soLuongNhap = value;
                OnPropertyChanged(nameof(SoLuongNhap));
            }
        }

        private string donGiaNhap;
        public string DonGiaNhap
        {
            get { return donGiaNhap; }
            set
            {
                donGiaNhap = value;
                OnPropertyChanged(nameof(DonGiaNhap));
            }
        }

        private DateTime ngayNhap;
        public DateTime NgayNhap
        {
            get { return ngayNhap; }
            set
            {
                ngayNhap = value;
                OnPropertyChanged(nameof(NgayNhap));
            }
        }

        private string maPhieuNhap;
        public string MaPhieuNhap
        {
            get { return maPhieuNhap; }
            set
            {
                maPhieuNhap = value;
                OnPropertyChanged(nameof(MaPhieuNhap));
            }
        }

        private double tongTien;
        public double TongTien
        {
            get { return tongTien; }
            set
            {
                tongTien = value;
                OnPropertyChanged(nameof(TongTien));
            }
        }

        private bool daTaoPhieu;
        public bool DaTaoPhieu
        {
            get { return daTaoPhieu; }
            set
            {
                daTaoPhieu = value;
                OnPropertyChanged(nameof(DaTaoPhieu));
                OnPropertyChanged(nameof(IsTaoPhieuEnabled));
                OnPropertyChanged(nameof(IsChonNhaCungCapEnabled));
            }
        }

        public bool IsTaoPhieuEnabled => !DaTaoPhieu;
        public bool IsChonNhaCungCapEnabled => !DaTaoPhieu;
        public bool IsLuuPhieuEnabled => DS_ChiTiet != null && DS_ChiTiet.Count > 0;

        private NhanVien currentNhanVien;
        public NhanVien CurrentNhanVien
        {
            get { return currentNhanVien; }
            set
            {
                currentNhanVien = value;
                OnPropertyChanged(nameof(CurrentNhanVien));
            }
        }

        public RelayCommand TaoPhieuCommand { get; set; }
        public RelayCommand ThemChiTietCommand { get; set; }
        public RelayCommand LuuPhieuCommand { get; set; }
        public RelayCommand HuyPhieuCommand { get; set; }
        public RelayCommand XoaChiTietCommand { get; set; }
        public RelayCommand InPhieuNhapCommand { get; set; }

        public PhieuNhapViewModel(NhanVien nhanVien)
        {
            CurrentNhanVien = nhanVien;
            NgayNhap = DateTime.Today;
            TongTien = 0;
            DaTaoPhieu = false;

            LoadDuLieu();
            DS_ChiTiet = new ObservableCollection<ChiTietPhieuNhapItem>();

            TaoPhieuCommand = new RelayCommand(o => TaoPhieu());
            ThemChiTietCommand = new RelayCommand(o => ThemChiTiet());
            LuuPhieuCommand = new RelayCommand(o => LuuPhieu());
            HuyPhieuCommand = new RelayCommand(o => HuyPhieu());
            XoaChiTietCommand = new RelayCommand(o => XoaChiTiet(o as ChiTietPhieuNhapItem));
            InPhieuNhapCommand = new RelayCommand(o => InPhieu());
        }

        private void LoadDuLieu()
        {
            DS_NhaCungCap = new ObservableCollection<NHACUNGCAP>(db.NHACUNGCAPs.ToList());
            DS_SanPham = new ObservableCollection<SANPHAM>(db.SANPHAMs.ToList());
            OnPropertyChanged(nameof(DS_NhaCungCap));
            OnPropertyChanged(nameof(DS_SanPham));
        }

        private void TaoPhieu()
        {
            if (SelectedNhaCungCap == null)
            {
                MessageBox.Show("Chọn nhà cung cấp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MaPhieuNhap = TaoMaPhieuNhap();
            NgayNhap = DateTime.Today;
            DaTaoPhieu = true;
        }

        private string TaoMaPhieuNhap()
        {
            int maxSo = 0;
            var dsMa = db.PHIEUNHAPs.Select(p => p.MAPHIEUNHAP).ToList();
            foreach (var ma in dsMa)
            {
                if (string.IsNullOrWhiteSpace(ma) || !ma.StartsWith("PN"))
                    continue;

                var so = ma.Substring(2);
                if (int.TryParse(so, out int soHienTai) && soHienTai > maxSo)
                    maxSo = soHienTai;
            }

            return "PN" + (maxSo + 1).ToString("D6");
        }

        private void ThemChiTiet()
        {
            if (!DaTaoPhieu)
            {
                MessageBox.Show("Vui lòng tạo phiếu trước", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedSanPham == null)
            {
                MessageBox.Show("Chọn sản phẩm", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SoLuongNhap, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải > 0", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(DonGiaNhap, out double donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải > 0", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ChiTietPhieuNhapItem item = new ChiTietPhieuNhapItem();
            item.MaSanPham = SelectedSanPham.MASANPHAM;
            item.TenSanPham = SelectedSanPham.TENSANPHAM;
            item.SoLuong = soLuong;
            item.DonGia = donGia;
            item.ThanhTien = soLuong * donGia;

            DS_ChiTiet.Add(item);
            TongTien = DS_ChiTiet.Sum(x => x.ThanhTien);
            OnPropertyChanged(nameof(IsLuuPhieuEnabled));

            SoLuongNhap = string.Empty;
            DonGiaNhap = string.Empty;
        }

        private void XoaChiTiet(ChiTietPhieuNhapItem item)
        {
            if (item == null)
                return;

            var result = MessageBox.Show("Bạn có chắc muốn xóa chi tiết này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            DS_ChiTiet.Remove(item);
            TongTien = DS_ChiTiet.Sum(x => x.ThanhTien);
            OnPropertyChanged(nameof(IsLuuPhieuEnabled));
        }

        private void InPhieu()
        {
            if (string.IsNullOrWhiteSpace(MaPhieuNhap))
            {
                MessageBox.Show("Chưa tạo phiếu nhập", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string maNCC = SelectedNhaCungCap?.MANCC ?? string.Empty;
            if (string.IsNullOrWhiteSpace(maNCC))
            {
                MessageBox.Show("Chưa chọn nhà cung cấp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ViewInPhieuNhap frm = new ViewInPhieuNhap(MaPhieuNhap, maNCC, NgayNhap);
            frm.ShowDialog();
        }

        private void LuuPhieu()
        {
            if (!DaTaoPhieu)
            {
                MessageBox.Show("Vui lòng tạo phiếu trước", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DS_ChiTiet.Count == 0)
            {
                MessageBox.Show("Chưa có chi tiết phiếu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedNhaCungCap == null)
            {
                MessageBox.Show("Chọn nhà cung cấp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CurrentNhanVien == null)
            {
                MessageBox.Show("Không xác định được nhân viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PHIEUNHAP phieuNhap = new PHIEUNHAP();
            phieuNhap.MAPHIEUNHAP = MaPhieuNhap;
            phieuNhap.MANCC = SelectedNhaCungCap.MANCC;
            phieuNhap.NGAYNHAP = NgayNhap;
            phieuNhap.THANHTIEN = TongTien;
            phieuNhap.MANV = CurrentNhanVien.MaNV;
            db.PHIEUNHAPs.Add(phieuNhap);

            foreach (var ct in DS_ChiTiet)
            {
                CHITIETPHIEUNHAP chiTiet = new CHITIETPHIEUNHAP();
                chiTiet.MAPHIEUNHAP = MaPhieuNhap;
                chiTiet.MASANPHAM = ct.MaSanPham;
                chiTiet.DONGIA = ct.DonGia;
                chiTiet.SOLUONG = ct.SoLuong;
                db.CHITIETPHIEUNHAPs.Add(chiTiet);

                var tonKho = db.TONKHO_NGAY.FirstOrDefault(x => x.MASANPHAM == ct.MaSanPham);
                if (tonKho != null)
                    tonKho.SOLUONGTON += ct.SoLuong;
                else
                    db.TONKHO_NGAY.Add(new TONKHO_NGAY { MASANPHAM = ct.MaSanPham, SOLUONGTON = ct.SoLuong });
            }

            db.SaveChanges();
            MessageBox.Show("Lưu phiếu nhập thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            
            // Tự động mở cửa sổ in phiếu
            InPhieu();
            
            ResetPhieu();
        }

        private void HuyPhieu()
        {
            ResetPhieu();
        }

        private void ResetPhieu()
        {
            MaPhieuNhap = string.Empty;
            SelectedNhaCungCap = null;
            SelectedSanPham = null;
            SoLuongNhap = string.Empty;
            DonGiaNhap = string.Empty;
            TongTien = 0;
            NgayNhap = DateTime.Today;
            DaTaoPhieu = false;
            DS_ChiTiet.Clear();
            OnPropertyChanged(nameof(IsLuuPhieuEnabled));
        }
    }

    internal class ChiTietPhieuNhapItem
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}
