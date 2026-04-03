using System.Collections.ObjectModel;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model
{
    internal class CauHoiKhaoSat : BaseViewModel
    {
        private int maCauHoi;
        public int MaCauHoi
        {
            get { return maCauHoi; }
            set
            {
                maCauHoi = value;
                OnPropertyChanged();
            }
        }

        private string noiDung;
        public string NoiDung
        {
            get { return noiDung; }
            set
            {
                noiDung = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> DanhSachLuaChon { get; set; }

        public CauHoiKhaoSat()
        {
            DanhSachLuaChon = new ObservableCollection<string>();
        }

        public CauHoiKhaoSat(int ma, string noiDungCauHoi, ObservableCollection<string> luaChon)
        {
            MaCauHoi = ma;
            NoiDung = noiDungCauHoi;
            DanhSachLuaChon = luaChon ?? new ObservableCollection<string>();
        }
    }
}
