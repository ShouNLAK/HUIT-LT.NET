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

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    /// <summary>
    /// Interaction logic for BTM1.xaml
    /// </summary>
    public partial class BTM1 : Window
    {
        public BTM1()
        {
            InitializeComponent();
        }

        private void MenuNV_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucNhanVien();
        }
        private void MenuPB_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucPhongBan();
        }
    }
}
