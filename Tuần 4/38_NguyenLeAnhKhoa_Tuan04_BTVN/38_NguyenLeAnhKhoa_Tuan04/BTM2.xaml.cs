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
    /// Interaction logic for BTM2.xaml
    /// </summary>
    public partial class BTM2 : Window
    {
        public BTM2()
        {
            InitializeComponent();
            AddPhongBan("Giám đốc", "BGĐ");
            AddPhongBan("Kế hoạch", "PKH");
            AddPhongBan("Kế toán", "PKT");
        }

        private void AddPhongBan(string tenPB, string maPB)
        {
            TreeViewItem pb = new TreeViewItem
            {
                Header = tenPB + " - " + maPB
            };
            tvPhongBan.Items.Add(pb);
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(txt_tenPB.Text) || String.IsNullOrEmpty(txt_maPB.Text))
                return false;
            return true;
        }

        private bool IsPBTonTai(string tenPB, string maPB)
        {
            foreach (TreeViewItem item in tvPhongBan.Items)
            {
                string header = item.Header.ToString();
                string[] parts = header.Split('-');
                string ten = parts[0].Trim();
                string ma = parts[1].Trim();
                if (ten.Equals(tenPB, StringComparison.OrdinalIgnoreCase) || ma.Equals(maPB, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        private void btn_ThemPB_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid())
                return;
            string ten = txt_tenPB.Text;
            string ma = txt_maPB.Text;
            if (!IsPBTonTai(ten, ma))
                return;
            AddPhongBan(ten, ma);
            txt_maPB.Text = "";
            txt_tenPB.Text = "";
        }

        private void tvPhongBan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(tvPhongBan.SelectedItem == null)
            {
                return;
            }
            TreeViewItem item = tvPhongBan.SelectedItem as TreeViewItem;
            string header = item.Header.ToString();
            string[] parts = header.Split('-');
            lblTenPB.Text = parts[0].Trim();
            lblMaPB.Text = parts[1].Trim();
        }

        private void Menu_XoaPB_Click(object sender, RoutedEventArgs e)
        {
            if(tvPhongBan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            TreeViewItem item = tvPhongBan.SelectedItem as TreeViewItem;
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban: " + item.Header.ToString() + " không ?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                tvPhongBan.Items.Remove(item);
                lblTenPB.Text = "";
                lblMaPB.Text = "";
            }
        }
    }
}
