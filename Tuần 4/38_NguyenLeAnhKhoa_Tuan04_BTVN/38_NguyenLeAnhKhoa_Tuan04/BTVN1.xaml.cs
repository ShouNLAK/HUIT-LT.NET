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

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    /// <summary>
    /// Interaction logic for BTVN1.xaml
    /// </summary>
    public partial class BTVN1 : Window
    {
        private ucNhanVien ucNV;

        public BTVN1()
        {
            InitializeComponent();
            ucNV = new ucNhanVien();
            MainContent.Content = ucNV;

            ThemPhongBan("Giám đốc");
            ThemPhongBan("Kế hoạch");

            TaoNhanVienMau("Giám đốc", new NhanVien("NV01", "Lê Trọng Tấn", "HCM", "0907635282", "Giám đốc"));
            TaoNhanVienMau("Kế hoạch", new NhanVien("NV02", "Bùi Đình Túy", "HCM", "028957686", "Kế hoạch"));

            ucNV.LamMoi("");
        }

        private void ThemPhongBan(string tenPhongBan)
        {
            TreeViewItem tviPB = new TreeViewItem { Header = tenPhongBan, IsExpanded = true, Tag = "PhongBan" };
            tvPhongBan.Items.Add(tviPB);
        }

        private void TaoNhanVienMau(string tenPhongBan, NhanVien nv)
        {
            foreach (TreeViewItem pb in tvPhongBan.Items)
            {
                if (pb.Header.ToString() == tenPhongBan)
                {
                    TreeViewItem tviNV = new TreeViewItem { Header = nv.MaNV + " - " + nv.HoTen, Tag = nv };
                    pb.Items.Add(tviNV);
                    break;
                }
            }
        }

        private void MenuNhanVien_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = ucNV;
        }

        private void MenuThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnThemPhongBan_Click(object sender, RoutedEventArgs e)
        {
            string tenPB = txtTenPhongBan.Text.Trim();
            if (string.IsNullOrEmpty(tenPB))
            {
                MessageBox.Show("Tên phòng ban không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (TreeViewItem item in tvPhongBan.Items)
            {
                if (item.Header.ToString().Equals(tenPB, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên phòng ban đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            ThemPhongBan(tenPB);
            MessageBox.Show("Thêm phòng ban thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            txtTenPhongBan.Clear();
        }

        private void btnXoaPhongBan_Click(object sender, RoutedEventArgs e)
        {
            if (tvPhongBan.SelectedItem is TreeViewItem selectedPB && selectedPB.Tag?.ToString() == "PhongBan")
            {
                if (selectedPB.Items.Count > 0)
                {
                    MessageBox.Show("Không thể xóa phòng ban khi vẫn còn nhân viên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    tvPhongBan.Items.Remove(selectedPB);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng ban để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public string LayTenPhongBanDangChon()
        {
            if (tvPhongBan.SelectedItem is TreeViewItem selectedItem)
            {
                if (selectedItem.Tag?.ToString() == "PhongBan")
                    return selectedItem.Header.ToString();

                if (selectedItem.Parent is TreeViewItem parentItem)
                    return parentItem.Header.ToString();
            }
            return "";
        }

        private void tvPhongBan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvPhongBan.SelectedItem is TreeViewItem selectedItem)
            {
                if (selectedItem.Tag?.ToString() == "PhongBan")
                {
                    ucNV.LamMoi(selectedItem.Header.ToString());
                }
                else if (selectedItem.Tag is NhanVien nv)
                {
                    if (selectedItem.Parent is TreeViewItem parentPB)
                    {
                        ucNV.HienThiThongTinNhanVien(nv, parentPB.Header.ToString());
                    }
                }
            }
        }

        public void ThemNhanVien(NhanVien nv, string tenPB)
        {
            foreach (TreeViewItem pb in tvPhongBan.Items)
            {
                if (pb.Header.ToString() == tenPB)
                {
                    foreach (TreeViewItem existingNV in pb.Items)
                    {
                        if (existingNV.Tag is NhanVien n && n.MaNV == nv.MaNV)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    TreeViewItem tviNV = new TreeViewItem { Header = nv.MaNV + " - " + nv.HoTen, Tag = nv };
                    pb.Items.Add(tviNV);
                    pb.IsExpanded = true;
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ucNV.LamMoi(tenPB);
                    return;
                }
            }
            MessageBox.Show("Phòng ban không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void SuaNhanVien(NhanVien nv)
        {
            if (tvPhongBan.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is NhanVien)
            {
                selectedItem.Tag = nv;
                selectedItem.Header = nv.MaNV +" - " + nv.HoTen;
                MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void XoaNhanVien()
        {
            if (tvPhongBan.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is NhanVien)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (selectedItem.Parent is TreeViewItem parentPB)
                    {
                        parentPB.Items.Remove(selectedItem);
                        ucNV.LamMoi(parentPB.Header.ToString());
                        parentPB.IsSelected = true;
                    }
                }
            }
        }
    }
}
