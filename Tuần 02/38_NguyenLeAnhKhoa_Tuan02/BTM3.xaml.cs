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
    /// Interaction logic for BTM3.xaml
    /// </summary>
    public partial class BTM3 : Window
    {
        List<Button> ds_gheChon = new List<Button>();
        List<Button> ds_gheMua = new List<Button>();

        public BTM3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Background == Brushes.LightGray)
            {
                btn.Background = Brushes.Blue;
                ds_gheChon.Add(btn);
                return;
            }
            else if (btn.Background == Brushes.Green)
            {
                MessageBox.Show("Ghế này đã được mua","Lỗi",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            else if (btn.Background == Brushes.Blue)
            {
                btn.Background = Brushes.LightGray;
                ds_gheChon.Remove(btn);
                return;
            }
        }
        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void btn_thanhToan_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            foreach (Button btn in ds_gheChon)
            {
                btn.Background = Brushes.Green;
                int ghe = int.Parse(btn.Content.ToString());
                if (ghe <=5)
                    sum += 1000;
                else if (ghe >= 11)
                    sum += 2000;
                else
                    sum += 1500;
                ds_gheMua.Add(btn);
            }
            ds_gheChon.Clear();
            txt_thanhToan.Text = sum.ToString();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in ds_gheChon)
                btn.Background = Brushes.LightGray;
            ds_gheChon.Clear();
            txt_thanhToan.Text = "";
        }
    }
}
