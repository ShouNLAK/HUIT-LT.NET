using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public BTVN5_VM()
        {
            LoadDL();
        }
        private MonHoc selected_MonHoc;
        public MonHoc Selected_MonHoc
        {
            get { return selected_MonHoc; }
            set
            {
                selected_MonHoc = value;
                MaMonHoc = selected_MonHoc.MaMonHoc.ToString();
                TenMonHoc = selected_MonHoc.TenMonHoc.ToString();
                SoTinChi = selected_MonHoc.SoTC.Value;
                TinhChat = selected_MonHoc.TinhChat.ToString();
                OnPropertyChanged();
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
        private int soTC;
        public int SoTinChi
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
    }
}
