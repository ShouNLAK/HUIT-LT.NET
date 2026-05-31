using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan12.Helper;
using _38_NguyenLeAnhKhoa_Tuan12.Model;
using _38_NguyenLeAnhKhoa_Tuan12.View;

namespace _38_NguyenLeAnhKhoa_Tuan12.ViewModel
{
    internal class DangNhapViewModel : BaseViewModel
    {
        private QLHANGHOAEntities db = new QLHANGHOAEntities(); 
        private string maNV;
        public string MaNV
        {
            get { return maNV; }
            set
            {
                maNV = value;
                OnPropertyChanged(nameof(MaNV));
                OnPropertyChanged(nameof(IsInputValid));
            }
        }
        private string matKhau;
        public string MatKhau
        {
            get { return matKhau; }
            set
            {
                matKhau = value;
                OnPropertyChanged(nameof(MatKhau));
                OnPropertyChanged(nameof(IsInputValid));
            }
        }
        public bool IsInputValid => !string.IsNullOrWhiteSpace(MaNV) && !string.IsNullOrWhiteSpace(MatKhau);

        private string thongBao;
        public string ThongBao
        {
            get { return thongBao; }
            set
            {
                thongBao = value;
                OnPropertyChanged(nameof(ThongBao));
                OnPropertyChanged(nameof(IsInputValid));
            }
        }
        public RelayCommand DangNhapCommand { get; set; }

        public DangNhapViewModel()
        {
            DangNhapCommand = new RelayCommand(p => DangNhap(p as Window),
                p => true);
        }

        private void DangNhap(Window loginWindow)
        {
            ThongBao = "";
            if(string.IsNullOrWhiteSpace(MaNV))
            {
                ThongBao = "Tên đăng nhập không được để trống";
                return;
            }
            if(string.IsNullOrWhiteSpace(MatKhau))
            {
                ThongBao = "Mật khẩu không được để trống";
                return;
            }

            var nhanVien = db.NhanViens.FirstOrDefault(x => x.MaNV == MaNV && x.MatKhau == MatKhau);
            if (nhanVien != null)
            {
                W_Form main = new W_Form();
                var vm = new FormViewModel();
                vm.TenNhanVien = "Xin chào " + nhanVien.TenNV;
                vm.VaiTro = nhanVien.VaiTro;
                vm.CurrentNhanVien = nhanVien;
                main.DataContext = vm;
                main.Show();

                if (loginWindow != null)
                    loginWindow.Close();
            }
            else
                ThongBao = "Sai tên đăng nhập hoặc mật khẩu! Vui lòng nhập lại";
        }
        
    }
}
