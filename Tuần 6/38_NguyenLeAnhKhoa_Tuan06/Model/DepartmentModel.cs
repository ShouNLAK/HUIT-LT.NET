using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan06.Model
{
    internal class DepartmentModel
    {
        private string ten;
        public string Ten
        {
            get { return ten; }
            set { if (ten == value) return; ten = value; OnPropertyChanged(); }
        }
        public ObservableCollection<EmployeeModel> Employees { get; set; } = new ObservableCollection<EmployeeModel>();
        public DepartmentModel()
        { }
        public DepartmentModel(string name)
        {
            Ten = name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
