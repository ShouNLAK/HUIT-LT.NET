using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

namespace _18_NguyenLeAnhKhoa.View
{
    /// <summary>
    /// Interaction logic for Bai1_Main.xaml
    /// </summary>
    public partial class Bai1_Main : Window
    {
        public Bai1_Main()
        {
            InitializeComponent();
            LoadDuLieu();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Thoát chương trình", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadDuLieu()
        {
            rdo_Nam.IsChecked = true;

            cbo_LoaiPhong.Items.Add("Phòng đơn");
            cbo_LoaiPhong.Items.Add("Phòng đôi");
            cbo_LoaiPhong.Items.Add("Phòng gia đình");
            cbo_LoaiPhong.Items.Add("Phòng VIP");

            lst_DichVu.Items.Add("Giặt ủi");
            lst_DichVu.Items.Add("Dọn phòng");
            lst_DichVu.Items.Add("Ăn sáng");
            lst_DichVu.Items.Add("Thuê xe");
        }

        private void rdo_Nam_Checked(object sender, RoutedEventArgs e)
        {
            rdo_Nu.IsChecked = false;
        }

        private void rdo_Nu_Checked(object sender, RoutedEventArgs e)
        {
            rdo_Nam.IsChecked = false;
        }

        private bool isValid()
        {
            if (String.IsNullOrWhiteSpace(txt_HoTenKH.Text) || String.IsNullOrWhiteSpace(txt_MaKH.Text))
                return false;
            if (cbo_LoaiPhong.SelectedValue == null)
                return false;
            return true;
        }

        private void btn_Luu_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin : Mã - Họ tên - Loại phòng", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
            string GT = rdo_Nam.IsChecked == true ? "Nam" : "Nữ";
            string TienIch = "";
            if (chk_TIch_MayLanh.IsChecked == true) 
                TienIch += chk_TIch_MayLanh.Content;
            if (chk_TIch_Wifi.IsChecked == true)
                TienIch += (TienIch == "" ? chk_TIch_MayLanh.Content : ", " + chk_TIch_MayLanh.Content);
            if (chk_TIch_GiuXe.IsChecked == true)
                TienIch += (TienIch == "" ? chk_TIch_GiuXe.Content : ", " + chk_TIch_GiuXe.Content);
            string DichVu = "";
            foreach( var Dvu in lst_DichVu.SelectedItems)
                DichVu += (DichVu == "" ? Dvu.ToString() : ", " + Dvu.ToString());
            string message =
                "Mã khách hàng : " + txt_MaKH.Text + "\n" +
                "Họ và tên : " + txt_HoTenKH.Text + "\n" +
                "Giới tính : " + GT + "\n" +
                "Loại phòng : " + cbo_LoaiPhong.SelectedValue.ToString() + "\n" + 
                "Tiện ích sử dụng : " + TienIch + "\n" + 
                "Dịch vụ sử dụng : " + DichVu + "\n";
            MessageBox.Show(message, "Kết quả", MessageBoxButton.OK);
        }
    }
}
