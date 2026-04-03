using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _38_NguyenLeAnhKhoa_Tuan06.Model
{
    internal class ClassModel : INotifyPropertyChanged
    {
        private string tenLop;
        public string TenLop { get => tenLop; set { tenLop = value; OnPropertyChanged(); } }
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        public ClassModel() { }
        public ClassModel(string name) { TenLop = name; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
