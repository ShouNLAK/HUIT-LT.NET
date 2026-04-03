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
    /// Interaction logic for StudentView_BTTL.xaml
    /// </summary>
    public partial class StudentView_BTTL : Window
    {
        public StudentView_BTTL()
        {
            InitializeComponent();
            this.DataContext = _viewModel_BTTL;
            LoadDuLieu();
        }
        private StudentViewModel_BTTL _viewModel_BTTL = new StudentViewModel_BTTL();

        void LoadDuLieu()
        {
            var cities = _viewModel_BTTL.Students.Select(s => s.Tp).Distinct().ToList();
            cbo_City.ItemsSource = cities;

            if (!string.IsNullOrWhiteSpace(_viewModel_BTTL.NewCity) && cities.Contains(_viewModel_BTTL.NewCity))
                cbo_City.SelectedItem = _viewModel_BTTL.NewCity;
            else
                cbo_City.SelectedIndex = -1;
        }

        private void btn_ThemSV_Click(object sender, RoutedEventArgs e)
        {
            _viewModel_BTTL.ThemSV();
            LoadDuLieu();
        }

        private void btn_XoaSV_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel_BTTL.SelectedStudent == null)
                return;

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            _viewModel_BTTL.XoaSV();
            LoadDuLieu();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            _viewModel_BTTL.TaiLai();
            LoadDuLieu();
        }
    }
}
