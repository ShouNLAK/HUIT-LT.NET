using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using _38_NguyenLeAnhKhoa_Tuan12.Model;
using _38_NguyenLeAnhKhoa_Tuan12.Report;

namespace _38_NguyenLeAnhKhoa_Tuan12.View
{
    /// <summary>
    /// Interaction logic for BTTL2.xaml
    /// </summary>
    public partial class BTTL2 : Window
    {
        public QLSINHVIENEntities DB = new QLSINHVIENEntities();

        public BTTL2()
        {
            InitializeComponent();
            foreach (SinhVien SV in DB.SinhViens)
                if (!cbo_ChonLop.Items.Contains(SV.MaLop))
                cbo_ChonLop.Items.Add(SV.MaLop);
        }

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            if (cbo_ChonLop.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp");
                return;
            }
            Report_DSSV_Lop rpt = new Report_DSSV_Lop();
            rpt.SetParameterValue("LocMaLop", cbo_ChonLop.SelectedValue.ToString());
            rpt_DSSV_Lop.ViewerCore.ReportSource = rpt;
            rpt.SetDatabaseLogon("sa", "123", "A109PC38\\CSSQL08", "QLSinhVien");
        }
    }
}
