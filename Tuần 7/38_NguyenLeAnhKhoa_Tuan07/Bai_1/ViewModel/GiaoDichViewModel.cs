using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.ViewModel
{
    internal class GiaoDichViewModel : BaseViewModel
    {
        private readonly ObservableCollection<TaiKhoan> dsTaiKhoan;
        private readonly ObservableCollection<GiaoDich> dsGiaoDich;
        private readonly Action sauKhiDuLieuThayDoi;

        public ObservableCollection<TaiKhoan> DSTK
        {
            get { return dsTaiKhoan; }
        }

        private string loaiGiaoDich;
        public string LoaiGiaoDich
        {
            get { return loaiGiaoDich; }
            set
            {
                loaiGiaoDich = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsTaiKhoanDichEnabled));
                CapNhatTomTat();
                XacNhanGiaoDichCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isGuiTien;
        public bool IsGuiTien
        {
            get { return isGuiTien; }
            set
            {
                if (isGuiTien == value) return;
                isGuiTien = value;
                if (value)
                {
                    isRutTien = false;
                    isChuyenKhoan = false;
                    LoaiGiaoDich = "Gửi tiền";
                    OnPropertyChanged(nameof(IsRutTien));
                    OnPropertyChanged(nameof(IsChuyenKhoan));
                }
                OnPropertyChanged();
            }
        }

        private bool isRutTien;
        public bool IsRutTien
        {
            get { return isRutTien; }
            set
            {
                if (isRutTien == value) return;
                isRutTien = value;
                if (value)
                {
                    isGuiTien = false;
                    isChuyenKhoan = false;
                    LoaiGiaoDich = "Rút tiền";
                    OnPropertyChanged(nameof(IsGuiTien));
                    OnPropertyChanged(nameof(IsChuyenKhoan));
                }
                OnPropertyChanged();
            }
        }

        private bool isChuyenKhoan;
        public bool IsChuyenKhoan
        {
            get { return isChuyenKhoan; }
            set
            {
                if (isChuyenKhoan == value) return;
                isChuyenKhoan = value;
                if (value)
                {
                    isGuiTien = false;
                    isRutTien = false;
                    LoaiGiaoDich = "Chuyển khoản";
                    OnPropertyChanged(nameof(IsGuiTien));
                    OnPropertyChanged(nameof(IsRutTien));
                }
                OnPropertyChanged();
            }
        }

        public bool IsTaiKhoanDichEnabled
        {
            get { return LoaiGiaoDich == "Chuyển khoản"; }
        }

        private TaiKhoan taiKhoanNguonGD;
        public TaiKhoan TaiKhoanNguonGD
        {
            get { return taiKhoanNguonGD; }
            set
            {
                taiKhoanNguonGD = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SoDuHienTai));
                CapNhatTomTat();
                XacNhanGiaoDichCommand.RaiseCanExecuteChanged();
            }
        }

        private TaiKhoan taiKhoanDichGD;
        public TaiKhoan TaiKhoanDichGD
        {
            get { return taiKhoanDichGD; }
            set
            {
                taiKhoanDichGD = value;
                OnPropertyChanged();
                XacNhanGiaoDichCommand.RaiseCanExecuteChanged();
            }
        }

        private string soTienGD;
        public string SoTienGD
        {
            get { return soTienGD; }
            set
            {
                soTienGD = value;
                OnPropertyChanged();
                CapNhatTomTat();
                XacNhanGiaoDichCommand.RaiseCanExecuteChanged();
            }
        }

        private string noiDungGD;
        public string NoiDungGD
        {
            get { return noiDungGD; }
            set
            {
                noiDungGD = value;
                OnPropertyChanged();
            }
        }

        public string SoDuHienTai
        {
            get
            {
                if (TaiKhoanNguonGD == null) return "";
                return TaiKhoanNguonGD.SoDu.ToString("N0");
            }
        }

        public string TomTatLoaiGiaoDich
        {
            get { return string.IsNullOrWhiteSpace(LoaiGiaoDich) ? "" : LoaiGiaoDich; }
        }

        public string TomTatSoTien
        {
            get
            {
                int soTien;
                if (!int.TryParse(SoTienGD, out soTien)) return "0";
                return soTien.ToString("N0");
            }
        }

        public string PhiGiaoDich
        {
            get
            {
                if (LoaiGiaoDich == "Chuyển khoản") return "2,000";
                return "0";
            }
        }

        public string SoDuSauGiaoDich
        {
            get
            {
                if (TaiKhoanNguonGD == null) return "";
                int soTien;
                if (!int.TryParse(SoTienGD, out soTien) || soTien < 0)
                {
                    return TaiKhoanNguonGD.SoDu.ToString("N0");
                }

                if (LoaiGiaoDich == "Gửi tiền") return (TaiKhoanNguonGD.SoDu + soTien).ToString("N0");
                if (LoaiGiaoDich == "Rút tiền") return (TaiKhoanNguonGD.SoDu - soTien).ToString("N0");
                if (LoaiGiaoDich == "Chuyển khoản") return (TaiKhoanNguonGD.SoDu - soTien).ToString("N0");
                return TaiKhoanNguonGD.SoDu.ToString("N0");
            }
        }

        public RelayCommand XacNhanGiaoDichCommand { get; set; }

        public GiaoDichViewModel(ObservableCollection<TaiKhoan> danhSachTaiKhoan, ObservableCollection<GiaoDich> danhSachGiaoDich, Action callbackDuLieuThayDoi)
        {
            dsTaiKhoan = danhSachTaiKhoan;
            dsGiaoDich = danhSachGiaoDich;
            sauKhiDuLieuThayDoi = callbackDuLieuThayDoi;

            XacNhanGiaoDichCommand = new RelayCommand(o => XacNhanGiaoDich(), o => CoTheXacNhan());

            IsGuiTien = true;
            SoTienGD = "";
            NoiDungGD = "";
            CapNhatTomTat();
        }

        private bool CoTheXacNhan()
        {
            int soTien;
            return TaiKhoanNguonGD != null
            && !string.IsNullOrWhiteSpace(LoaiGiaoDich)
                && int.TryParse(SoTienGD, out soTien)
                && soTien > 0;
        }

        private void XacNhanGiaoDich()
        {
            if (TaiKhoanNguonGD == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản nguồn");
                return;
            }

            if (TaiKhoanNguonGD.TrangThai == "Khóa")
            {
                MessageBox.Show("Tài khoản nguồn đang bị khóa, không thể giao dịch");
                return;
            }

            int soTien;
            if (!int.TryParse(SoTienGD, out soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền giao dịch phải > 0");
                return;
            }

            if (LoaiGiaoDich == "Rút tiền" && TaiKhoanNguonGD.SoDu < soTien)
            {
                MessageBox.Show("Số dư không đủ để rút tiền");
                return;
            }

            if (LoaiGiaoDich == "Chuyển khoản")
            {
                if (TaiKhoanDichGD == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản đích");
                    return;
                }

                if (TaiKhoanDichGD.SoTK == TaiKhoanNguonGD.SoTK)
                {
                    MessageBox.Show("Không thể chuyển khoản cùng một tài khoản");
                    return;
                }

                if (TaiKhoanDichGD.TrangThai == "Khóa")
                {
                    MessageBox.Show("Tài khoản đích đang bị khóa, không thể giao dịch");
                    return;
                }

                if (TaiKhoanNguonGD.SoDu < soTien)
                {
                    MessageBox.Show("Số dư không đủ để chuyển khoản");
                    return;
                }
            }

            if (LoaiGiaoDich == "Gửi tiền")
            {
                TaiKhoanNguonGD.SoDu += soTien;
            }
            else if (LoaiGiaoDich == "Rút tiền")
            {
                TaiKhoanNguonGD.SoDu -= soTien;
            }
            else if (LoaiGiaoDich == "Chuyển khoản")
            {
                TaiKhoanNguonGD.SoDu -= soTien;
                TaiKhoanDichGD.SoDu += soTien;
            }

            var maGD = "GD" + (dsGiaoDich.Count + 1).ToString("0000");
            var giaoDich = new GiaoDich(maGD, DateTime.Now, LoaiGiaoDich, TaiKhoanNguonGD, TaiKhoanDichGD, soTien, NoiDungGD ?? "");
            dsGiaoDich.Add(giaoDich);

            SoTienGD = "";
            NoiDungGD = "";
            CapNhatTomTat();

            sauKhiDuLieuThayDoi?.Invoke();
            MessageBox.Show("Thực hiện giao dịch thành công");
        }

        public void CapNhatTomTat()
        {
            OnPropertyChanged(nameof(TomTatLoaiGiaoDich));
            OnPropertyChanged(nameof(TomTatSoTien));
            OnPropertyChanged(nameof(PhiGiaoDich));
            OnPropertyChanged(nameof(SoDuSauGiaoDich));
            OnPropertyChanged(nameof(SoDuHienTai));
        }
    }
}
