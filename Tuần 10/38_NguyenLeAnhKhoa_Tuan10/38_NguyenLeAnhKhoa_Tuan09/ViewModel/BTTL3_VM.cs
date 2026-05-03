using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;
using _38_NguyenLeAnhKhoa_Tuan09.Model;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTTL3_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        private ObservableCollection<Lop> ds_Lop;
        public ObservableCollection<Lop> DS_Lop
        {
            get { return ds_Lop; }
            set
            {
                ds_Lop = value;
                OnPropertyChanged(nameof(DS_Lop));
            }
        }
        public ObservableCollection<Khoa> DS_Khoa { get; set; }

        private Lop selected_Lop;
        public Lop Selected_Lop
        {
            get { return selected_Lop; }
            set
            {
                selected_Lop = value;
                OnPropertyChanged(nameof(Selected_Lop));
                if (!isAdding && !isEditing)
                {
                    if (selected_Lop != null)
                    {
                        MaLop = selected_Lop.MaLop;
                        MaKhoa = selected_Lop.MaKhoa;
                    }
                    else
                    {
                        MaLop = string.Empty;
                        MaKhoa = string.Empty;
                    }
                    IsUpdateEnabled = selected_Lop != null;
                    IsDeleteEnabled = selected_Lop != null;
                }
            }
        }
        private string maLop;
        public string MaLop
        {
            get { return maLop; }
            set
            {
                maLop = value;
                OnPropertyChanged(nameof(MaLop));
            }
        }
        private string maKhoa;
        public string MaKhoa
        {
            get { return maKhoa; }
            set
            {
                maKhoa = value;
                OnPropertyChanged(nameof(MaKhoa));
            }
        }
        private bool isAdding;
        private bool isEditing;

        private bool isMaLopEnabled = true;
        public bool IsMaLopEnabled
        {
            get { return isMaLopEnabled; }
            set
            {
                isMaLopEnabled = value;
                OnPropertyChanged(nameof(IsMaLopEnabled));
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

        private void LoadDL()
        {
            DS_Lop = new ObservableCollection<Lop>(db.Lop.ToList());
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged(nameof(DS_Lop));
            OnPropertyChanged(nameof(DS_Khoa));
        }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public BTTL3_VM()
        {
            LoadDL();
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
            IsMaLopEnabled = true;
            AddCommand = new RelayCommand(o => Add());
            DeleteCommand = new RelayCommand(o => Delete());
            UpdateCommand = new RelayCommand(o => Update());
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }
        public void Add()
        {
            isAdding = true;
            isEditing = false;
            Selected_Lop = null;
            MaLop = string.Empty;
            MaKhoa = string.Empty;
            IsMaLopEnabled = true;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }

        public void Delete()
        {
            if (Selected_Lop == null)
            {
                MessageBox.Show("Chọn lớp cần xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.SinhVien.Any(sv => sv.MaLop == Selected_Lop.MaLop))
            {
                MessageBox.Show("Không thể xóa lớp đang có sinh viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            var lop = db.Lop.Find(Selected_Lop.MaLop);
            if (lop != null)
            {
                db.Lop.Remove(lop);
                db.SaveChanges();
            }
            LoadDL();
            Selected_Lop = null;
            MaLop = string.Empty;
            MaKhoa = string.Empty;
            IsMaLopEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }

        public void Update()
        {
            if (Selected_Lop == null)
            {
                MessageBox.Show("Chọn lớp cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.SinhVien.Any(sv => sv.MaLop == Selected_Lop.MaLop))
            {
                MessageBox.Show("Không thể sửa lớp đang có sinh viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            isAdding = false;
            isEditing = true;
            IsMaLopEnabled = false;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }
        public void Save()
        {
            if (string.IsNullOrWhiteSpace(MaLop) || string.IsNullOrWhiteSpace(MaKhoa))
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (isAdding)
            {
                if (db.Lop.Any(l => l.MaLop == MaLop))
                {
                    MessageBox.Show("Mã lớp đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                db.Lop.Add(new Lop { MaLop = MaLop, MaKhoa = MaKhoa });
            }
            else if (isEditing)
            {
                if (db.SinhVien.Any(sv => sv.MaLop == MaLop))
                {
                    MessageBox.Show("Không thể sửa lớp đang có sinh viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var lop = db.Lop.Find(MaLop);
                if (lop == null)
                {
                    MessageBox.Show("Không tìm thấy lớp cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                lop.MaKhoa = MaKhoa;
            }
            else
            {
                return;
            }

            db.SaveChanges();
            LoadDL();
            isAdding = false;
            isEditing = false;
            Selected_Lop = null;
            MaLop = string.Empty;
            MaKhoa = string.Empty;
            IsMaLopEnabled = true;
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
            Selected_Lop = null;
            MaLop = string.Empty;
            MaKhoa = string.Empty;
            IsMaLopEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }
    }
}
