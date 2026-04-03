using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan06.Model
{
    internal class BankModel
    {
        private int stt;
        private string sotk;
        private string tenKH;
        private string diachi;
        private string tp;
        private int sotien;

        public int STT
        {
            get { return stt; }
            set { stt = value; }
        }
        public string SoTK
        {
            get { return sotk; }
            set { sotk = value; }
        }
        public string TenKH
        {
            get { return tenKH; }
            set { tenKH = value; }
        }
        public string Diachi
        {
            get { return diachi; }
            set { diachi = value; }
        }
        public string TP
        {
            get { return tp; }
            set { tp = value; }
        }
        public int SoTien
        {
            get { return sotien; }
            set { sotien = value; }
        }

        public BankModel(int sothutu,string sotaikhoan, string tenkhachhang, string diachiKH, string thanhpho, int sodutk)
        {
            STT = sothutu;
            SoTK = sotaikhoan;
            TenKH = tenkhachhang;
            Diachi = diachiKH;
            TP = thanhpho;
            SoTien = sodutk;
        }
        public BankModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
