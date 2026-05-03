using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;
using _38_NguyenLeAnhKhoa_Tuan09.Model;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTVN5_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<MonHoc> DS_MonHoc {  get; set; }
        public List<String> DS_TinhChat { get; set; }
        private void LoadDL()
        {
            DS_MonHoc = new ObservableCollection<MonHoc>(db.MonHoc.ToList());
            DS_TinhChat = new List<String>(DS_MonHoc.Select(mh => mh.TinhChat.ToString()).Distinct().ToList());
            OnPropertyChanged(nameof(DS_MonHoc));
            OnPropertyChanged(nameof(DS_TinhChat));
        }
        public BTVN5_VM()
        {
            LoadDL();
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
            IsMaMonHocEnabled = true;
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
        private MonHoc selected_MonHoc;
        public MonHoc Selected_MonHoc
        {
            get { return selected_MonHoc; }
            set
            {
                selected_MonHoc = value;
                OnPropertyChanged(nameof(Selected_MonHoc));
                if (!isAdding && !isEditing)
                {
                    if (selected_MonHoc != null)
                    {
                        MaMonHoc = selected_MonHoc.MaMonHoc;
                        TenMonHoc = selected_MonHoc.TenMonHoc;
                        SoTinChi = selected_MonHoc.SoTC.HasValue ? selected_MonHoc.SoTC.Value.ToString() : string.Empty;
                        TinhChat = selected_MonHoc.TinhChat;
                    }
                    else
                    {
                        MaMonHoc = string.Empty;
                        TenMonHoc = string.Empty;
                        SoTinChi = string.Empty;
                        TinhChat = null;
                    }
                    IsUpdateEnabled = selected_MonHoc != null;
                    IsDeleteEnabled = selected_MonHoc != null;
                }
            }
        }
        private string maMH;
        public string MaMonHoc
        {
            get { return maMH; }
            set { 
                maMH = value;
                OnPropertyChanged(nameof(MaMonHoc));
            }
        }
        private string tenMH;
        public string TenMonHoc
        {
            get { return tenMH; }
            set
            {
                tenMH = value;
                OnPropertyChanged(nameof(TenMonHoc));
            }
        }
        private string soTC;
        public string SoTinChi
        {
            get { return soTC; }
            set
            {
                soTC = value;
                OnPropertyChanged(nameof(SoTinChi));
            }
        }
        private string tinhChat;
        public string TinhChat
        {
            get { return tinhChat; }
            set
            {
                tinhChat = value;
                OnPropertyChanged(nameof(TinhChat));
            }
        }

        private bool isAdding;
        private bool isEditing;

        private bool isMaMonHocEnabled = true;
        public bool IsMaMonHocEnabled
        {
            get { return isMaMonHocEnabled; }
            set
            {
                isMaMonHocEnabled = value;
                OnPropertyChanged(nameof(IsMaMonHocEnabled));
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
            Selected_MonHoc = null;
            MaMonHoc = string.Empty;
            TenMonHoc = string.Empty;
            SoTinChi = string.Empty;
            TinhChat = null;
            IsMaMonHocEnabled = true;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }

        public void Update()
        {
            if (Selected_MonHoc == null)
            {
                MessageBox.Show("Chọn môn học cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.KetQua.Any(k => k.MaMonHoc == Selected_MonHoc.MaMonHoc))
            {
                MessageBox.Show("Không thể sửa môn học đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            isAdding = false;
            isEditing = true;
            IsMaMonHocEnabled = false;
            IsAddEnabled = false;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = true;
            IsCancelEnabled = true;
        }

        public void Delete()
        {
            if (Selected_MonHoc == null)
            {
                MessageBox.Show("Chọn môn học cần xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.KetQua.Any(k => k.MaMonHoc == Selected_MonHoc.MaMonHoc))
            {
                MessageBox.Show("Không thể xóa môn học đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show("Bạn có chắc muốn xóa môn học này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            var mh = db.MonHoc.Find(Selected_MonHoc.MaMonHoc);
            if (mh != null)
            {
                db.MonHoc.Remove(mh);
                db.SaveChanges();
            }
            LoadDL();
            Selected_MonHoc = null;
            MaMonHoc = string.Empty;
            TenMonHoc = string.Empty;
            SoTinChi = string.Empty;
            TinhChat = null;
            IsMaMonHocEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(MaMonHoc) || string.IsNullOrWhiteSpace(TenMonHoc) || string.IsNullOrWhiteSpace(SoTinChi) || string.IsNullOrWhiteSpace(TinhChat))
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SoTinChi, out int soTCValue) || soTCValue <= 0)
            {
                MessageBox.Show("Số tín chỉ không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (isAdding)
            {
                if (db.MonHoc.Any(mh => mh.MaMonHoc == MaMonHoc))
                {
                    MessageBox.Show("Mã môn đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                db.MonHoc.Add(new MonHoc
                {
                    MaMonHoc = MaMonHoc,
                    TenMonHoc = TenMonHoc,
                    SoTC = soTCValue,
                    TinhChat = TinhChat
                });
            }
            else if (isEditing)
            {
                if (db.KetQua.Any(k => k.MaMonHoc == MaMonHoc))
                {
                    MessageBox.Show("Không thể sửa môn học đã có kết quả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var mh = db.MonHoc.Find(MaMonHoc);
                if (mh == null)
                {
                    MessageBox.Show("Không tìm thấy môn học cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                mh.TenMonHoc = TenMonHoc;
                mh.SoTC = soTCValue;
                mh.TinhChat = TinhChat;
            }
            else
            {
                return;
            }
            db.SaveChanges();
            LoadDL();
            isAdding = false;
            isEditing = false;
            Selected_MonHoc = null;
            MaMonHoc = string.Empty;
            TenMonHoc = string.Empty;
            SoTinChi = string.Empty;
            TinhChat = null;
            IsMaMonHocEnabled = true;
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
            Selected_MonHoc = null;
            MaMonHoc = string.Empty;
            TenMonHoc = string.Empty;
            SoTinChi = string.Empty;
            TinhChat = null;
            IsMaMonHocEnabled = true;
            IsAddEnabled = true;
            IsUpdateEnabled = false;
            IsDeleteEnabled = false;
            IsSaveEnabled = false;
            IsCancelEnabled = false;
        }
    }
}
