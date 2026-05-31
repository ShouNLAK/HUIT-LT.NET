using _38_NguyenLeAnhKhoa_Tuan12.Report;
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
    /// Interaction logic for ViewInPhieuNhap.xaml
    /// </summary>
    public partial class ViewInPhieuNhap : Window
    {
        private string maPhieuNhap;
        private string maNCC;
        private DateTime ngayNhap;
        public ViewInPhieuNhap(string maphieu, string ncc, DateTime ngay)
        {
            InitializeComponent();
            this.maPhieuNhap = maphieu;
            this.maNCC = ncc;
            this.ngayNhap = ngay;
            LoadReport();
        }

        public void LoadReport()
        {
            Report_NhapHang rpt = new Report_NhapHang();
            rpt.SetParameterValue("LocMaPhieu", maPhieuNhap);
            rpt.SetParameterValue("LocMaNCC", maNCC);
            rpt.SetParameterValue("LocNgayNhap", ngayNhap);
            rpt.SetDatabaseLogon("sa", "123", "DESKTOP-P1D5RMO\\MSSQLSERVER2025", "QLHANGHOA");
            rpt_PhieuNhap.ViewerCore.ReportSource = rpt;
        }
    }
}
