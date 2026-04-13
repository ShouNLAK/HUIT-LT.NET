using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;
using _38_NguyenLeAnhKhoa_Tuan09.Model;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTTL1_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<Khoa> DS_Khoa { get; set; }
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
                MaKhoa = selected_Khoa.MaKhoa.ToString();
                TenKhoa = selected_Khoa.TenKhoa.ToString();
                Lop = selected_Khoa.Lop.ToString();
                OnPropertyChanged();
            }
        }
        public BTTL1_VM()
        {
            LoadDL();
        }
        public void LoadDL()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }
    }
}
