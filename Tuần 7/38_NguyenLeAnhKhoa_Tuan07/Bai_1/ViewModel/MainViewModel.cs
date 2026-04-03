using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TaiKhoan> DanhSachTaiKhoan { get; set; }
        public ObservableCollection<GiaoDich> DanhSachGiaoDich { get; set; }

        public TaiKhoanViewModel TaiKhoanViewModel { get; set; }
        public GiaoDichViewModel GiaoDichViewModel { get; set; }
        public LichSuViewModel LichSuViewModel { get; set; }

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

        public RelayCommand navigation_TK { get; set; }
        public RelayCommand navigation_GD { get; set; }
        public RelayCommand navigation_LS { get; set; }

        public MainViewModel()
        {
            DanhSachTaiKhoan = new ObservableCollection<TaiKhoan>
            {
                new TaiKhoan("2001240206", "Nguyễn Lê Anh Khoa", 1000000, "Tiết kiệm", "Hoạt động"),
                new TaiKhoan("2001240207", "Trần Văn Bình", 1200000, "Thanh toán", "Hoạt động"),
                new TaiKhoan("2001240002", "Lê Thanh Tú", 250000, "Tiết kiệm", "Khóa")
            };

            DanhSachGiaoDich = new ObservableCollection<GiaoDich>
            {
                new GiaoDich("GD0001", DateTime.Now.AddDays(-2), "Gửi tiền", DanhSachTaiKhoan.FirstOrDefault(o => o.SoTK == "2001240206"), null, 200000, "Nộp tiền mặt"),
                new GiaoDich("GD0002", DateTime.Now.AddDays(-1), "Rút tiền", DanhSachTaiKhoan.FirstOrDefault(o => o.SoTK == "2001240207"), null, 100000, "Chi tiêu cá nhân")
            };

            TaiKhoanViewModel = new TaiKhoanViewModel(DanhSachTaiKhoan, DanhSachGiaoDich, DongBoDuLieu);
            GiaoDichViewModel = new GiaoDichViewModel(DanhSachTaiKhoan, DanhSachGiaoDich, DongBoDuLieu);
            LichSuViewModel = new LichSuViewModel(DanhSachTaiKhoan, DanhSachGiaoDich);

            navigation_TK = new RelayCommand(o =>
            {
                CurrentViewModel = TaiKhoanViewModel;
            });

            navigation_GD = new RelayCommand(o =>
            {
                CurrentViewModel = GiaoDichViewModel;
            });

            navigation_LS = new RelayCommand(o =>
            {
                CurrentViewModel = LichSuViewModel;
            });

            CurrentViewModel = TaiKhoanViewModel;
            DongBoDuLieu();
        }

        private void DongBoDuLieu()
        {
            TaiKhoanViewModel.CapNhatThongKe();
            GiaoDichViewModel.CapNhatTomTat();
            LichSuViewModel.LamMoiDuLieu();
        }
    }
}
