using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public BTTL4_VM()
        {
            LoadDL();
        }

        private SinhVien selected_SinhVien;
        public SinhVien Selected_SinhVien
        {
            get { return selected_SinhVien; }
            set
            {
                selected_SinhVien = value;
                MaSinhVien = selected_SinhVien.MaSinhVien.ToString();
                HoTen = selected_SinhVien.HoTen.ToString();
                GioiTinh = selected_SinhVien.GioiTinh.ToString();
                NgaySinh = selected_SinhVien.NgaySinh.Value;
                MaLop = selected_SinhVien.MaLop.ToString();
                OnPropertyChanged(nameof(selected_SinhVien));
            }
        }

        private string maSinhVien;
        public string MaSinhVien
        {
            get { return maSinhVien; }
            set { 
                maSinhVien = value; 
                OnPropertyChanged(nameof(maSinhVien));
            }
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
        private string gioiTinh;    
        public string GioiTinh
        {
            get { return gioiTinh; }
            set
            {
                gioiTinh = value;
                OnPropertyChanged(nameof(gioiTinh));
            }
        }
        private DateTime ngaySinh;
        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set
            {
                ngaySinh = value;
                OnPropertyChanged(nameof(ngaySinh));
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
