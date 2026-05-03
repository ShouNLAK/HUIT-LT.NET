using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;
using _38_NguyenLeAnhKhoa_Tuan09.Model;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTTL1_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<Khoa> DS_Khoa { get; set; }
        private Khoa selected_Khoa;
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        private string tenKhoa;
        public string TenKhoa
        {
            get { return tenKhoa; }
            set
            {
                tenKhoa = value;
                OnPropertyChanged(nameof(TenKhoa));
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
        public Khoa Selected_Khoa
        {
            get { return selected_Khoa; }
            set
            {
                selected_Khoa = value;
                if (selected_Khoa != null)
                {
                    MaKhoa = selected_Khoa.MaKhoa;
                    TenKhoa = selected_Khoa.TenKhoa;
                }
                else
                {
                    MaKhoa = string.Empty;
                    TenKhoa = string.Empty;
                }
                OnPropertyChanged(nameof(Selected_Khoa));
            }
        }
        public BTTL1_VM()
        {
            LoadDL();
            AddCommand = new RelayCommand(o => Add());
            UpdateCommand = new RelayCommand(o => Update());
            DeleteCommand = new RelayCommand(o => Delete());
        }
        public void LoadDL()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }

        public void Add()
        {
            if (string.IsNullOrWhiteSpace(MaKhoa) || string.IsNullOrWhiteSpace(TenKhoa))
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.Khoa.Any(k => k.MaKhoa == MaKhoa))
            {
                MessageBox.Show("Mã khoa đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.Khoa.Add(new Khoa { MaKhoa = MaKhoa, TenKhoa = TenKhoa });
            db.SaveChanges();
            LoadDL();
            Selected_Khoa = null;
            MaKhoa = string.Empty;
            TenKhoa = string.Empty;
        }

        public void Update()
        {
            if (Selected_Khoa == null)
            {
                MessageBox.Show("Chọn khoa cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(TenKhoa))
            {
                MessageBox.Show("Tên khoa không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var khoa = db.Khoa.Find(Selected_Khoa.MaKhoa);
            if (khoa == null)
            {
                MessageBox.Show("Không tìm thấy khoa cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            khoa.TenKhoa = TenKhoa;
            db.SaveChanges();
            LoadDL();
            Selected_Khoa = null;
            MaKhoa = string.Empty;
            TenKhoa = string.Empty;
        }

        public void Delete()
        {
            if (Selected_Khoa == null)
            {
                MessageBox.Show("Chọn khoa cần xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.Lop.Any(l => l.MaKhoa == Selected_Khoa.MaKhoa))
            {
                MessageBox.Show("Không thể xóa khoa đang có lớp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show("Bạn có chắc muốn xóa khoa này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            var khoa = db.Khoa.Find(Selected_Khoa.MaKhoa);
            if (khoa != null)
            {
                db.Khoa.Remove(khoa);
                db.SaveChanges();
            }
            LoadDL();
            Selected_Khoa = null;
            MaKhoa = string.Empty;
            TenKhoa = string.Empty;
        }
    }
}
