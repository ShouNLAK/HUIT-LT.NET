using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.ViewModel
{
    internal class CauHoiTraLoiItem : BaseViewModel
    {
        public CauHoiKhaoSat CauHoi { get; set; }

        public string TieuDeCauHoi
        {
            get { return "Câu hỏi " + CauHoi.MaCauHoi; }
        }

        public string NoiDungCauHoi
        {
            get { return CauHoi.NoiDung; }
        }

        public string TenNhomLuaChon
        {
            get { return "NhomCauHoi_" + CauHoi.MaCauHoi; }
        }

        private string dapAnDaChon;
        public string DapAnDaChon
        {
            get { return dapAnDaChon; }
            set
            {
                if (dapAnDaChon == value) return;
                dapAnDaChon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ChonRatHaiLong));
                OnPropertyChanged(nameof(ChonHaiLong));
                OnPropertyChanged(nameof(ChonBinhThuong));
                OnPropertyChanged(nameof(ChonKhongHaiLong));
                OnPropertyChanged(nameof(HienThiTomTat));
                OnPropertyChanged(nameof(ThongTinGoiY));
            }
        }

        public bool ChonRatHaiLong
        {
            get { return DapAnDaChon == "Rất hài lòng"; }
            set { if (value) DapAnDaChon = "Rất hài lòng"; }
        }

        public bool ChonHaiLong
        {
            get { return DapAnDaChon == "Hài lòng"; }
            set { if (value) DapAnDaChon = "Hài lòng"; }
        }

        public bool ChonBinhThuong
        {
            get { return DapAnDaChon == "Bình thường"; }
            set { if (value) DapAnDaChon = "Bình thường"; }
        }

        public bool ChonKhongHaiLong
        {
            get { return DapAnDaChon == "Không hài lòng"; }
            set { if (value) DapAnDaChon = "Không hài lòng"; }
        }

        public string HienThiTomTat
        {
            get
            {
                string dapAn = string.IsNullOrWhiteSpace(DapAnDaChon) ? "..." : DapAnDaChon;
                return TieuDeCauHoi + ": " + NoiDungCauHoi + " - Đáp án: " + dapAn;
            }
        }

        public string ThongTinGoiY
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DapAnDaChon))
                {
                    return "Gợi ý: chọn 1 đáp án";
                }

                return "Đã chọn: " + DapAnDaChon;
            }
        }
    }

    internal class KhaoSatGopYViewModel : BaseViewModel
    {
        private readonly MainViewModel mainViewModel;

        public ObservableCollection<CauHoiTraLoiItem> DanhSachCauHoiTraLoi { get; set; }

        private CauHoiTraLoiItem cauHoiDangChon;
        public CauHoiTraLoiItem CauHoiDangChon
        {
            get { return cauHoiDangChon; }
            set
            {
                cauHoiDangChon = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand GuiPhanHoiCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }

        public KhaoSatGopYViewModel(MainViewModel mainVm)
        {
            mainViewModel = mainVm;
            DanhSachCauHoiTraLoi = new ObservableCollection<CauHoiTraLoiItem>();

            GuiPhanHoiCommand = new RelayCommand((p) => GuiPhanHoi(), (p) => true);
            NhapLaiCommand = new RelayCommand((p) => NhapLai(), (p) => true);

            KhoiTaoDanhSachCauHoi();
            DongBoThongTinKhachHang();
        }

        public void DongBoThongTinKhachHang()
        {
            if (mainViewModel.KhachHangHienTai == null)
            {
                TenKhachHang = string.Empty;
                SoDienThoai = string.Empty;
                Email = string.Empty;
                return;
            }

            TenKhachHang = mainViewModel.KhachHangHienTai.TenKhachHang;
            SoDienThoai = mainViewModel.KhachHangHienTai.SoDienThoai;
            Email = mainViewModel.KhachHangHienTai.Email;
        }

        private void KhoiTaoDanhSachCauHoi()
        {
            DanhSachCauHoiTraLoi.Clear();
            foreach (var cauHoi in mainViewModel.DanhSachCauHoiMau)
            {
                DanhSachCauHoiTraLoi.Add(new CauHoiTraLoiItem
                {
                    CauHoi = cauHoi,
                    DapAnDaChon = string.Empty
                });
            }

            if (DanhSachCauHoiTraLoi.Count > 0)
            {
                CauHoiDangChon = DanhSachCauHoiTraLoi[0];
            }
        }

        private void NhapLai()
        {
            foreach (var item in DanhSachCauHoiTraLoi)
            {
                item.DapAnDaChon = string.Empty;
            }

            GopYThem = string.Empty;

            if (DanhSachCauHoiTraLoi.Count > 0)
            {
                CauHoiDangChon = DanhSachCauHoiTraLoi[0];
            }
        }

        private void GuiPhanHoi()
        {
            if (!CoTheGuiPhanHoi()) return;

            ObservableCollection<ChiTietTraLoi> danhSachTraLoi = TaoDanhSachTraLoi();
            string maPhanHoiMoi = TaoMaPhanHoiMoi();

            PhanHoiKhachHang phanHoiMoi = new PhanHoiKhachHang(
                maPhanHoiMoi,
                DateTime.Now,
                new KhachHang(mainViewModel.KhachHangHienTai.TenKhachHang, mainViewModel.KhachHangHienTai.SoDienThoai, mainViewModel.KhachHangHienTai.Email),
                danhSachTraLoi,
                GopYThem,
                false);

            mainViewModel.DanhSachPhanHoi.Insert(0, phanHoiMoi);
            mainViewModel.DongBoThongKe();

            MessageBox.Show("Gửi phản hồi thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            NhapLai();
        }

        private bool CoTheGuiPhanHoi()
        {
            if (mainViewModel.KhachHangHienTai == null)
            {
                MessageBox.Show("Bạn cần lưu thông tin khách hàng trước khi gửi phản hồi.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            bool coCauHoiChuaTraLoi = DanhSachCauHoiTraLoi.Any(x => string.IsNullOrWhiteSpace(x.DapAnDaChon));
            if (coCauHoiChuaTraLoi)
            {
                MessageBox.Show("Vui lòng trả lời đầy đủ tất cả câu hỏi.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private ObservableCollection<ChiTietTraLoi> TaoDanhSachTraLoi()
        {
            ObservableCollection<ChiTietTraLoi> danhSachTraLoi = new ObservableCollection<ChiTietTraLoi>();

            foreach (CauHoiTraLoiItem item in DanhSachCauHoiTraLoi)
            {
                int diem = QuyDoiDiem(item.DapAnDaChon);
                ChiTietTraLoi chiTiet = new ChiTietTraLoi(item.CauHoi.MaCauHoi, item.CauHoi.NoiDung, item.DapAnDaChon, diem);
                danhSachTraLoi.Add(chiTiet);
            }

            return danhSachTraLoi;
        }

        private string TaoMaPhanHoiMoi()
        {
            int soThuTu = mainViewModel.DanhSachPhanHoi.Count + 1;
            return "PH" + soThuTu.ToString("000");
        }

        private int QuyDoiDiem(string dapAn)
        {
            if (dapAn == "Rất hài lòng") return 4;
            if (dapAn == "Hài lòng") return 3;
            if (dapAn == "Bình thường") return 2;
            return 1;
        }
    }
}
