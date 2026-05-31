using _38_NguyenLeAnhKhoa_Tuan12.Report;
using CrystalDecisions.CrystalReports.Engine;
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

namespace _38_NguyenLeAnhKhoa_Tuan12.View
{
    /// <summary>
    /// Interaction logic for ViewInBangDiem.xaml
    /// </summary>
    public partial class ViewInBangDiem : Window
    {
        private string maMon;
        private string namHoc;
        private int hocKy;

        public ViewInBangDiem(string maMon, string namHoc, int hocKy)
        {
            InitializeComponent();
            this.maMon = maMon;
            this.namHoc = namHoc;
            this.hocKy = hocKy;
            LoadReport();
        }
        public void LoadReport()
        {
            Report_BangDiem rpt = new Report_BangDiem();
            rpt.SetParameterValue("LocMaMon", maMon);
            rpt.SetParameterValue("LocNamHoc", namHoc);
            rpt.SetParameterValue("LocHocKy", hocKy);
            rpt.SetDatabaseLogon("sa", "123", "DESKTOP-P1D5RMO\\MSSQLSERVER2025", "QLSINHVIEN");
           rpt_BangDiem.ViewerCore.ReportSource = rpt;
        }
    }
}
