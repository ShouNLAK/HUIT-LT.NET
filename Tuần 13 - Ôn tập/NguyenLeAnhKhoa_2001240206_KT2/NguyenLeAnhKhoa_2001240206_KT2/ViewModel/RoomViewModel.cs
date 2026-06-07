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
    public class RoomViewModel : BaseViewModel
    {
        private QL_KaraokeEntities db = new QL_KaraokeEntities();

        private ObservableCollection<PHONG> danhSachPhong;
        public ObservableCollection<PHONG> DanhSachPhong
        {
            get { return danhSachPhong; }
            set
            {
                danhSachPhong = value;
                OnPropertyChanged(nameof(DanhSachPhong));
            }
        }

        private ObservableCollection<LOAIPHONG> danhSachTang;
        public ObservableCollection<LOAIPHONG> DanhSachTang
        {
            get { return danhSachTang; }
            set
            {
                danhSachTang = value;
                OnPropertyChanged(nameof(DanhSachTang));
            }
        }

        private PHONG selectedRoom;
        public PHONG SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
                if (selectedRoom != null)
                {
                    MaPhong  = selectedRoom.MaPhong  ?? string.Empty;
                    TenPhong = selectedRoom.TenPhong ?? string.Empty;
                    GiaPhong = selectedRoom.GiaPhong != null ? selectedRoom.GiaPhong.Value.ToString() : string.Empty;
                    SucChua  = selectedRoom.SucChua  != null ? selectedRoom.SucChua.Value.ToString()  : string.Empty;

                    if (selectedRoom.KieuPhong == "2")
                    { IsACRoom = true;  IsFanRoom = false; }
                    else
                    { IsFanRoom = true; IsACRoom  = false; }

                    if (DanhSachTang != null && selectedRoom.MaNhom != null)
                        SelectedFloor = DanhSachTang.FirstOrDefault(x => x.MaNhom == selectedRoom.MaNhom);
                    else
                        SelectedFloor = null;
                }
            }
        }

        private string maPhong;
        public string MaPhong
        {
            get { return maPhong; }
            set { maPhong = value; OnPropertyChanged(nameof(MaPhong)); }
        }

        private string tenPhong;
        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; OnPropertyChanged(nameof(TenPhong)); }
        }

        private string giaPhong;
        public string GiaPhong
        {
            get { return giaPhong; }
            set { giaPhong = value; OnPropertyChanged(nameof(GiaPhong)); }
        }

        private string sucChua;
        public string SucChua
        {
            get { return sucChua; }
            set { sucChua = value; OnPropertyChanged(nameof(SucChua)); }
        }

        private bool isFanRoom = true;
        public bool IsFanRoom
        {
            get { return isFanRoom; }
            set { isFanRoom = value; OnPropertyChanged(nameof(IsFanRoom)); }
        }

        private bool isACRoom;
        public bool IsACRoom
        {
            get { return isACRoom; }
            set { isACRoom = value; OnPropertyChanged(nameof(IsACRoom)); }
        }

        private LOAIPHONG selectedFloor;
        public LOAIPHONG SelectedFloor
        {
            get { return selectedFloor; }
            set { selectedFloor = value; OnPropertyChanged(nameof(SelectedFloor)); }
        }

        public RelayCommand AddCommand    { get; set; }
        public RelayCommand EditCommand   { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand   { get; set; }
        public RelayCommand ClearCommand  { get; set; }

        public RoomViewModel()
        {
            LoadDuLieu();

            AddCommand    = new RelayCommand(o => Them());
            EditCommand   = new RelayCommand(o => Sua());
            DeleteCommand = new RelayCommand(o => Xoa());
            SaveCommand   = new RelayCommand(o => Luu());
            ClearCommand  = new RelayCommand(o => Clear());
        }

        private void LoadDuLieu()
        {
            DanhSachTang  = new ObservableCollection<LOAIPHONG>(db.LOAIPHONGs.ToList());
            DanhSachPhong = new ObservableCollection<PHONG>(db.PHONGs.Include("LOAIPHONG").ToList());
            OnPropertyChanged(nameof(DanhSachTang));
            OnPropertyChanged(nameof(DanhSachPhong));
        }

        // Lấy lại danh sách từ Local cache của context để cập nhật GridView
        private void LamMoiDanhSach()
        {
            DanhSachPhong = new ObservableCollection<PHONG>(db.PHONGs.Local.ToList());
            OnPropertyChanged(nameof(DanhSachPhong));
        }

        // ---------------------------------------------------------------
        // Thêm: thêm vào EF context (tạm thời, chưa commit xuống DB)
        // ---------------------------------------------------------------
        private void Them()
        {
            if (string.IsNullOrWhiteSpace(MaPhong) || string.IsNullOrWhiteSpace(TenPhong))
            {
                MessageBox.Show("Nhập đầy đủ mã phòng và tên phòng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra trùng mã trong Local cache (gồm cả chưa lưu) và DB
            bool trungMa = db.PHONGs.Local.Any(x => x.MaPhong == MaPhong)
                        || db.PHONGs.AsNoTracking().Any(x => x.MaPhong == MaPhong);
            if (trungMa)
            {
                MessageBox.Show("Mã phòng đã tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal giaPhongVal = 0;
            if (!string.IsNullOrWhiteSpace(GiaPhong) && !decimal.TryParse(GiaPhong, out giaPhongVal))
            {
                MessageBox.Show("Giá phòng không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int sucChuaVal = 0;
            if (!string.IsNullOrWhiteSpace(SucChua) && !int.TryParse(SucChua, out sucChuaVal))
            {
                MessageBox.Show("Sức chứa không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PHONG newRoom = new PHONG();
            newRoom.MaPhong  = MaPhong;
            newRoom.TenPhong = TenPhong;
            newRoom.GiaPhong = giaPhongVal;
            newRoom.SucChua  = sucChuaVal;
            newRoom.KieuPhong = IsACRoom ? "2" : "1";
            newRoom.MaNhom = SelectedFloor != null ? SelectedFloor.MaNhom : null;

            db.PHONGs.Add(newRoom);   // Thêm vào EF context (tạm thời)
            LamMoiDanhSach();         // Cập nhật GridView từ Local cache
            Clear();
        }

        // ---------------------------------------------------------------
        // Sửa: cập nhật entity trong EF context (tạm thời, chưa commit)
        // Quan trọng: lưu reference trước khi gọi Clear()
        // ---------------------------------------------------------------
        private void Sua()
        {
            if (SelectedRoom == null)
            {
                MessageBox.Show("Chọn phòng cần sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(TenPhong))
            {
                MessageBox.Show("Tên phòng không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal giaPhongVal = 0;
            if (!string.IsNullOrWhiteSpace(GiaPhong) && !decimal.TryParse(GiaPhong, out giaPhongVal))
            {
                MessageBox.Show("Giá phòng không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int sucChuaVal = 0;
            if (!string.IsNullOrWhiteSpace(SucChua) && !int.TryParse(SucChua, out sucChuaVal))
            {
                MessageBox.Show("Sức chứa không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lưu reference TRƯỚC khi gọi Clear() để tránh null
            PHONG phongCanSua = SelectedRoom;

            // Cập nhật thuộc tính entity đang được EF track
            phongCanSua.TenPhong  = TenPhong;
            phongCanSua.GiaPhong  = giaPhongVal;
            phongCanSua.SucChua   = sucChuaVal;
            phongCanSua.KieuPhong = IsACRoom ? "2" : "1";
            phongCanSua.MaNhom    = SelectedFloor != null ? SelectedFloor.MaNhom : null;

            // Xóa trắng form
            Clear();

            // Cập nhật lại GridView (rebuild collection để DataGrid tự refresh)
            LamMoiDanhSach();
        }

        // ---------------------------------------------------------------
        // Xóa: đánh dấu xóa trong EF context (tạm thời, chưa commit)
        // Quan trọng: lưu reference trước khi gọi Clear()
        // ---------------------------------------------------------------
        private void Xoa()
        {
            if (SelectedRoom == null)
            {
                MessageBox.Show("Chọn phòng cần xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ketQua = MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ketQua != MessageBoxResult.Yes)
                return;

            // Lưu reference TRƯỚC khi gọi Clear() để tránh null
            PHONG phongCanXoa = SelectedRoom;

            // Xóa trắng form trước (tránh binding còn trỏ vào entity bị xóa)
            Clear();

            // Đánh dấu xóa trong EF context (tạm thời)
            db.PHONGs.Remove(phongCanXoa);

            // Cập nhật lại GridView
            LamMoiDanhSach();
        }

        // ---------------------------------------------------------------
        // Lưu: commit toàn bộ thay đổi tạm xuống DB
        // ---------------------------------------------------------------
        private void Luu()
        {
            try
            {
                db.SaveChanges();
                // Tạo context mới để tránh cache cũ
                db = new QL_KaraokeEntities();
                LoadDuLieu();
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ---------------------------------------------------------------
        // Clear: xóa trắng form nhập liệu
        // ---------------------------------------------------------------
        private void Clear()
        {
            // Đặt SelectedRoom = null TRƯỚC các field khác
            // để tránh setter của SelectedRoom ghi đè lại form
            selectedRoom = null;
            OnPropertyChanged(nameof(SelectedRoom));

            MaPhong  = string.Empty;
            TenPhong = string.Empty;
            GiaPhong = string.Empty;
            SucChua  = string.Empty;
            IsFanRoom = true;
            IsACRoom  = false;
            SelectedFloor = null;
        }
    }
}
