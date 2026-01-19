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

namespace _38_NguyenLeAnhKhoa_Tuan01
{
    /// <summary>
    /// Interaction logic for BTM7.xaml
    /// </summary>
    public partial class BTM7 : Window
    {
        public BTM7()
        {
            InitializeComponent();
        }
        private void txt_soLuong_LostFocus(object sender, RoutedEventArgs e)
        {
            int n;
            if (!int.TryParse(txt_soLuong.Text, out n) && n > 0)
            {
                MessageBox.Show("Phải là số nguyên dương", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_soLuong.Clear();
                txt_soLuong.Focus();
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int SoLuong = int.Parse(txt_soLuong.Text);
            Random ran = new Random();
            int sumC = 0,sumL = 0,sum = 0;
            int[] num = new int[SoLuong];
            for (int i = 0; i < SoLuong; i++)
                num[i] = ran.Next(-1000,1000);
            for (int i = 0; i < SoLuong; i++)
            {
                txt_random.Text += num[i] + " ";
                if (num[i] % 2 == 0)
                    sumC += num[i];
                else
                    sumL += num[i];
                sum += num[i];
            }
            txt_tongAll.Text = sum.ToString();
            txt_tongChan.Text = sumC.ToString();
            txt_tongLe.Text = sumL.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
