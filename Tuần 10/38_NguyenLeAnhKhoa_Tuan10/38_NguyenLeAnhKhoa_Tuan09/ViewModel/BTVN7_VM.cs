using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan09.Helper;
using _38_NguyenLeAnhKhoa_Tuan09.Model;

namespace _38_NguyenLeAnhKhoa_Tuan09.ViewModel
{
    internal class BTVN7_VM : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();
        public ObservableCollection<BANGDIEM> DS_BangDiem { get; set; }
        public List<string> DS_NamHoc { get; set; }
        public List<int> DS_HocKy { get; set; }

        private void LoadDL()
        {
            DS_BangDiem = new ObservableCollection<BANGDIEM>();
            DS_NamHoc = new List<string>(db.KetQua.Select(k => k.NamHoc).Distinct().ToList());
            DS_HocKy = new List<int>(db.KetQua.Select(k => k.HocKy).Distinct().ToList());
            OnPropertyChanged(nameof(DS_BangDiem));
        }

        public BTVN7_VM()
        {
            LoadDL();
            TimCommand = new RelayCommand(
                p =>
                {
                    if (string.IsNullOrWhiteSpace(MaSinhVien))
                    {
                        MessageBox.Show("Không được để trống mã sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    SinhVien sv = db.SinhVien.FirstOrDefault(s => s.MaSinhVien == MaSinhVien.Trim());
                    if (sv == null)
                    {
                        MessageBox.Show("Không tìm thấy sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        HoTen = string.Empty;
                        MaLop = string.Empty;
                        return;
                    }

                    HoTen = sv.HoTen;
                    MaLop = sv.MaLop;
                });

            XemBangDiemCommand = new RelayCommand(
                p =>
                {
                    if (string.IsNullOrWhiteSpace(MaSinhVien))
                    {
                        MessageBox.Show("Vui lòng nhập mã sinh viên trước khi xem bảng điểm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Selected_NamHoc) || !Selected_HocKy.HasValue)
                    {
                        MessageBox.Show("Vui lòng chọn đầy đủ năm học và học kỳ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    SinhVien sv = db.SinhVien.FirstOrDefault(s => s.MaSinhVien == MaSinhVien.Trim());
                    if (sv == null)
                    {
                        MessageBox.Show("Không tìm thấy sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    HoTen = sv.HoTen;
                    MaLop = sv.MaLop;

                    var duLieu = (from kq in db.KetQua
                                  join mh in db.MonHoc on kq.MaMonHoc equals mh.MaMonHoc
                                  where kq.MaSinhVien == MaSinhVien.Trim()
                                     && kq.NamHoc == Selected_NamHoc
                                     && kq.HocKy == Selected_HocKy.Value
                                  select new { kq, mh }).ToList();

                    DS_BangDiem = new ObservableCollection<BANGDIEM>();
                    int stt = 1;
                    foreach (var item in duLieu)
                    {
                        double diem10 = item.kq.Diem.HasValue ? item.kq.Diem.Value : 0;
                        DS_BangDiem.Add(new BANGDIEM
                        {
                            STT = stt,
                            MaMH = item.mh.MaMonHoc,
                            TenMonHoc = item.mh.TenMonHoc,
                            SoTC = item.mh.SoTC.HasValue ? item.mh.SoTC.Value : 0,
                            Diem = diem10,
                            DiemChu = LayDiemChu(diem10)
                        });
                        stt++;
                    }
                    OnPropertyChanged(nameof(DS_BangDiem));

                    TongTinChi = DS_BangDiem.Sum(x => x.SoTC);
                    double tongDiemHe4 = DS_BangDiem.Sum(x => ChuyenDiemHe4(x.Diem) * x.SoTC);
                    GPA = TongTinChi > 0 ? Math.Round(tongDiemHe4 / TongTinChi, 2) : 0;
                    XepLoai = LayXepLoai(GPA);
                });
        }

        public RelayCommand TimCommand { get; set; }
        public RelayCommand XemBangDiemCommand { get; set; }

        private string maSinhVien;
        public string MaSinhVien
        {
            get { return maSinhVien; }
            set
            {
                maSinhVien = value;
                OnPropertyChanged(nameof(MaSinhVien));
            }
        }

        private string hoTen;
        public string HoTen
        {
            get { return hoTen; }
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }

        private string maLop;
        public string MaLop
        {
            get { return maLop; }
            set
            {
                maLop = value;
                OnPropertyChanged(nameof(MaLop));
            }
        }

        private string selected_NamHoc;
        public string Selected_NamHoc
        {
            get { return selected_NamHoc; }
            set
            {
                selected_NamHoc = value;
                OnPropertyChanged(nameof(Selected_NamHoc));
            }
        }

        private int? selected_HocKy;
        public int? Selected_HocKy
        {
            get { return selected_HocKy; }
            set
            {
                selected_HocKy = value;
                OnPropertyChanged(nameof(Selected_HocKy));
            }
        }

        private int tongTinChi;
        public int TongTinChi
        {
            get { return tongTinChi; }
            set
            {
                tongTinChi = value;
                OnPropertyChanged(nameof(TongTinChi));
            }
        }

        private double gpa;
        public double GPA
        {
            get { return gpa; }
            set
            {
                gpa = value;
                OnPropertyChanged(nameof(GPA));
            }
        }

        private string xepLoai;
        public string XepLoai
        {
            get { return xepLoai; }
            set
            {
                xepLoai = value;
                OnPropertyChanged(nameof(XepLoai));
            }
        }

        private double ChuyenDiemHe4(double diem10)
        {
            if (diem10 >= 8.5) return 4.0;
            if (diem10 >= 8.0) return 3.5;
            if (diem10 >= 7.0) return 3.0;
            if (diem10 >= 6.5) return 2.5;
            if (diem10 >= 5.5) return 2.0;
            if (diem10 >= 5.0) return 1.5;
            if (diem10 >= 4.0) return 1.0;
            return 0.0;
        }

        private string LayDiemChu(double diem10)
        {
            if (diem10 >= 8.5) return "A";
            if (diem10 >= 8.0) return "B+";
            if (diem10 >= 7.0) return "B";
            if (diem10 >= 6.5) return "C+";
            if (diem10 >= 5.5) return "C";
            if (diem10 >= 5.0) return "D+";
            if (diem10 >= 4.0) return "D";
            return "F";
        }

        private string LayXepLoai(double gpa)
        {
            if (gpa >= 3.6) return "Xuất sắc";
            if (gpa >= 3.2) return "Giỏi";
            if (gpa >= 2.5) return "Khá";
            if (gpa >= 2.0) return "Trung bình";
            if (gpa >= 1.0) return "Yếu";
            return "Kém";
        }
    }

    internal class BANGDIEM
    {
        public int STT { get; set; }
        public string MaMH { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTC { get; set; }
        public double Diem { get; set; }
        public string DiemChu { get; set; }
    }
}
