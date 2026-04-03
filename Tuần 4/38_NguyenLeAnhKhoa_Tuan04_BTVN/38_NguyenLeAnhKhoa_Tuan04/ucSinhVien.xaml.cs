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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    /// <summary>
    /// Interaction logic for ucSinhVien.xaml
    /// </summary>
    public partial class ucSinhVien : UserControl
    {
        public TreeView temp = new TreeView();
        public ucSinhVien()
        {
            InitializeComponent();
            LoadDuLieu();
            btn_Nam.IsChecked = true;
        }

        private void LoadDuLieu()
        {
            cbo_Lop.ItemsSource = new List<string> { "---Chọn lớp---", "15DHTH01", "15DHTH02", "15DHTH03", "15DHTH04" };
            cbo_Lop.SelectedIndex = 0;
            txt_Date.SelectedDate = DateTime.Now;
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(txt_TenSV.Text) || String.IsNullOrEmpty(txt_MSSV.Text))
                return false;
            return true;
        }

        public void LuuSinhVien()
        {
            if (!IsValid())
            {
                MessageBox.Show("Mã sinh viên và Họ tên không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string mssv = txt_MSSV.Text;
            string ten = txt_TenSV.Text;
            DateTime dt = txt_Date.SelectedDate.Value;
            string phai = btn_Nam.IsChecked == true ? "Nam" : "Nữ";
            string lop = cbo_Lop.Text;

            List<String> hobby = new List<String>();
            if (chk_Music.IsChecked == true)
                hobby.Add("Âm nhạc");
            if (chk_Travel.IsChecked == true)
                hobby.Add("Du lịch");
            if (chk_Sport.IsChecked == true)
                hobby.Add("Thể thao");

            SinhVien sv = new SinhVien(mssv, ten, dt, phai, hobby, lop);
            BTM3 main = Window.GetWindow(this) as BTM3;
            if (main != null)
            {
                main.ThemMoiSinhVien(sv);
            }
        }

        private void btn_Luu_Click(object sender, RoutedEventArgs e)
        {
            LuuSinhVien();
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            BTM3 main = Window.GetWindow(this) as BTM3;
            if (main != null)
            {
                main.XoaSinhVien();
            }
        }

        public void HienThiThongTin(SinhVien sv)
        {
            txt_MSSV.Text = sv.MSSV;
            txt_TenSV.Text = sv.Ten;
            txt_Date.SelectedDate = sv.NgaySinh;
            if (sv.GioiTinh == "Nam") btn_Nam.IsChecked = true; else btn_Nu.IsChecked = true;
            chk_Music.IsChecked = sv.SoThich != null && sv.SoThich.Contains("Âm nhạc");
            chk_Travel.IsChecked = sv.SoThich != null && sv.SoThich.Contains("Du lịch");
            chk_Sport.IsChecked = sv.SoThich != null && sv.SoThich.Contains("Thể thao");
            cbo_Lop.Text = sv.Lop;

            btn_Luu.IsEnabled = false;
            btn_Xoa.IsEnabled = true;

            txt_MSSV.IsReadOnly = true;
            txt_TenSV.IsReadOnly = true;
            txt_Date.IsEnabled = false;
            btn_Nam.IsEnabled = false;
            btn_Nu.IsEnabled = false;
            chk_Music.IsEnabled = false;
            chk_Travel.IsEnabled = false;
            chk_Sport.IsEnabled = false;
            cbo_Lop.IsEnabled = false;
        }

        public void LamMoi()
        {
            txt_MSSV.Clear();
            txt_TenSV.Clear();
            txt_Date.SelectedDate = DateTime.Now;
            btn_Nam.IsChecked = true;
            btn_Nu.IsChecked = false;
            chk_Music.IsChecked = false;
            chk_Sport.IsChecked = false;
            chk_Travel.IsChecked = false;
            cbo_Lop.SelectedIndex = 0;

            btn_Luu.IsEnabled = true;
            btn_Xoa.IsEnabled = false;

            txt_MSSV.IsReadOnly = false;
            txt_TenSV.IsReadOnly = false;
            txt_Date.IsEnabled = true;
            btn_Nam.IsEnabled = true;
            btn_Nu.IsEnabled = true;
            chk_Music.IsEnabled = true;
            chk_Travel.IsEnabled = true;
            chk_Sport.IsEnabled = true;
            cbo_Lop.IsEnabled = true;

            txt_MSSV.Focus();
        }

        public void ResetTrangThai()
        {
            btn_Luu.IsEnabled = true;
            btn_Xoa.IsEnabled = false;
        }

        private void btn_Nam_Checked(object sender, RoutedEventArgs e)
        {
            if (btn_Nu != null)
                btn_Nu.IsChecked = false;
        }

        private void btn_Nu_Checked(object sender, RoutedEventArgs e)
        {
            if (btn_Nam != null)
                btn_Nam.IsChecked = false;
        }

        private void btn_LamMoi_Click(object sender, RoutedEventArgs e)
        {
            LamMoi();
        }
    }
}
