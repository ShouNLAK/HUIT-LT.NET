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
    /// Interaction logic for BTM4.xaml
    /// </summary>
    public partial class BTM4 : Window
    {
        public int tongTien;
        public BTM4()
        {
            InitializeComponent();
        }

        private bool isValid()
        {
            if (string.IsNullOrWhiteSpace(txt_tenKH.Text))
                return false;
            int a;
            if (string.IsNullOrWhiteSpace(txt_SDT.Text) || txt_SDT.Text.Length != 10 && !int.TryParse(txt_SDT.Text, out a))
                return false;
            if (Cbo_doUong.SelectedIndex == 0)
                return false;
            return true;
        }

        private void btn_Them_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            if (!isValid())
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            lbl_tenKH.Text = txt_tenKH.Text;
            lbl_SDT.Text = txt_SDT.Text;
            string drink = Cbo_doUong.Text;
            string size = (rbu_SM.IsChecked == true) ? "M" : "L";
            sum += 35000;
            if (size == "L") sum += 5000;
            var toppings = new List<string>();
            if (chk_kemPM.IsChecked == true) { toppings.Add("Kem Cheese"); sum += 10000; }
            if (chk_tranChau.IsChecked == true) { toppings.Add("Trân châu"); sum += 7000; }
            if (chk_pudding.IsChecked == true) { toppings.Add("Pudding"); sum += 7000; }
            if (chk_thach.IsChecked == true) { toppings.Add("Thạch trái cây"); sum += 6000; }
            if (chk_thachDua.IsChecked == true) { toppings.Add("Thạch dừa"); sum += 8000; }
            string toppingsText = toppings.Count > 0 ? " - (" + string.Join(", ", toppings) + ")" : string.Empty;
            int index = lst_donHang.Items.Count + 1;
            string donHang = index + ". " + drink + " - Size " + size + toppingsText + " - Ghi chú: " + (string.IsNullOrWhiteSpace(txt_ghiChu.Text) ? "Không có" : txt_ghiChu.Text.Trim()) + " , " + sum + " đ";
            lst_donHang.Items.Add(donHang);
            tongTien += sum;
            lbl_tongTien.Text = string.Format("{0:N0}", tongTien) + " đ";
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            lst_donHang.Items.Clear();
            tongTien = 0;
            lbl_tongTien.Text = "0 đ";
        }

        private void btn_In_Click(object sender, RoutedEventArgs e)
        {
            if (lst_donHang.Items.Count == 0)
            {
                MessageBox.Show("Bạn chưa thêm món nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            lbl_tenKH.Text = txt_tenKH.Text;
            lbl_SDT.Text = txt_SDT.Text;
            lst_chiTietDH.Items.Clear();
            foreach (var item in lst_donHang.Items)
                lst_chiTietDH.Items.Add(item);
            lbl_tongTien.Text = string.Format("{0:N0}", tongTien) + " đ";
        }

        private void btn_Huy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn hủy toàn bộ đơn hàng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                txt_tenKH.Clear();
                txt_SDT.Clear();
                Cbo_doUong.SelectedIndex = 0;
                rbu_SM.IsChecked = true;
                chk_tranChau.IsChecked = false;
                chk_pudding.IsChecked = false;
                chk_thach.IsChecked = false;
                chk_kemPM.IsChecked = false;
                chk_thachDua.IsChecked = false;
                txt_ghiChu.Clear();
                lst_donHang.Items.Clear();
                lst_chiTietDH.Items.Clear();
                tongTien = 0;
                lbl_tongTien.Text = "0 đ";
                lbl_tenKH.Text = "(chưa có)";
                lbl_SDT.Text = "(chưa có)";
            }
        }

        private void btn_Dat_Click(object sender, RoutedEventArgs e)
        {
            if (lst_donHang.Items.Count == 0)
            {
                MessageBox.Show("Bạn chưa thêm món nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Đặt hàng thành công! Cảm ơn quý khách.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            txt_tenKH.Clear();
            txt_SDT.Clear();
            Cbo_doUong.SelectedIndex = 0;
            rbu_SM.IsChecked = true;
            chk_tranChau.IsChecked = false;
            chk_pudding.IsChecked = false;
            chk_thach.IsChecked = false;
            chk_kemPM.IsChecked = false;
            chk_thachDua.IsChecked = false;
            txt_ghiChu.Clear();
            lst_donHang.Items.Clear();
            lst_chiTietDH.Items.Clear();
            tongTien = 0;
            lbl_tongTien.Text = "0 đ";
            lbl_tenKH.Text = "(chưa có)";
            lbl_SDT.Text = "(chưa có)";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
