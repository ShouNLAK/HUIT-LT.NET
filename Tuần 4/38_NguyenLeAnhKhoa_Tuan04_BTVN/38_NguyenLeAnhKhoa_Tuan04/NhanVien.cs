using System;

namespace _38_NguyenLeAnhKhoa_Tuan04
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string PhongBan { get; set; }

        public NhanVien(string maNV, string hoTen, string diaChi, string dienThoai, string phongBan)
        {
            MaNV = maNV;
            HoTen = hoTen;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            PhongBan = phongBan;
        }
    }
}
