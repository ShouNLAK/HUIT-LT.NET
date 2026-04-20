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
    internal class BTVN6_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<KetQua> DS_KetQua { get; set; }
        public ObservableCollection<SinhVien> DS_SinhVien { get; set; }
        public ObservableCollection<MonHoc> DS_MonHoc { get; set; }
        public List<int> DS_HocKy { get; set; } = new List<int>();
        public List<String> DS_NamHoc { get; set; }
        private void LoadDL()
        {
            DS_KetQua = new ObservableCollection<KetQua>();
            DS_SinhVien = new ObservableCollection<SinhVien>(db.SinhVien.ToList());
            DS_MonHoc = new ObservableCollection<MonHoc>(db.MonHoc.ToList());
            DS_NamHoc = new List<String>(db.KetQua.Select(k => k.NamHoc).Distinct().ToList());
            DS_HocKy = new List<int>(db.KetQua.Select(k => k.HocKy)).Distinct().ToList();
            OnPropertyChanged(nameof(DS_KetQua));
        }
        public BTVN6_VM()
        {
            LoadDL();
            TaiDanhSachSinhVienCommand = new RelayCommand(p => LoadSV());
            SaveCommand = new RelayCommand( p => Save(), p => DS_KetQua.Count > 0);
        }
        public RelayCommand TaiDanhSachSinhVienCommand { get; set; }
        public void LoadSV()
        {
            if (string.IsNullOrWhiteSpace(Selected_MonHoc) ||
                        string.IsNullOrWhiteSpace(Selected_NamHoc) ||
                        !Selected_HocKy.HasValue)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Môn học - Năm học - Học kỳ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DS_KetQua = new ObservableCollection<KetQua>(
                db.KetQua.Where(k => k.MaMonHoc == Selected_MonHoc
                                  && k.NamHoc == Selected_NamHoc
                                  && k.HocKy == Selected_HocKy.Value).ToList());
            if (DS_KetQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào cho môn học và kỳ học đã chọn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            OnPropertyChanged(nameof(DS_KetQua));
        }
        public RelayCommand SaveCommand { get; set; }
        public void Save()
        {
            foreach (var ketQua in DS_KetQua)
            {
                var existingEntry = db.KetQua.FirstOrDefault(k => k.MaSinhVien == ketQua.MaSinhVien
                                                                    && k.MaMonHoc == ketQua.MaMonHoc
                                                                    && k.NamHoc == ketQua.NamHoc
                                                                    && k.HocKy == ketQua.HocKy);
                if (existingEntry != null)
                {
                    existingEntry.Diem = ketQua.Diem;
                }
                else
                {
                    db.KetQua.Add(ketQua);
                }
            }
            db.SaveChanges();
            MessageBox.Show("Lưu điểm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private string selected_MonHoc;
        public string Selected_MonHoc
        {
            get { return selected_MonHoc; }
            set { selected_MonHoc = value; OnPropertyChanged(nameof(Selected_MonHoc)); }
        }

        private string selected_NamHoc;
        public string Selected_NamHoc
        {
            get { return selected_NamHoc; }
            set
            {
                selected_NamHoc = value;
                OnPropertyChanged(nameof(Selected_NamHoc));
            }
        }

        private int? selected_HocKy;
        public int? Selected_HocKy
        {
            get { return selected_HocKy; }
            set
            {
                selected_HocKy = value;
                OnPropertyChanged(nameof(Selected_HocKy));
            }
        }

        private string maSV;
        public string MaSinhVien
        {
            get { return maSV; }
            set
            {
                maSV = value;
                OnPropertyChanged(nameof(MaSinhVien));
            }
        }
        private string diem;
        public string Diem
        {
            get { return diem; }
            set { diem = value; OnPropertyChanged(nameof(Diem)); }
        }
        private string hoTen;
        public string HoTen
        {
            get { return hoTen; }
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(hoTen));
            }
        }
        private string maLop;
        public string MaLop
        {
            get { return maLop; }
            set { maLop = value; OnPropertyChanged(nameof(maLop)); }
        }
    }
}
