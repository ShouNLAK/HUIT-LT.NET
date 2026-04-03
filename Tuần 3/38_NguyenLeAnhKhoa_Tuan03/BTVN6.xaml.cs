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
    /// Interaction logic for BTVN6.xaml
    /// </summary>
    public partial class BTVN6 : Window
    {
        public BTVN6()
        {
            InitializeComponent();
            LoadDuLieu();
        }

        private void LoadDuLieu()
        {
            cbo_LopKhoa.ItemsSource = new List<string> { "---Chọn Khoa---", "Công nghệ thông tin", "Công nghệ thực phẩm", "Du lịch", "Điện - Điện tử" };
            cbo_LopKhoa.SelectedIndex = 0;
        }

        private void btn_GuiKS_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_HoTen.Text) || cbo_LopKhoa.SelectedIndex==0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên và lớp/khoa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string gt = rbu_Nam.IsChecked == true ? "Nam" : (rbu_Nu.IsChecked == true ? "Nữ" : "Khác");

            string cau1 = rbu_Rat_Tot.IsChecked == true ? "Rất tốt" :
                          rbu_Tot.IsChecked == true ? "Tốt" :
                          rbu_TrungBinh.IsChecked == true ? "Trung bình" : "Kém";

            string cau2 = rbu_RatHL.IsChecked == true ? "Rất hài lòng" :
                          rbu_HaiLong.IsChecked == true ? "Hài lòng" :
                          rbu_BinhThuong.IsChecked == true ? "Bình thường" : "Không hài lòng";

            string cau3 = rbu_Co.IsChecked == true ? "Có" :
                          rbu_Khong.IsChecked == true ? "Không" : "Tùy trường hợp";

            var deXuat = new List<string>();
            if (chk_GiamHocPhi.IsChecked == true) deXuat.Add("Giảm tải học phí");
            if (chk_TangTH.IsChecked == true) deXuat.Add("Tăng thời lượng thực hành");
            if (chk_CaiThienCSVC.IsChecked == true) deXuat.Add("Cải thiện cơ sở vật chất");
            if (chk_Wifi.IsChecked == true) deXuat.Add("Cải thiện tốc độ Wifi");
            if (chk_GiangVien.IsChecked == true) deXuat.Add("Giảng viên nhiệt tình hơn");
            if (chk_Khac.IsChecked == true && !string.IsNullOrWhiteSpace(txt_DeXuatKhac.Text))
                deXuat.Add(txt_DeXuatKhac.Text.Trim());

            var sb = new StringBuilder();
            sb.AppendLine("- Thông tin sinh viên ");
            sb.AppendLine("Họ và tên: " + txt_HoTen.Text);
            sb.AppendLine("Lớp/Khoa: " + cbo_LopKhoa.Text);
            sb.AppendLine("Giới tính: " + gt);
            sb.AppendLine();
            sb.AppendLine("-- Khảo sát của sinh viên");
            sb.AppendLine("1. Chất lượng giảng dạy: " + cau1);
            sb.AppendLine("2. Hài lòng cơ sở vật chất: " + cau2);
            sb.AppendLine("3. Muốn học online: " + cau3);
            sb.AppendLine();
            sb.AppendLine("--- Sinh viên đề xuất & Cải thiện");
            sb.AppendLine(deXuat.Count > 0 ? string.Join(", ", deXuat) : "Không có");
            sb.AppendLine();
            sb.AppendLine("=== Ý KIẾN ĐÓNG GÓP ===");
            sb.AppendLine(string.IsNullOrWhiteSpace(txt_YKien.Text) ? "Không có" : txt_YKien.Text.Trim());

            txt_KetQua.Text = sb.ToString();
            tabControl.SelectedIndex = 1;
        }

        private void btn_LamMoi_Click(object sender, RoutedEventArgs e)
        {
            txt_HoTen.Clear();
            cbo_LopKhoa.SelectedIndex=0;
            rbu_Nam.IsChecked = true;
            rbu_TrungBinh.IsChecked = true;
            rbu_HaiLong.IsChecked = true;
            rbu_Khong.IsChecked = true;
            chk_GiamHocPhi.IsChecked = false;
            chk_TangTH.IsChecked = false;
            chk_CaiThienCSVC.IsChecked = false;
            chk_Wifi.IsChecked = false;
            chk_GiangVien.IsChecked = false;
            chk_Khac.IsChecked = false;
            txt_DeXuatKhac.Clear();
            txt_YKien.Clear();
            txt_KetQua.Clear();
            tabControl.SelectedIndex = 0;
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
