using System.Windows;

namespace _38_NguyenLeAnhKhoa_Tuan06.View
{
    public partial class ClassView : Window
    {
        public ClassView()
        {
            InitializeComponent();
        }

        private void tvClasses_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is _38_NguyenLeAnhKhoa_Tuan06.ViewModel.ClassViewModel vm)
            {
                if (e.NewValue is _38_NguyenLeAnhKhoa_Tuan06.Model.ClassModel cls)
                    vm.SelectedClass = cls;
            }
        }
    }
}
