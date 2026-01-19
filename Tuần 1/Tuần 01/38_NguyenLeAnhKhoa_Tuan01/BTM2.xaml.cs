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
using System.Globalization;

namespace _38_NguyenLeAnhKhoa_Tuan01
{
    /// <summary>
    /// Interaction logic for BTM2.xaml
    /// </summary>
    public partial class BTM2 : Window
    {
        public BTM2()
        {
            InitializeComponent();
        }

        private void btn_xinChao_Click(object sender, RoutedEventArgs e)
        {
            if (txt_hoTen.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên", "Lỗi thiếu họ tên", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_hoTen.Focus();
                return;
            }
            string hoTen = txt_hoTen.Text;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string ketQua = "Xin chào " + textInfo.ToTitleCase(hoTen.ToLower());
            lbl_thongBao.Content = ketQua;
            MessageBox.Show(ketQua, "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }
    }
}
