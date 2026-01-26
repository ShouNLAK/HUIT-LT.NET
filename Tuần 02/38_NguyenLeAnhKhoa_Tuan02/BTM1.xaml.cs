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
    /// Interaction logic for BTM1.xaml
    /// </summary>
    public partial class BTM1 : Window
    {
        public BTM1()
        {
            InitializeComponent();
        }
        private void txt_hoTen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_hoTen.Text))
                txt_hoTen_Check.Text = "Nhập đầy đủ thông tin";
            else
                txt_hoTen_Check.Text = "";
            return;
        }

        private void txt_namSinh_LostFocus(object sender, RoutedEventArgs e)
        {
            int test;
            if (string.IsNullOrWhiteSpace(txt_namSinh.Text))
            {
                txt_namSinh_Check.Text = "Nhập đầy đủ thông tin";
                return;
            }
            else if (!int.TryParse(txt_namSinh.Text, out test) || (test >= DateTime.Today.Year || test < 0))
            {
                txt_namSinh_Check.Text = "Nhập số nguyên và nhỏ hơn năm hiện tại";
                txt_namSinh.Clear();
                return;
            }
            else 
            {
                txt_namSinh_Check.Text = "";
                return;
            }
        }

        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_hoTen_Check.Text) && string.IsNullOrWhiteSpace(txt_namSinh_Check.Text))
            {
                int namSinh;
                int namHienTai = DateTime.Today.Year;
                int.TryParse(txt_namSinh.Text, out namSinh);
                string KQ = "Họ và tên : " + txt_hoTen.Text + "\nTuổi : " + (namHienTai - namSinh);
                MessageBox.Show(KQ, "Kết quả", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            txt_hoTen.Clear();
            txt_namSinh.Clear();
            txt_hoTen.Focus();
            return;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                 e.Cancel= true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_hoTen.Focus();
        }

    }
}
