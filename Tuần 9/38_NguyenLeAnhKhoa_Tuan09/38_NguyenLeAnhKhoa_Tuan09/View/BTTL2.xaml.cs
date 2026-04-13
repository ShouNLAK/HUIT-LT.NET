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

namespace _38_NguyenLeAnhKhoa_Tuan09.View
{
    /// <summary>
    /// Interaction logic for BTTL2.xaml
    /// </summary>
    public partial class BTTL2 : Window
    {
        public BTTL2()
        {
            InitializeComponent();
        }

        private void MenuKhoa_Click(object sender, RoutedEventArgs e)
        {
            NoiDungChinh.Content = new UC_BTTL1_Khoa();
        }
    }
}
