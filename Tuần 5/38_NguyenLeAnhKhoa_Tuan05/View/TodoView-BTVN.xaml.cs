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
using Microsoft.Win32;
using _38_NguyenLeAnhKhoa_Tuan05.ViewModel;

namespace _38_NguyenLeAnhKhoa_Tuan05.View
{
    /// <summary>
    /// Interaction logic for TodoView_BTVN.xaml
    /// </summary>
    public partial class TodoView_BTVN : Window
    {
        private readonly TodoViewModel_BTVN viewModelTodo = new TodoViewModel_BTVN();

        public TodoView_BTVN()
        {
            InitializeComponent();
            DataContext = viewModelTodo;
        }

        private void btn_Them_Click(object sender, RoutedEventArgs e)
        {
            if (!viewModelTodo.ThemCongViec())
            {
                MessageBox.Show("Vui lòng nhập tên công việc.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (viewModelTodo.CongViecDuocChon == null)
                return;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa công việc này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                viewModelTodo.XoaCongViecDuocChon();
        }

        private void btn_LamMoi_Click(object sender, RoutedEventArgs e)
        {
            viewModelTodo.LamMoiForm();
        }

        private void btn_LuuJSON_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "JSON file (*.json)|*.json";
            dlg.FileName = "todo.json";

            if (dlg.ShowDialog() == true)
            {
                viewModelTodo.LuuJson(dlg.FileName);
                MessageBox.Show("Lưu file thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_TaiJSON_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "JSON file (*.json)|*.json";

            if (dlg.ShowDialog() == true)
            {
                viewModelTodo.TaiJson(dlg.FileName);
                MessageBox.Show("Tải file thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
