using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _38_NguyenLeAnhKhoa_Tuan11.ViewModel;

namespace _38_NguyenLeAnhKhoa_Tuan11.View
{
    /// <summary>
    /// Interaction logic for W_DangNhap.xaml
    /// </summary>
    public partial class W_DangNhap : Window
    {
        public W_DangNhap()
        {
            InitializeComponent();
            DataContext = new DangNhapViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DangNhapViewModel;
            if (vm == null)
                return;

            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
                vm.MatKhau = passwordBox.Password;
        }
    }
}
