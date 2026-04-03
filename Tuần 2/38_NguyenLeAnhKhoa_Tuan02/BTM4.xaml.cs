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
            if (chk_PTB1.IsChecked == true)
            {
                if (int.TryParse(txt_A.Text, out a) && int.TryParse(txt_B.Text, out b))
                {
                    btn_giai.IsEnabled = true;
                    return;
                }
            }
            else
                if (int.TryParse(txt_A.Text, out a) && int.TryParse(txt_B.Text, out b) && int.TryParse(txt_C.Text, out c))
                {
                    btn_giai.IsEnabled = true;
                    return;
                }
            btn_giai.IsEnabled = false;
            return;
        }

        private void btn_giai_Click(object sender, RoutedEventArgs e)
        {
            float a, b, c;
            float.TryParse(txt_A.Text, out a);
            float.TryParse(txt_B.Text, out b);
            float.TryParse(txt_C.Text, out c);
            if (chk_PTB1.IsChecked == true)
            {
                if (a == 0)
                    if (b == 0)
                    {
                        txt_KQ.Text = "Vô số nghiệm";
                        return;
                    }
                    else
                    {
                        txt_KQ.Text = "Vô nghiệm";
                        return;
                    }
                txt_KQ.Text = "Nghiệm của phương trình :\nX = " + (-b/a);
                return;
            }
            else
            {
                if (a == 0)
                {
                    txt_KQ.Text = "Không phải phương trình bậc 2";
                    return;
                }
                else
                {
                    float delta = b * b - 4 * a * c;
                    if (delta > 0)
                        txt_KQ.Text = "Hai nghiệm của phương trình :\nX1 = " + (-1 * b + Math.Sqrt(delta)) / (2 * a) + "\nX2 = " + (-1 * b - Math.Sqrt(delta)) / (2 * a);
                    else
                    {
                        if (delta == 0)
                            txt_KQ.Text = "Phương trình có nghiệm duy nhất :\nX = " + (-b) / (2 * a);
                        else
                            txt_KQ.Text = "Phương trình vô nghiệm";
                    }
                    return;
                }
            }
        }

    }
}
