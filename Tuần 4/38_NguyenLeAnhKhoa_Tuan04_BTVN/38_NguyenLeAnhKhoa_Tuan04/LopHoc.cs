using System;

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    public class LopHoc
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int SiSo { get; set; }
        public string GianVien { get; set; }

        public LopHoc(string maLop, string tenLop, int siSo, string gianVien)
        {
            MaLop = maLop;
            TenLop = tenLop;
            SiSo = siSo;
            GianVien = gianVien;
        }
    }
}
