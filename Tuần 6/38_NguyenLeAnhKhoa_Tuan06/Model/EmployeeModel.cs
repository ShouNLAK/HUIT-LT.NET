using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan06.Model
{
    internal class EmployeeModel : INotifyPropertyChanged
    {
        private string idNhanVien;
        private string nameNhanVien;
        private string diaChi;

        public string IDNhanVien
        {
            get => idNhanVien;
            set
            {
                if (idNhanVien == value) return;
                idNhanVien = value;
                OnPropertyChanged();
            }
        }
        public string NameNhanVien
        {
            get => nameNhanVien;
            set
            {
                if (nameNhanVien == value) return;
                nameNhanVien = value;
                OnPropertyChanged();
            }
        }
        public string DiaChi
        {
            get => diaChi;
            set
            {
                if (diaChi == value) return;
                diaChi = value;
                OnPropertyChanged();
            }
        }

        public EmployeeModel(string ID, string name, string DC)
        {
            IDNhanVien = ID;
            NameNhanVien = name;
            DiaChi = DC;
        }
        public EmployeeModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
