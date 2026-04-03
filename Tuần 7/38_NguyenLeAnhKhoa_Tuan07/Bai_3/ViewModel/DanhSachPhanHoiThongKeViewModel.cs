using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.ViewModel
{
    internal class DanhSachPhanHoiThongKeViewModel : BaseViewModel
    {
        private readonly MainViewModel mainViewModel;

        public ObservableCollection<PhanHoiKhachHang> DanhSachPhanHoiHienThi { get; set; }

        private int tongSoPhanHoi;
        public int TongSoPhanHoi
        {
            get { return tongSoPhanHoi; }
            set
            {
                tongSoPhanHoi = value;
                OnPropertyChanged();
            }
        }

        private float diemTrungBinhTatCa;
        public float DiemTrungBinhTatCa
        {
            get { return diemTrungBinhTatCa; }
            set
            {
                diemTrungBinhTatCa = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DiemTrungBinhTatCaText));
            }
        }

        private int soPhanHoiChuaXuLy;
        public int SoPhanHoiChuaXuLy
        {
            get { return soPhanHoiChuaXuLy; }
            set
            {
                soPhanHoiChuaXuLy = value;
                OnPropertyChanged();
            }
        }

        public string DiemTrungBinhTatCaText
        {
            get { return DiemTrungBinhTatCa.ToString("0.00"); }
        }

        public ICommand LamMoiCommand { get; set; }

        public DanhSachPhanHoiThongKeViewModel(MainViewModel mainVm)
        {
            mainViewModel = mainVm;
            DanhSachPhanHoiHienThi = mainViewModel.DanhSachPhanHoi;

            LamMoiCommand = new RelayCommand((p) => CapNhatThongKe(), (p) => true);

            if (DanhSachPhanHoiHienThi != null)
            {
                DanhSachPhanHoiHienThi.CollectionChanged += DanhSachPhanHoiHienThi_CollectionChanged;
            }

            CapNhatThongKe();
        }

        private void DanhSachPhanHoiHienThi_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CapNhatThongKe();
        }

        public void CapNhatThongKe()
        {
            TongSoPhanHoi = DanhSachPhanHoiHienThi == null ? 0 : DanhSachPhanHoiHienThi.Count;

            if (DanhSachPhanHoiHienThi == null || DanhSachPhanHoiHienThi.Count == 0)
            {
                DiemTrungBinhTatCa = 0f;
                SoPhanHoiChuaXuLy = 0;
                return;
            }

            DiemTrungBinhTatCa = (float)DanhSachPhanHoiHienThi.Average(x => x.DiemTrungBinh);
            SoPhanHoiChuaXuLy = DanhSachPhanHoiHienThi.Count(x => !x.DaXuLy);
        }
    }
}
