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
    /// Interaction logic for BTM3.xaml
    /// </summary>
    public partial class BTM3 : Window
    {
        public BTM3()
        {
            InitializeComponent();
            LoadDuLieuMau();
        }
        void LoadDuLieuMau()
        {
            cboBan.ItemsSource = new List<string> { "---Chọn bàn---", "Bàn 1", "Bàn 2", "Bàn 3", "Bàn 4" };
            lstMon.ItemsSource = new List<string> { "Phở", "Bún bò", "Cơm tấm", "Trà sữa", "Nước ngọt" };
            cboBan.SelectedIndex = 0;
        }

        private void btn_XacNhan_Click(object sender, RoutedEventArgs e)
        {
            int a;
            if (string.IsNullOrWhiteSpace(txt_TenKH.Text) ||
                (string.IsNullOrWhiteSpace(txt_SDT.Text) || txt_SDT.Text.Length != 10 && !int.TryParse(txt_SDT.Text, out a)) ||
                cboBan.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (lstMon.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món ăn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            txt_ThongTinKH.Text = "Khách hàng: " + txt_TenKH.Text +
                "\nSĐT: " + txt_SDT.Text +
                "\nBàn: " + cboBan.SelectedItem.ToString();

            foreach (var item in lstMon.SelectedItems)
            {
                if (!lstMonDaChon.Items.Contains(item))
                    lstMonDaChon.Items.Add(item);
            }
        }

        private void btn_XoaMon_Click(object sender, RoutedEventArgs e)
        {
            if (lstMonDaChon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (lstMonDaChon.Items.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Danh sách món đặt của bạn chỉ còn 1 món, bạn chắc chắn muốn xóa?",
                    "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
            }

            lstMonDaChon.Items.Remove(lstMonDaChon.SelectedItem);
        }

        private void btn_DatMon_Click(object sender, RoutedEventArgs e)
        {
            if (lstMonDaChon.Items.Count == 0)
            {
                MessageBox.Show("Danh sách món đặt không được rỗng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Đặt món thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            txt_TenKH.Clear();
            txt_SDT.Clear();
            cboBan.SelectedIndex = 0;
            lstMon.UnselectAll();
            txt_ThongTinKH.Clear();
            lstMonDaChon.Items.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
