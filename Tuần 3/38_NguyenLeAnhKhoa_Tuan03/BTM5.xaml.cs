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
    /// Interaction logic for BTM5.xaml
    /// </summary>
    public partial class BTM5 : Window
    {
        public BTM5()
        {
            InitializeComponent();
            LoadDuLieu();
        }

        void LoadDuLieu()
        {
            cbo_PhongBan.ItemsSource = new List<string> { "---Chọn phòng ban---", "Phòng IT", "Phòng Nhân sự", "Phòng Kế toán", "Phòng Marketing" };
            cbo_PhongBan.SelectedIndex = 0;

            cbo_ViTri.ItemsSource = new List<string> { "---Chọn vị trí---", "Nhân viên", "Trưởng nhóm", "Quản lý" };
            cbo_ViTri.SelectedIndex = 0;
        }

        private void btn_ThemGiayTo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_GiayToKhac.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giấy tờ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            lst_GiayTo.Items.Add(txt_GiayToKhac.Text.Trim());
            txt_GiayToKhac.Clear();
        }

        private void btn_NopHS_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaHS.Text) || string.IsNullOrWhiteSpace(txt_HoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbo_ViTri.SelectedIndex == 0 || cbo_PhongBan.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string gt = rbu_Nam.IsChecked == true ? "Nam" : "Nữ";
            string hopDong = rbu_ThoiVu.IsChecked == true ? "Thời vụ" :
                             rbu_NienHan.IsChecked == true ? "Hợp đồng niên" : "Chính thức";

            string thongBao = "Nộp hồ sơ thành công!" +
                "\nMã HS: " + txt_MaHS.Text +
                "\nHọ tên: " + txt_HoTen.Text +
                "\nGiới tính: " + gt +
                "\nPhòng ban: " + cbo_PhongBan.SelectedItem.ToString() +
                "\nVị trí: " + cbo_ViTri.SelectedItem.ToString() +
                "\nLoại HĐ: " + hopDong;

            MessageBox.Show(thongBao, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_TaiLai_Click(object sender, RoutedEventArgs e)
        {
            txt_MaHS.Clear();
            txt_HoTen.Clear();
            rbu_Nam.IsChecked = true;
            date_NgaySinh.SelectedDate = null;
            cbo_PhongBan.SelectedIndex = 0;
            cbo_ViTri.SelectedIndex = 0;
            rbu_ThoiVu.IsChecked = true;
            txt_GhiChu.Clear();
            chk_SKhoe.IsChecked = false;
            chk_BangCap.IsChecked = false;
            chk_TinHoc.IsChecked = false;
            chk_NgoaiNgu.IsChecked = false;
            txt_GiayToKhac.Clear();
            lst_GiayTo.Items.Clear();
        }
        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Exit = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Exit == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
