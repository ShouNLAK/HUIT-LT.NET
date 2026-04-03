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

namespace _38_NguyenLeAnhKhoa_Tuan03
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
        private bool check_valid()
        {
            if (String.IsNullOrEmpty(txt_Ho.Text) && String.IsNullOrEmpty(txt_Ten.Text))
                return false;
            if (Cbo_Que.SelectedItem == null)
                return false;
            return true;
        }

        private void btn_XemTT_Click(object sender, RoutedEventArgs e)
        {
            if (!check_valid())
                return;
            string Ho = txt_Ho.Text;
            string Ten = txt_Ten.Text;
            string Que = Cbo_Que.Text;
            string GT = rbu_Nam.IsChecked == true ? "Mr." : "Miss/Mrs.";
            string message = "Xin chào " + GT + " " + Ho  + " " + Ten +
                "\nNgoại ngữ : " + ((chk_TAnh.IsChecked == true && chk_TTrung.IsChecked == true) ? ("Tiếng Anh và Tiếng Trung") : (chk_TAnh.IsChecked == true ? "Tiếng Anh" : (chk_TTrung.IsChecked == true ? "Tiếng Trung" : "")))
                + "\nQuê quán : " + Que;
            MessageBox.Show(message, "Xem thông tin", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            txt_Ho.Clear();
            txt_Ten.Clear();
            chk_TAnh.IsChecked = false;
            chk_TTrung.IsChecked = false;
            Cbo_Que.SelectedIndex = 0;
        }
    }
}
