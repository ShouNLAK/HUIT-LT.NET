using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_3.ViewModel;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
