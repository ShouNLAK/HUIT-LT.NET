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
    internal class UC_BTTL2_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<Khoa> DS_Khoa { get; set; }
        private Khoa newKhoa;
        public Khoa NewKhoa
        {
            get { return newKhoa; }
            set
            {
                newKhoa = value;
                OnPropertyChanged(nameof(newKhoa));
                if(newKhoa != null )
                {
                    MaKhoa = newKhoa.MaKhoa.ToString();
                    TenKhoa = newKhoa.TenKhoa.ToString();
                    Lop = newKhoa.Lop.ToString();
                }
            }
        }


        private Khoa selected_Khoa;
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
        private string lop;
        public string Lop
        {
            get { return lop; }
            set
            {
                lop = value;
                OnPropertyChanged(nameof(Lop));
            }
        }
        public Khoa Selected_Khoa
        {
            get { return selected_Khoa; }
            set
            {
                selected_Khoa = value;
                OnPropertyChanged();
                NewKhoa = value;
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public UC_BTTL2_VM()
        {
            LoadDL();
            AddCommand = new RelayCommand(o => Add());
            DeleteCommand = new RelayCommand(o => Delete(), o => Selected_Khoa != null );
            UpdateCommand = new RelayCommand(o => Update(), o => Selected_Khoa != null);
        }
        public void LoadDL()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }

        public void Add()
        {
            if (string.IsNullOrEmpty(MaKhoa) || string.IsNullOrEmpty(TenKhoa))
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi : Dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.Khoa.Find(NewKhoa.MaKhoa) != null)
            {
                MessageBox.Show("Trùng dữ liệu", "Lỗi : Dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Khoa k = new Khoa();
            k.MaKhoa = MaKhoa;
            k.TenKhoa = TenKhoa;

            db.Khoa.Add(k);
            db.SaveChanges();
            MessageBox.Show("Đã thêm thành công","Thành công",MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDL();
            selected_Khoa = k;
        }
        public void Delete()
        {
            if (newKhoa == null)
            {
                MessageBox.Show("Nhập chọn dữ liệu cần xóa", "Lỗi : Dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.Khoa.Remove(newKhoa);
            db.SaveChanges();
            MessageBox.Show("Đã xóa thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDL();
            selected_Khoa = null;
        }
        public void Update()
        {
            Khoa k = db.Khoa.Find(newKhoa.MaKhoa);
            k.TenKhoa = TenKhoa;

            db.SaveChanges();
            LoadDL();
            MessageBox.Show("Đã cập nhật thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
