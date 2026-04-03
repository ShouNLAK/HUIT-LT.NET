using System;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model
{
    internal class GiaoDich
    {
        private string maGD;
        private DateTime ngayGD;
        private string loaiGD;
        private TaiKhoan tKNguon;
        private TaiKhoan tKDich;
        private int soTien;
        private string noiDung;

        public string MaGD
        {
            get { return maGD; }
            set { maGD = value; }
        }
        public DateTime NgayGD
        {
            get { return ngayGD; }
            set { ngayGD = value; }
        }
        public string LoaiGD
        {
            get { return loaiGD; }
            set { loaiGD = value; }
        }
        public int SoTien
        {
            get { return soTien; }
            set { soTien = value; }
        }
        public string NoiDung
        {
            get { return noiDung; }
            set { noiDung = value; }
        }
        public TaiKhoan TKNguon
        {
            get { return  tKNguon; }
            set { tKNguon = value; }
        }
        public TaiKhoan TKDich
        {
            get { return tKDich; }
            set { tKDich = value; }
        }
        public string SoTKNguon
        {
            get { return TKNguon == null ? "" : TKNguon.SoTK; }
        }
        public string SoTKDich
        {
            get { return TKDich == null ? "" : TKDich.SoTK; }
        }

        public GiaoDich(string ma, DateTime ngay, string loai, TaiKhoan nguon, TaiKhoan dich, int tien,string nd)
        {
            MaGD = ma;
            NgayGD = ngay;
            LoaiGD = loai;
            NoiDung = nd;
            TKNguon = nguon;
            TKDich = dich;
            SoTien = tien;
        }
    }
}
