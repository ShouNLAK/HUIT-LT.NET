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
    /// Interaction logic for BTM6.xaml
    /// </summary>
    public partial class BTM6 : Window
    {
        public BTM6()
        {
            InitializeComponent();
        }

        private void txt_A_LostFocus(object sender, RoutedEventArgs e)
        {
            int A;
            if ((txt_A.Text == string.Empty) || (!int.TryParse(txt_A.Text,out A)))
            {
                MessageBox.Show("Vui lòng nhập a", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_A.Focus();
                return;
            }
        }

        private bool kiemTraB()
        {
            int B;
            if ((txt_B.Text == string.Empty) || (!int.TryParse(txt_B.Text, out B)))
            {
                MessageBox.Show("Vui lòng nhập b", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_B.Focus();
                return false;
            }
            return true;
        }

        private void Clear_Data()
        {
            txt_A.Clear();
            txt_B.Clear();
        }

        private void btn_Cong_Click(object sender, RoutedEventArgs e)
        {
            if (!kiemTraB())
                return;
            int a = int.Parse(txt_A.Text);
            int b = int.Parse(txt_B.Text);
            txt_ketQua.Text = (a + b).ToString();
            Clear_Data();
        }

        private void btn_Tru_Click(object sender, RoutedEventArgs e)
        {
            if (!kiemTraB())
                return;
            int a = int.Parse(txt_A.Text);
            int b = int.Parse(txt_B.Text);
            txt_ketQua.Text = (a - b).ToString();
            Clear_Data();
        }

        private void btn_Nhan_Click(object sender, RoutedEventArgs e)
        {
            if (!kiemTraB())
                return;
            int a = int.Parse(txt_A.Text);
            int b = int.Parse(txt_B.Text);
            txt_ketQua.Text = (a * b).ToString();
            Clear_Data();
        }

        private void btn_Chia_Click(object sender, RoutedEventArgs e)
        {
            if (!kiemTraB())
                return;
            int a = int.Parse(txt_A.Text);
            int b = int.Parse(txt_B.Text);
            if (b == 0)
            {
                MessageBox.Show("Không thể chia cho 0", "Lỗi phép tính", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_B.Clear();
                txt_B.Focus();
                return;
            }
            txt_ketQua.Text = (a / b).ToString();
            Clear_Data();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Confirm = MessageBox.Show("Bạn có chắc sẽ đóng chương trình chứ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Confirm == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
