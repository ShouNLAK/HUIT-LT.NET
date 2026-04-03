using System.Windows;
using System.Windows.Input;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.ViewModel
{
    internal class ThongTinKhachHangViewModel : BaseViewModel
    {
        private readonly MainViewModel mainViewModel;

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

        public ICommand LuuThongTinCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }

        public ThongTinKhachHangViewModel(MainViewModel mainVm)
        {
            mainViewModel = mainVm;

            LuuThongTinCommand = new RelayCommand((p) => LuuThongTinKhachHang(), (p) => true);
            NhapLaiCommand = new RelayCommand((p) => XoaForm(), (p) => true);

            DongBoTuDuLieuHienTai();
        }

        public void DongBoTuDuLieuHienTai()
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

        private void LuuThongTinKhachHang()
        {
            if (string.IsNullOrWhiteSpace(TenKhachHang) || string.IsNullOrWhiteSpace(SoDienThoai) || string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            mainViewModel.KhachHangHienTai = new KhachHang(TenKhachHang.Trim(), SoDienThoai.Trim(), Email.Trim());
            mainViewModel.DongBoDuLieuKhachHang();

            MessageBox.Show("Đã lưu thông tin khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void XoaForm()
        {
            TenKhachHang = string.Empty;
            SoDienThoai = string.Empty;
            Email = string.Empty;
        }
    }
}
