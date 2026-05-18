using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan11.Helper;
using _38_NguyenLeAnhKhoa_Tuan11.Model;
using _38_NguyenLeAnhKhoa_Tuan11.View;

namespace _38_NguyenLeAnhKhoa_Tuan11.ViewModel
{
    internal class FormViewModel : BaseViewModel
    {
        private string tenNhanVien;
        public string TenNhanVien
        {
            get { return tenNhanVien; }
            set {
                tenNhanVien = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DangXuatCommand { get; set; }
        public FormViewModel()
        {
            DangXuatCommand = new RelayCommand(p => DangXuat(p as Window));
        }
        private void DangXuat(Window mainWindow)
        {
            W_DangNhap login = new W_DangNhap();
            login.Show();

            if (mainWindow != null)
                mainWindow.Close();
        }
    }
}
