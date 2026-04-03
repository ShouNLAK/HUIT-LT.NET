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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    /// <summary>
    /// Interaction logic for ucPhongBan.xaml
    /// </summary>
    public partial class ucPhongBan : UserControl
    {
        public ucPhongBan()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Lưu phòng ban đang được xây dựng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
