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

namespace _38_NguyenLeAnhKhoa_Tuan06.View
{
    /// <summary>
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : Window
    {
        public DepartmentView()
        {
            InitializeComponent();
        }
        private void tvDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is _38_NguyenLeAnhKhoa_Tuan06.ViewModel.PBNVViewModel vm)
            {
                if (e.NewValue is _38_NguyenLeAnhKhoa_Tuan06.Model.DepartmentModel dep)
                    vm.SelectDepartment = dep;
            }
        }
    }
}
