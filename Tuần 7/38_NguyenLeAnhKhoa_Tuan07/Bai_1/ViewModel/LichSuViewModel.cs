using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.ViewModel
{
    internal class LichSuViewModel : BaseViewModel
    {
        private readonly ObservableCollection<TaiKhoan> dsTaiKhoan;
        private readonly ObservableCollection<GiaoDich> dsGiaoDich;

        public ObservableCollection<TaiKhoan> DSTK
        {
            get { return dsTaiKhoan; }
        }

        public ObservableCollection<string> DSLoaiGiaoDichLoc { get; set; }
        public ObservableCollection<GiaoDich> DSGDHienThi { get; set; }

        private TaiKhoan taiKhoanLocLichSu;
        public TaiKhoan TaiKhoanLocLichSu
        {
            get { return taiKhoanLocLichSu; }
            set
            {
                taiKhoanLocLichSu = value;
                OnPropertyChanged();
            }
        }

        private string loaiGiaoDichLoc;
        public string LoaiGiaoDichLoc
        {
            get { return loaiGiaoDichLoc; }
            set
            {
                loaiGiaoDichLoc = value;
                OnPropertyChanged();
            }
        }

        private string tuKhoaLichSu;
        public string TuKhoaLichSu
        {
            get { return tuKhoaLichSu; }
            set
            {
                tuKhoaLichSu = value;
                OnPropertyChanged();
            }
        }

        private int tongThu;
        public int TongThu
        {
            get { return tongThu; }
            set
            {
                tongThu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongThuText));
            }
        }

        private int tongChi;
        public int TongChi
        {
            get { return tongChi; }
            set
            {
                tongChi = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongChiText));
            }
        }

        private int soGiaoDich;
        public int SoGiaoDich
        {
            get { return soGiaoDich; }
            set
            {
                soGiaoDich = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SoGiaoDichText));
            }
        }

        public string TongThuText
        {
            get { return TongThu.ToString("N0"); }
        }

        public string TongChiText
        {
            get { return TongChi.ToString("N0"); }
        }

        public string SoGiaoDichText
        {
            get { return SoGiaoDich.ToString(); }
        }

        public RelayCommand LocLichSuCommand { get; set; }
        public RelayCommand HienThiTatCaCommand { get; set; }

        public LichSuViewModel(ObservableCollection<TaiKhoan> danhSachTaiKhoan, ObservableCollection<GiaoDich> danhSachGiaoDich)
        {
            dsTaiKhoan = danhSachTaiKhoan;
            dsGiaoDich = danhSachGiaoDich;

            DSLoaiGiaoDichLoc = new ObservableCollection<string> { "Tất cả", "Gửi tiền", "Rút tiền", "Chuyển khoản" };
            DSGDHienThi = new ObservableCollection<GiaoDich>();

            LocLichSuCommand = new RelayCommand(o => ApplyFilterLichSu(), o => true);
            HienThiTatCaCommand = new RelayCommand(o => HienThiTatCaLichSu(), o => true);

            LoaiGiaoDichLoc = "Tất cả";
            TuKhoaLichSu = "";
            LamMoiDuLieu();
        }

        public void LamMoiDuLieu()
        {
            ApplyFilterLichSu();
        }

        private void HienThiTatCaLichSu()
        {
            TaiKhoanLocLichSu = null;
            LoaiGiaoDichLoc = "Tất cả";
            TuKhoaLichSu = "";
            ApplyFilterLichSu();
        }

        private void ApplyFilterLichSu()
        {
            DSGDHienThi.Clear();

            var query = dsGiaoDich.AsEnumerable();

            if (TaiKhoanLocLichSu != null)
            {
                query = query.Where(x =>
                    (x.TKNguon != null && x.TKNguon.SoTK == TaiKhoanLocLichSu.SoTK) ||
                    (x.TKDich != null && x.TKDich.SoTK == TaiKhoanLocLichSu.SoTK));
            }

            if (!string.IsNullOrWhiteSpace(LoaiGiaoDichLoc) && LoaiGiaoDichLoc != "Tất cả")
            {
                query = query.Where(x => x.LoaiGD == LoaiGiaoDichLoc);
            }

            if (!string.IsNullOrWhiteSpace(TuKhoaLichSu))
            {
                string key = TuKhoaLichSu.Trim().ToLower();
                query = query.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.NoiDung) && x.NoiDung.ToLower().Contains(key)) ||
                    (!string.IsNullOrWhiteSpace(x.MaGD) && x.MaGD.ToLower().Contains(key)) ||
                    (x.TKNguon != null && x.TKNguon.SoTK.ToLower().Contains(key)) ||
                    (x.TKDich != null && x.TKDich.SoTK.ToLower().Contains(key)));
            }

            foreach (var item in query.OrderByDescending(x => x.NgayGD))
            {
                DSGDHienThi.Add(item);
            }

            UpdateThongKeLichSu();
        }

        private void UpdateThongKeLichSu()
        {
            int thu = 0;
            int chi = 0;

            foreach (var item in DSGDHienThi)
            {
                if (TaiKhoanLocLichSu == null)
                {
                    if (item.LoaiGD == "Gửi tiền") thu += item.SoTien;
                    if (item.LoaiGD == "Rút tiền") chi += item.SoTien;
                }
                else
                {
                    string soTKLoc = TaiKhoanLocLichSu.SoTK;

                    if (item.LoaiGD == "Gửi tiền")
                    {
                        if (item.TKNguon != null && item.TKNguon.SoTK == soTKLoc) thu += item.SoTien;
                    }
                    else if (item.LoaiGD == "Rút tiền")
                    {
                        if (item.TKNguon != null && item.TKNguon.SoTK == soTKLoc) chi += item.SoTien;
                    }
                    else if (item.LoaiGD == "Chuyển khoản")
                    {
                        if (item.TKNguon != null && item.TKNguon.SoTK == soTKLoc) chi += item.SoTien;
                        if (item.TKDich != null && item.TKDich.SoTK == soTKLoc) thu += item.SoTien;
                    }
                }
            }

            TongThu = thu;
            TongChi = chi;
            SoGiaoDich = DSGDHienThi.Count;
        }
    }
}
