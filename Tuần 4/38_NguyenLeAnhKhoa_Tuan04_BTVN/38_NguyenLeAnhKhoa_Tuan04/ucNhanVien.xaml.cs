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
    /// Interaction logic for ucNhanVien.xaml
    /// </summary>
    public partial class ucNhanVien : UserControl
    {
        public ucNhanVien()
        {
            InitializeComponent();
        }

        public void HienThiThongTinNhanVien(NhanVien nv, string phongBan)
        {
            txtMaSo.Text = nv.MaNV;
            txtHoTen.Text = nv.HoTen;
            txtDiaChi.Text = nv.DiaChi;
            txtDienThoai.Text = nv.DienThoai;
            txtPhongBan.Text = phongBan;

            txtMaSo.IsReadOnly = true;
            btnThem.IsEnabled = false;
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
        }

        public void LamMoi(string phongBan)
        {
            txtMaSo.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtPhongBan.Text = phongBan;

            txtMaSo.IsReadOnly = false;
            btnThem.IsEnabled = true;
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;

            txtMaSo.Focus();
        }

        private bool KiemTra()
        {
            if (string.IsNullOrWhiteSpace(txtMaSo.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtDienThoai.Text) ||
                string.IsNullOrWhiteSpace(txtPhongBan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (!KiemTra()) return;

            NhanVien nv = new NhanVien(txtMaSo.Text, txtHoTen.Text, txtDiaChi.Text, txtDienThoai.Text, txtPhongBan.Text);
            BTVN1 main = Window.GetWindow(this) as BTVN1;
            if (main != null)
            {
                main.ThemNhanVien(nv, txtPhongBan.Text);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (!KiemTra()) return;

            NhanVien nv = new NhanVien(txtMaSo.Text, txtHoTen.Text, txtDiaChi.Text, txtDienThoai.Text, txtPhongBan.Text);
            BTVN1 main = Window.GetWindow(this) as BTVN1;
            if (main != null)
            {
                main.SuaNhanVien(nv);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            BTVN1 main = Window.GetWindow(this) as BTVN1;
            if (main != null)
            {
                main.XoaNhanVien();
            }
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            BTVN1 main = Window.GetWindow(this) as BTVN1;
            if (main != null)
            {
                string phongBan = main.LayTenPhongBanDangChon();
                LamMoi(phongBan);
            }
        }
    }
}
