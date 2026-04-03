using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _38_NguyenLeAnhKhoa_Tuan06.Model
{
    internal class StudentModel : INotifyPropertyChanged
    {
        private string maSV;
        private string hoTen;
        private string diaChi;
        private double diem1;
        private double diem2;
        private double diem3;

        public string MaSV { get => maSV; set { maSV = value; OnPropertyChanged(); } }
        public string HoTen { get => hoTen; set { hoTen = value; OnPropertyChanged(); } }
        public string DiaChi { get => diaChi; set { diaChi = value; OnPropertyChanged(); } }
        public double Diem1 { get => diem1; set { diem1 = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiemTB)); } }
        public double Diem2 { get => diem2; set { diem2 = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiemTB)); } }
        public double Diem3 { get => diem3; set { diem3 = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiemTB)); } }
        public double DiemTB => Math.Round((Diem1 + Diem2 + Diem3) / 3.0, 2);

        public StudentModel() { }
        public StudentModel(string ma, string ten, string dc, double d1, double d2, double d3)
        {
            MaSV = ma;
            HoTen = ten;
            DiaChi = dc;
            Diem1 = d1;
            Diem2 = d2;
            Diem3 = d3;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
