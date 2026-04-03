using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_2.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_2.ViewModel
{
    internal class DanhSachHoaDonViewModel : BaseViewModel
    {
        private readonly ObservableCollection<HoaDon> hoaDonNguon;

        public ObservableCollection<HoaDon> DanhSachHoaDonHienThi { get; set; }

        private DateTime? tuNgay;
        public DateTime? TuNgay
        {
            get { return tuNgay; }
            set
            {
                tuNgay = value;
                OnPropertyChanged();
            }
        }

        private DateTime? denNgay;
        public DateTime? DenNgay
        {
            get { return denNgay; }
            set
            {
                denNgay = value;
                OnPropertyChanged();
            }
        }

        private int tongSoHoaDon;
        public int TongSoHoaDon
        {
            get { return tongSoHoaDon; }
            set
            {
                tongSoHoaDon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongSoHoaDonText));
            }
        }

        private int tongDoanhThu;
        public int TongDoanhThu
        {
            get { return tongDoanhThu; }
            set
            {
                tongDoanhThu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongDoanhThuText));
            }
        }

        public string TongSoHoaDonText
        {
            get { return TongSoHoaDon.ToString(); }
        }

        public string TongDoanhThuText
        {
            get { return TongDoanhThu.ToString("N0"); }
        }

        public RelayCommand LocHoaDonCommand { get; set; }
        public RelayCommand HienThiTatCaCommand { get; set; }

        public DanhSachHoaDonViewModel(ObservableCollection<HoaDon> danhSachHoaDon)
        {
            hoaDonNguon = danhSachHoaDon;
            DanhSachHoaDonHienThi = new ObservableCollection<HoaDon>();

            LocHoaDonCommand = new RelayCommand(o => LocHoaDon(), o => true);
            HienThiTatCaCommand = new RelayCommand(o => HienThiTatCa(), o => true);

            hoaDonNguon.CollectionChanged += HoaDonNguon_CollectionChanged;
            LamMoiDanhSach();
        }

        private void HoaDonNguon_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LamMoiDanhSach();
        }

        public void LamMoiDanhSach()
        {
            LocHoaDon();
        }

        private void HienThiTatCa()
        {
            TuNgay = null;
            DenNgay = null;
            LocHoaDon();
        }

        private void LocHoaDon()
        {
            DanhSachHoaDonHienThi.Clear();

            var query = hoaDonNguon.AsEnumerable();

            if (TuNgay.HasValue)
            {
                DateTime from = TuNgay.Value.Date;
                query = query.Where(x => x.NgayLap.Date >= from);
            }

            if (DenNgay.HasValue)
            {
                DateTime to = DenNgay.Value.Date;
                query = query.Where(x => x.NgayLap.Date <= to);
            }

            foreach (var item in query.OrderByDescending(x => x.NgayLap))
            {
                DanhSachHoaDonHienThi.Add(item);
            }

            TongSoHoaDon = DanhSachHoaDonHienThi.Count;
            TongDoanhThu = DanhSachHoaDonHienThi.Sum(x => x.ThanhToan);
        }
    }
}
