using System;
using System.Collections.ObjectModel;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<HoaDon> DanhSachHoaDon { get; set; }

        public LapHoaDonViewModel LapHoaDonViewModel { get; set; }
        public DanhSachHoaDonViewModel DanhSachHoaDonViewModel { get; set; }

        private object currentViewModel;
        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand navigation_LapHoaDon { get; set; }
        public RelayCommand navigation_DanhSachHoaDon { get; set; }
        public RelayCommand navigation_ThongKe { get; set; }
        public RelayCommand ThoatUngDungCommand { get; set; }

        public MainViewModel()
        {
            DanhSachHoaDon = new ObservableCollection<HoaDon>();

            var kh1 = new KhachHang("Nguyễn Văn A", "0909123456", 2, true);
            var ct1 = new ObservableCollection<ChiTietHoaDon>
            {
                new ChiTietHoaDon("Cafe sữa", 2, 25000),
                new ChiTietHoaDon("Bánh mỳ trứng", 1, 15000)
            };
            DanhSachHoaDon.Add(new HoaDon("HD20260403001", DateTime.Now.AddHours(-3), kh1, "Bàn 01", ct1));

            var kh2 = new KhachHang("Trần Thị B", "0988123456", 3, false);
            var ct2 = new ObservableCollection<ChiTietHoaDon>
            {
                new ChiTietHoaDon("Mỳ xào bò", 1, 40000),
                new ChiTietHoaDon("Cafe đen", 3, 20000)
            };
            DanhSachHoaDon.Add(new HoaDon("HD20260403002", DateTime.Now.AddHours(-1), kh2, "Bàn 03", ct2));

            DanhSachHoaDonViewModel = new DanhSachHoaDonViewModel(DanhSachHoaDon);
            LapHoaDonViewModel = new LapHoaDonViewModel(DanhSachHoaDon, SauKhiThanhToan);

            navigation_LapHoaDon = new RelayCommand(o =>
            {
                CurrentViewModel = LapHoaDonViewModel;
            }, o => true);

            navigation_DanhSachHoaDon = new RelayCommand(o =>
            {
                DanhSachHoaDonViewModel.LamMoiDanhSach();
                CurrentViewModel = DanhSachHoaDonViewModel;
            }, o => true);

            navigation_ThongKe = new RelayCommand(o =>
            {
                DanhSachHoaDonViewModel.LamMoiDanhSach();
                CurrentViewModel = DanhSachHoaDonViewModel;
            }, o => true);

            ThoatUngDungCommand = new RelayCommand(o =>
            {
                if (System.Windows.MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }, o => true);

            CurrentViewModel = LapHoaDonViewModel;
        }

        private void SauKhiThanhToan()
        {
            DanhSachHoaDonViewModel.LamMoiDanhSach();
        }
    }
}
