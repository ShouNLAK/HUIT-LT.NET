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
    /// Interaction logic for BTM4.xaml
    /// </summary>
    public partial class BTM4 : Window
    {
        private string[,] Nguoi = new string[5, 2];
        private int count = 0;

        public BTM4()
        {
            InitializeComponent();
        }

        private void btn_gui_Click(object sender, RoutedEventArgs e)
        {
            if (txt_soTuoi.Text == string.Empty)
            {
                MessageBox.Show("Phải nhập đầy đủ số tuổi", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string hoTen = txt_hoTen.Text.Trim();
            string age = txt_soTuoi.Text.Trim();
            int doTuoi;
            if (!int.TryParse(age,out doTuoi))
            {
                MessageBox.Show("Số tuổi phải là số nguyên", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_soTuoi.Clear();
                txt_soTuoi.Focus();
                return;
            }
            if (count >= Nguoi.GetLength(0))
            {
                MessageBox.Show("Chỉ chứa tối đa 5 người", "Lỗi bộ nhớ", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Nguoi[count, 0] = hoTen;
            Nguoi[count, 1] = age;
            count++;
            txt_ketQua.Text = "\n\tDanh sách người : \n";
            for (int i = 0;i < count;i++)
            {
                txt_ketQua.Text += (i + 1).ToString() + "." + Nguoi[i, 0] + " | " + Nguoi[i, 1] + " tuổi\n";
            }
            txt_hoTen.Clear();
            txt_soTuoi.Clear();
            txt_hoTen.Focus();
        }

        private void txt_hoTen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_hoTen.Text == string.Empty)
            {
                MessageBox.Show("Phải nhập đầy đủ họ tên", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            txt_hoTen.Focus();
        }
    }
}
