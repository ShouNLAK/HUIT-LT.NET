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

namespace _38_NguyenLeAnhKhoa_Tuan02
{
    /// <summary>
    /// Interaction logic for BTM4.xaml
    /// </summary>
    public partial class BTM4 : Window
    {
        public BTM4()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chk_PTB1.IsChecked = true;
            btn_giai.IsEnabled = false;

        }

        private void btn_giai_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (btn_giai.IsEnabled == true)
            {
                btn_giai.Background = Brushes.Blue;
                btn_giai.Foreground = Brushes.White;
            }
            else
            {
                btn_giai.Background = Brushes.LightGray;
                btn_giai.Foreground = Brushes.Gray;
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void chk_PTB1_Checked(object sender, RoutedEventArgs e)
        {
            txt_C.IsEnabled = false;
            chk_PTB2.IsChecked = false;
        }

        private void chk_PTB2_Checked(object sender, RoutedEventArgs e)
        {
            txt_C.IsEnabled = true;
            chk_PTB1.IsChecked = false;
        }

        private void txt_HeSo_LostFocus(object sender, RoutedEventArgs e)
        {
            int a, b, c;
            if ((!int.TryParse(txt_A.Text,a)) || (!int.TryParse(txt_B.Text,b)) || (!int.TryParse(txt_C.Text,c)))

        }

    }
}
