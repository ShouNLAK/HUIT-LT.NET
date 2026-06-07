using System.Windows;
using NguyenLeAnhKhoa_2001240206_KT2.View;

namespace NguyenLeAnhKhoa_2001240206_KT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCau1_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
        }

        private void BtnCau2_Click(object sender, RoutedEventArgs e)
        {
            frmTimKiem w = new frmTimKiem();
            w.Show();
        }

        private void BtnCau3_Click(object sender, RoutedEventArgs e)
        {
            Window_DatPhong w = new Window_DatPhong();
            w.Show();
        }
    }
}
