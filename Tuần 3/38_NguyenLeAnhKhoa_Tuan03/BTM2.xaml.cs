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
    /// Interaction logic for BTM2.xaml
    /// </summary>
    public partial class BTM2 : Window
    {
        public BTM2()
        {
            InitializeComponent();
        }

        private bool isValid()
        {
            if (String.IsNullOrWhiteSpace(txt_Name.Text))
                return false;
            if (String.IsNullOrWhiteSpace(txt_Job.Text))
                return false;
            if (date_Birth == null)
                return false;
            if (String.IsNullOrWhiteSpace(cbo_Nationality.Text))
                return false;
            return true;
        }

        private void btn_Data_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            lbl_Ten.Text = txt_Name.Text;
            lbl_Nghe.Text = txt_Job.Text;
            lbl_ngaySinh.Text = date_Birth.Text;
            lbl_quocTich.Text = cbo_Nationality.Text;
            lbl_GT.Text = (rbu_Nam.IsChecked == true) ? "Nam" : "Nữ";
            var soThich = new StringBuilder();
            if (chk_Nhac.IsChecked == true) soThich.Append(chk_Nhac.Content);
            if (chk_Sport.IsChecked == true) soThich.Append(chk_Sport.Content);
            if (chk_Travel.IsChecked == true) soThich.Append(chk_Travel.Content);
            if (chk_Sach.IsChecked == true) soThich.Append(chk_Sach.Content);
            lbl_soThich.Text = soThich.Length > 0 ? soThich.ToString().TrimEnd(',',' ') : "Không có";

            var kyNang = lst_Skill.SelectedItems.Cast<ListBoxItem>().Select(i => i.Content.ToString());
            lbl_kyNang.Text = kyNang.Any() ? string.Join(",",kyNang) : "Chưa chọn";

            lbl_ghiChu.Text =  "Không có";

            tabControl.SelectedIndex = 1;
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
    }
}
