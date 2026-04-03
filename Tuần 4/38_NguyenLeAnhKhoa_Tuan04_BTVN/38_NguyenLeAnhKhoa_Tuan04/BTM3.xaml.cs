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
    /// Interaction logic for BTM3.xaml
    /// </summary>
    public partial class BTM3 : Window
    {
        private TreeViewItem rootSV;
        private TreeViewItem rootLH;

        public BTM3()
        {
            InitializeComponent();
            rootSV = new TreeViewItem() { Header = "Danh sách sinh viên", IsExpanded = true };
            rootLH = new TreeViewItem() { Header = "Danh sách lớp học", IsExpanded = true };
            tv_SV.Items.Add(rootSV);
            tv_SV.Items.Add(rootLH);
        }

        private void MenuSV_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucSinhVien();
        }

        private void MenuLH_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucLopHoc();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình","Đóng chương trình?",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        public void ThemMoiSinhVien(SinhVien sv)
        {
            foreach (TreeViewItem item in rootSV.Items)
            {
                if (item.Tag is SinhVien s && s.MSSV == sv.MSSV)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại trong danh sách!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            TreeViewItem tvi = new TreeViewItem();
            tvi.Header = sv.MSSV + " - " + sv.Ten;
            tvi.Tag = sv;
            rootSV.Items.Add(tvi);
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void XoaSinhVien()
        {
            if (tv_SV.SelectedItem is TreeViewItem selectedItem && selectedItem != rootSV && selectedItem != rootLH && selectedItem.Tag is SinhVien)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    rootSV.Items.Remove(selectedItem);
                    if (MainContent.Content is ucSinhVien ucSV)
                    {
                        ucSV.LamMoi();
                    }
                }
            }
        }

        public void ThemMoiLopHoc(LopHoc lh)
        {
            foreach (TreeViewItem item in rootLH.Items)
            {
                if (item.Tag is LopHoc c && c.MaLop == lh.MaLop)
                {
                    MessageBox.Show("Mã lớp đã tồn tại trong danh sách!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            TreeViewItem tvi = new TreeViewItem();
            tvi.Header = lh.MaLop +" - " + lh.TenLop;
            tvi.Tag = lh;
            rootLH.Items.Add(tvi);
            MessageBox.Show("Thêm lớp học thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void XoaLopHoc()
        {
            if (tv_SV.SelectedItem is TreeViewItem selectedItem && selectedItem != rootLH && selectedItem != rootSV && selectedItem.Tag is LopHoc)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    rootLH.Items.Remove(selectedItem);
                    if (MainContent.Content is ucLopHoc ucLH)
                    {
                        ucLH.LamMoi();
                    }
                }
            }
        }

        private void btnToolBarThem_Click(object sender, RoutedEventArgs e)
        {
            if (MainContent.Content is ucSinhVien ucSV)
            {
                ucSV.LuuSinhVien();
            }
            else if (MainContent.Content is ucLopHoc ucLH)
            {
                ucLH.LuuLopHoc();
            }
        }

        private void btnToolBarXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MainContent.Content is ucSinhVien)
            {
                XoaSinhVien();
            }
            else if (MainContent.Content is ucLopHoc)
            {
                XoaLopHoc();
            }
        }

        private void tv_SV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (MainContent.Content is ucSinhVien ucSV)
            {
                if (tv_SV.SelectedItem is TreeViewItem selectedItem && selectedItem != rootSV && selectedItem != rootLH && selectedItem.Tag is SinhVien sv)
                {
                    ucSV.HienThiThongTin(sv);
                }
                else
                {
                    ucSV.ResetTrangThai();
                }
            }
            else if (MainContent.Content is ucLopHoc ucLH)
            {
                if (tv_SV.SelectedItem is TreeViewItem selectedItem && selectedItem != rootSV && selectedItem != rootLH && selectedItem.Tag is LopHoc lh)
                {
                    ucLH.HienThiThongTin(lh);
                }
                else
                {
                    ucLH.ResetTrangThai();
                }
            }
        }
    }
}
