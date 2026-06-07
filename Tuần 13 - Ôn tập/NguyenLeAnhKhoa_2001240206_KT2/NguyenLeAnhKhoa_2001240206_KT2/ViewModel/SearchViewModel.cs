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
    public class SearchViewModel : BaseViewModel
    {
        private QL_KaraokeEntities db = new QL_KaraokeEntities();

        public ObservableCollection<LOAIPHONG> DanhSachTang { get; set; }

        private LOAIPHONG selectedFloor;
        public LOAIPHONG SelectedFloor
        {
            get { return selectedFloor; }
            set
            {
                selectedFloor = value;
                OnPropertyChanged(nameof(SelectedFloor));
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

        private ObservableCollection<PHONG> ketQuaTimKiem;
        public ObservableCollection<PHONG> KetQuaTimKiem
        {
            get { return ketQuaTimKiem; }
            set
            {
                ketQuaTimKiem = value;
                OnPropertyChanged(nameof(KetQuaTimKiem));
            }
        }

        private PHONG selectedPhong;
        public PHONG SelectedPhong
        {
            get { return selectedPhong; }
            set
            {
                selectedPhong = value;
                OnPropertyChanged(nameof(SelectedPhong));
                HienThiThongTin();
            }
        }

        private string tenPhong;
        public string TenPhong
        {
            get { return tenPhong; }
            set
            {
                tenPhong = value;
                OnPropertyChanged(nameof(TenPhong));
            }
        }

        private string sucChuaHienThi;
        public string SucChuaHienThi
        {
            get { return sucChuaHienThi; }
            set
            {
                sucChuaHienThi = value;
                OnPropertyChanged(nameof(SucChuaHienThi));
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

        private string kieuPhong;
        public string KieuPhong
        {
            get { return kieuPhong; }
            set
            {
                kieuPhong = value;
                OnPropertyChanged(nameof(KieuPhong));
            }
        }

        private string tinhTrang;
        public string TinhTrang
        {
            get { return tinhTrang; }
            set
            {
                tinhTrang = value;
                OnPropertyChanged(nameof(TinhTrang));
            }
        }

        public RelayCommand TimKiemCommand { get; set; }

        public SearchViewModel()
        {
            LoadDuLieu();
            TimKiemCommand = new RelayCommand(o => TimKiem());
        }

        private void LoadDuLieu()
        {
            DanhSachTang = new ObservableCollection<LOAIPHONG>(db.LOAIPHONGs.ToList());
            OnPropertyChanged(nameof(DanhSachTang));
        }

        private void TimKiem()
        {
            try
            {
                var query = db.PHONGs.AsQueryable();

                if (SelectedFloor != null)
                {
                    string maNhom = SelectedFloor.MaNhom;
                    query = query.Where(x => x.MaNhom == maNhom);
                }

                if (!string.IsNullOrWhiteSpace(SucChua))
                {
                    if (!int.TryParse(SucChua, out int sucChuaVal) || sucChuaVal <= 0)
                    {
                        MessageBox.Show("Sức chứa không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    query = query.Where(x => x.SucChua == sucChuaVal);
                }

                KetQuaTimKiem = new ObservableCollection<PHONG>(query.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HienThiThongTin()
        {
            if (SelectedPhong == null)
            {
                TenPhong        = string.Empty;
                SucChuaHienThi  = string.Empty;
                GiaPhong        = string.Empty;
                KieuPhong       = string.Empty;
                TinhTrang       = string.Empty;
                return;
            }

            TenPhong       = SelectedPhong.TenPhong  ?? string.Empty;
            SucChuaHienThi = SelectedPhong.SucChua  != null ? SelectedPhong.SucChua.Value.ToString()  : string.Empty;
            GiaPhong       = SelectedPhong.GiaPhong != null ? SelectedPhong.GiaPhong.Value.ToString() : string.Empty;

            if (SelectedPhong.KieuPhong == "1")
                KieuPhong = "Phòng quạt";
            else if (SelectedPhong.KieuPhong == "2")
                KieuPhong = "Phòng máy lạnh";
            else
                KieuPhong = SelectedPhong.KieuPhong ?? string.Empty;

            try
            {
                string maPhong = SelectedPhong.MaPhong;
                bool daDat = db.DATPHONGs.Any(dp =>
                    dp.MaPh == maPhong &&
                    dp.NgayDat != null && dp.NgayTra != null &&
                    dp.NgayDat <= DateTime.Now && dp.NgayTra >= DateTime.Now);
                TinhTrang = daDat ? "Khách đang nhận phòng" : "Phòng trống";
            }
            catch
            {
                TinhTrang = string.Empty;
            }
        }
    }
}
