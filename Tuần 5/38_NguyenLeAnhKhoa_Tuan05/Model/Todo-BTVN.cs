using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan05.Model
{
    internal class Todo_BTVN
    {
        private string tenCongViec;
        private string mucDoUuTien;
        private bool daHoanThanh;
        private string ghiChu;

        public string TenCongViec
        {
            get { return tenCongViec; }
            set { tenCongViec = value; }
        }
        public string MucDoUuTien
        { 
            get { return mucDoUuTien; }
            set { mucDoUuTien = value; }
        }
        public bool DaHoanThanh
        {
            get { return daHoanThanh; }
            set { daHoanThanh = value; }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }

        public Todo_BTVN()
        {
        }

        public Todo_BTVN(string tenCongViec, string mucDoUuTien, bool daHoanThanh, string ghiChu)
        {
            TenCongViec = tenCongViec;
            MucDoUuTien = mucDoUuTien;
            DaHoanThanh = daHoanThanh;
            GhiChu = ghiChu;
        }
    }
}
