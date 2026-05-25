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
using _38_NguyenLeAnhKhoa_Tuan12.Report;


namespace _38_NguyenLeAnhKhoa_Tuan12.View
{
    /// <summary>
    /// Interaction logic for BTTL1.xaml
    /// </summary>
    public partial class BTTL1 : Window
    {
        public BTTL1()
        {
            InitializeComponent();
        }

        private void btn_Report_Click(object sender, RoutedEventArgs e)
        {
            Report_DSSV rpt = new Report_DSSV();
            rpt_DSSV.ViewerCore.ReportSource = rpt;
        }
    }
}
