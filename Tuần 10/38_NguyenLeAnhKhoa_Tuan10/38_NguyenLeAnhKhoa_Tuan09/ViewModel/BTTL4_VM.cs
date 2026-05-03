using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan09.Model;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTTL4_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<SinhVien> DS_SinhVien { get; set; }
        public ObservableCollection<Lop> DS_Lop { get; set; }
        public List<string> List_GioiTinh {  get; set; }

        private void LoadDL()
        {
            DS_SinhVien = new ObservableCollection<SinhVien>(db.SinhVien.ToList());
            DS_Lop = new ObservableCollection<Lop>(db.Lop.ToList());
            List_GioiTinh = new List<string>(DS_SinhVien.Select(sv => sv.GioiTinh.ToString()).Distinct().ToList());
            OnPropertyChanged(nameof(DS_SinhVien));
            OnPropertyChanged(nameof(DS_Lop));
            OnPropertyChanged(nameof(List_GioiTinh));
        }
        public BTTL4_VM()
        {
            LoadDL();
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
            IsMaSinhVienEnabled = true;
            AddCommand = new RelayCommand(o => Add());
            UpdateCommand = new RelayCommand(o => Update());
            DeleteCommand = new RelayCommand(o => Delete());
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private SinhVien selected_SinhVien;
        public SinhVien Selected_SinhVien
        {
            get { return selected_SinhVien; }
            set
            {
                selected_SinhVien = value;
                OnPropertyChanged(nameof(Selected_SinhVien));
                if (!isAdding && !isEditing)
                {
                    if (selected_SinhVien != null)
                    {
                        MaSinhVien = selected_SinhVien.MaSinhVien;
                        HoTen = selected_SinhVien.HoTen;
                        GioiTinh = selected_SinhVien.GioiTinh;
                        NgaySinh = selected_SinhVien.NgaySinh;
                        MaLop = selected_SinhVien.MaLop;
                    }
                    else
                    {
                        MaSinhVien = string.Empty;
                        HoTen = string.Empty;
                        GioiTinh = null;
                        NgaySinh = null;
                        MaLop = null;
                    }
                    IsUpdateEnabled = selected_SinhVien != null;
                    IsDeleteEnabled = selected_SinhVien != null;
                }
            }
        }

        private string maSinhVien;
        public string MaSinhVien
        {
            get { return maSinhVien; }
            set { 
                maSinhVien = value; 
                OnPropertyChanged(nameof(MaSinhVien));
            }
        }
        private string hoTen;
        public string HoTen
        {
            get { return hoTen; }
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }
        private string gioiTinh;    
        public string GioiTinh
        {
            get { return gioiTinh; }
            set
            {
                gioiTinh = value;
                OnPropertyChanged(nameof(GioiTinh));
            }
        }
        private DateTime? ngaySinh;
        public DateTime? NgaySinh
        {
            get { return ngaySinh; }
            set
            {
                ngaySinh = value;
                OnPropertyChanged(nameof(NgaySinh));
            }
        }
        private string maLop;
        public string MaLop
        {
            get { return maLop; }
            set { maLop = value; OnPropertyChanged(nameof(MaLop)); }
        }

        private bool isAdding;
        private bool isEditing;

        private bool isMaSinhVienEnabled = true;
        public bool IsMaSinhVienEnabled
        {
            get { return isMaSinhVienEnabled; }
            set
            {
                isMaSinhVienEnabled = value;
                OnPropertyChanged(nameof(IsMaSinhVienEnabled));
            }
        }

        private bool isAddEnabled = true;
        public bool IsAddEnabled
        {
            get { return isAddEnabled; }
            set
            {
                isAddEnabled = value;
                OnPropertyChanged(nameof(IsAddEnabled));
            }
        }

        private bool isUpdateEnabled;
        public bool IsUpdateEnabled
        {
            get { return isUpdateEnabled; }
            set
            {
                isUpdateEnabled = value;
                OnPropertyChanged(nameof(IsUpdateEnabled));
            }
        }

        private bool isDeleteEnabled;
        public bool IsDeleteEnabled
        {
            get { return isDeleteEnabled; }
            set
            {
                isDeleteEnabled = value;
                OnPropertyChanged(nameof(IsDeleteEnabled));
            }
        }

        private bool isSaveEnabled;
        public bool IsSaveEnabled
        {
            get { return isSaveEnabled; }
            set
            {
                isSaveEnabled = value;
                OnPropertyChanged(nameof(IsSaveEnabled));
            }
        }

        private bool isCancelEnabled;
        public bool IsCancelEnabled
        {
            get { return isCancelEnabled; }
            set
            {
                isCancelEnabled = value;
                OnPropertyChanged(nameof(IsCancelEnabled));
            }
        }

        public void Add()
        {
            isAdding = true;
            isEditing = false;
            Selected_SinhVien = null;
            MaSinhVien = string.Empty;
            HoTen = string.Empty;
            GioiTinh = null;
            NgaySinh = null;
            MaLop = null;
            IsMaSinhVienEnabled = true;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }

        public void Update()
        {
            if (Selected_SinhVien == null)
            {
                MessageBox.Show("Chọn sinh viên cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.KetQua.Any(k => k.MaSinhVien == Selected_SinhVien.MaSinhVien))
            {
                MessageBox.Show("Không thể sửa sinh viên đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            isAdding = false;
            isEditing = true;
            IsMaSinhVienEnabled = false;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }

        public void Delete()
        {
            if (Selected_SinhVien == null)
            {
                MessageBox.Show("Chọn sinh viên cần xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.KetQua.Any(k => k.MaSinhVien == Selected_SinhVien.MaSinhVien))
            {
                MessageBox.Show("Không thể xóa sinh viên đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            var sv = db.SinhVien.Find(Selected_SinhVien.MaSinhVien);
            if (sv != null)
            {
                db.SinhVien.Remove(sv);
                db.SaveChanges();
            }
            LoadDL();
            Selected_SinhVien = null;
            MaSinhVien = string.Empty;
            HoTen = string.Empty;
            GioiTinh = null;
            NgaySinh = null;
            MaLop = null;
            IsMaSinhVienEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(MaSinhVien) || string.IsNullOrWhiteSpace(HoTen) || string.IsNullOrWhiteSpace(GioiTinh) || string.IsNullOrWhiteSpace(MaLop) || !NgaySinh.HasValue)
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!DS_Lop.Any(l => l.MaLop == MaLop))
            {
                MessageBox.Show("Mã lớp không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (isAdding)
            {
                if (db.SinhVien.Any(sv => sv.MaSinhVien == MaSinhVien))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                db.SinhVien.Add(new SinhVien
                {
                    MaSinhVien = MaSinhVien,
                    HoTen = HoTen,
                    GioiTinh = GioiTinh,
                    NgaySinh = NgaySinh,
                    MaLop = MaLop
                });
            }
            else if (isEditing)
            {
                if (db.KetQua.Any(k => k.MaSinhVien == MaSinhVien))
                {
                    MessageBox.Show("Không thể sửa sinh viên đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var sv = db.SinhVien.Find(MaSinhVien);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                sv.HoTen = HoTen;
                sv.GioiTinh = GioiTinh;
                sv.NgaySinh = NgaySinh;
                sv.MaLop = MaLop;
            }
            else
            {
                return;
            }
            db.SaveChanges();
            LoadDL();
            isAdding = false;
            isEditing = false;
            Selected_SinhVien = null;
            MaSinhVien = string.Empty;
            HoTen = string.Empty;
            GioiTinh = null;
            NgaySinh = null;
            MaLop = null;
            IsMaSinhVienEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
            MessageBox.Show("Đã lưu thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Cancel()
        {
            isAdding = false;
            isEditing = false;
            LoadDL();
            Selected_SinhVien = null;
            MaSinhVien = string.Empty;
            HoTen = string.Empty;
            GioiTinh = null;
            NgaySinh = null;
            MaLop = null;
            IsMaSinhVienEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }
    }
}
