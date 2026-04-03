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
    /// Interaction logic for ucLopHoc.xaml
    /// </summary>
    public partial class ucLopHoc : UserControl
    {
        public ucLopHoc()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(txt_MaLop.Text) || string.IsNullOrEmpty(txt_TenLop.Text))
                return false;
            return true;
        }

        public void LuuLopHoc()
        {
            if (!IsValid())
            {
                MessageBox.Show("Mã lớp và Tên lớp không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string maLop = txt_MaLop.Text;
            string tenLop = txt_TenLop.Text;
            string gv = txt_GV.Text;

            LopHoc lh = new LopHoc(maLop, tenLop, 0, gv);
            BTM3 main = Window.GetWindow(this) as BTM3;
            if (main != null)
            {
                main.ThemMoiLopHoc(lh);
            }
        }

        private void btn_Luu_Click(object sender, RoutedEventArgs e)
        {
            LuuLopHoc();
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            BTM3 main = Window.GetWindow(this) as BTM3;
            if (main != null)
            {
                main.XoaLopHoc();
            }
        }

        public void HienThiThongTin(LopHoc lh)
        {
            txt_MaLop.Text = lh.MaLop;
            txt_TenLop.Text = lh.TenLop;
            txt_SiSo.Text = lh.SiSo.ToString();
            txt_GV.Text = lh.GianVien;

            btn_Luu.IsEnabled = false;
            btn_Xoa.IsEnabled = true;

            txt_MaLop.IsReadOnly = true;
            txt_TenLop.IsReadOnly = true;
            txt_GV.IsReadOnly = true;
        }

        public void LamMoi()
        {
            txt_MaLop.Clear();
            txt_TenLop.Clear();
            txt_SiSo.Text = "0";
            txt_GV.Clear();

            btn_Luu.IsEnabled = true;
            btn_Xoa.IsEnabled = false;

            txt_MaLop.IsReadOnly = false;
            txt_TenLop.IsReadOnly = false;
            txt_GV.IsReadOnly = false;

            txt_MaLop.Focus();
        }

        public void ResetTrangThai()
        {
            btn_Luu.IsEnabled = true;
            btn_Xoa.IsEnabled = false;
        }

        private void btn_LamMoi_Click(object sender, RoutedEventArgs e)
        {
            LamMoi();
        }
    }
}
