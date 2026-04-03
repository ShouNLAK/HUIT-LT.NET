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
using _38_NguyenLeAnhKhoa_Tuan05.Model;
using _38_NguyenLeAnhKhoa_Tuan05.ViewModel;

namespace _38_NguyenLeAnhKhoa_Tuan05.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        public StudentView()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private StudentViewModel _viewModel = new StudentViewModel();

        private void btn_ThemSV_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ThemSV();
            txt_hoTen.Focus();
        }

        private void btn_XoaSV_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Xác nhận","Bạn có muốn xóa sinh viên này?",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                _viewModel.XoaSV();
        }

        private void dtg_dssv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_dssv.SelectedItem is Student selected)
                _viewModel.SelectedStudent = selected;
        }

        private void btn_Loc_Click(object sender, RoutedEventArgs e)
        {
            _viewModel?.ApplyFilter();
        }

        private void btn_SapXep_Click(object sender, RoutedEventArgs e)
        {
            _viewModel?.SortByAge();
        }
    }
}
