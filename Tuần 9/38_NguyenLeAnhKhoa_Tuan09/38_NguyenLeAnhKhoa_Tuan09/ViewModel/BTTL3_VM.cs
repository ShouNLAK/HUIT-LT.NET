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
        public ObservableCollection<Lop> DS_Lop { get; set; }
        public ObservableCollection<Khoa> DS_Khoa { get; set; }

        private Lop selected_Lop;
        public Lop Selected_Lop
        {
            get { return selected_Lop; }
            set { selected_Lop = value;
                OnPropertyChanged(nameof(selected_Lop));
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
        public BTTL3_VM()
        {
            LoadDL();
        }
    }
}
