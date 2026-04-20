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
        public ObservableCollection<Lop> DS_Lop_Original { get; set; }
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
                OnPropertyChanged(nameof(selected_Lop));
                NewLop = value;
            }
        }
        private Lop newLop;
        public Lop NewLop
        {
            get { return newLop; }
            set
            {
                newLop = value;
                OnPropertyChanged(nameof(newLop));
                if (newLop != null)
                {
                    MaLop = newLop.MaLop;
                    MaKhoa = newLop.MaKhoa;
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
                OnPropertyChanged(nameof(maLop));
            }
        }
        private string maKhoa;
        public string MaKhoa
        {
            get { return maKhoa; }
            set
            {
                maKhoa = value;
                OnPropertyChanged(nameof(maKhoa));
            }
        }

        private void LoadDL()
        {
            DS_Lop = new ObservableCollection<Lop>(db.Lop.ToList());
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged();
        }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public BTTL3_VM()
        {
            LoadDL();
            DS_Lop_Original = new ObservableCollection<Lop>(DS_Lop.ToList());
            AddCommand = new RelayCommand(o => Add());
            DeleteCommand = new RelayCommand(o => Delete(), o => selected_Lop != null);
            UpdateCommand = new RelayCommand(o => Update(), o => selected_Lop != null);
            SaveCommand = new RelayCommand(o => Save(), 
                o => DS_Lop.Any(l => !DS_Lop_Original.Any(ol => ol.MaLop == l.MaLop && ol.MaKhoa == l.MaKhoa)) || DS_Lop_Original.Any(ol => !DS_Lop.Any(l => l.MaLop == ol.MaLop && l.MaKhoa == ol.MaKhoa)));
            CancelCommand = new RelayCommand(o => LoadDL(),
                o => DS_Lop.Any(l => !DS_Lop_Original.Any(ol => ol.MaLop == l.MaLop && ol.MaKhoa == l.MaKhoa)) || DS_Lop_Original.Any(ol => !DS_Lop.Any(l => l.MaLop == ol.MaLop && l.MaKhoa == ol.MaKhoa)));
        }
        public void Add()
        {
            if (string.IsNullOrWhiteSpace(MaLop) || string.IsNullOrWhiteSpace(MaKhoa))
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DS_Lop.Any(x => x.MaLop == MaLop))
            {
                MessageBox.Show("Mã lớp đã tồn tại trong danh sách", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DS_Lop.Add(new Lop { MaLop = MaLop, MaKhoa = MaKhoa });
        }

        public void Delete()
        {
            if (Selected_Lop == null)
            {
                MessageBox.Show("Chọn lớp cần xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DS_Lop.Remove(Selected_Lop);
            Selected_Lop = null;
        }

        public void Update()
        {
            if (Selected_Lop == null)
            {
                MessageBox.Show("Chọn lớp cần sửa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MaKhoa))
            {
                MessageBox.Show("Mã khoa không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Selected_Lop.MaKhoa = MaKhoa;
            OnPropertyChanged(nameof(DS_Lop));
        }
        public void Save()
        {
            var maLopHienTai = DS_Lop.Select(l => l.MaLop).ToList();
            var lopToRemove = db.Lop.Where(l => !maLopHienTai.Contains(l.MaLop)).ToList();
            foreach (var lop in lopToRemove)
            {
                db.Lop.Remove(lop);
            }

            foreach (Lop lop in DS_Lop)
            {
                var existingLop = db.Lop.Find(lop.MaLop);
                if (existingLop == null)
                {
                    db.Lop.Add(new Lop { MaLop = lop.MaLop, MaKhoa = lop.MaKhoa });
                }
                else
                {
                    existingLop.MaKhoa = lop.MaKhoa;
                }
            }

            db.SaveChanges();
            LoadDL();
            DS_Lop_Original = new ObservableCollection<Lop>(
                DS_Lop.Select(x => new Lop { MaLop = x.MaLop, MaKhoa = x.MaKhoa })
            );

            MessageBox.Show("Đã lưu thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void Cancel()
        {
            DS_Lop = new ObservableCollection<Lop>(
                DS_Lop_Original.Select(x => new Lop { MaLop = x.MaLop, MaKhoa = x.MaKhoa })
            );
            db.SaveChanges();
            Selected_Lop = null;
            MessageBox.Show("Đã hủy thay đổi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
