using _18_NguyenLeAnhKhoa.Model;
using _18_NguyenLeAnhKhoa_Tuan07.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace _18_NguyenLeAnhKhoa.ViewModel
{
    internal class Bai2_ViewModel : BaseViewModel
    {
        private ObservableCollection<NhomDichVu> ds_NhomDV;
        public ObservableCollection<NhomDichVu> DS_NhomDV
        {
            get { return ds_NhomDV; }
            set
            {
                ds_NhomDV = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DichVu> ds_DichVu;
        public ObservableCollection<DichVu> DS_DichVu
        {
            get { return ds_DichVu; }
            set
            {
                ds_DichVu = value;
                OnPropertyChanged();
            }
        }

        private NhomDichVu selected_NhomDichVu;
        public NhomDichVu Selected_NhomDichVu
        {
            get { return selected_NhomDichVu; }
            set
            {
                selected_NhomDichVu = value;
                if (selected_NhomDichVu == null)
                    DS_DichVu_Loc = new ObservableCollection<DichVu>();
                else
                    DS_DichVu_Loc = new ObservableCollection<DichVu>(DS_DichVu.Where(o => o.MaNhom.MaNhomDV == selected_NhomDichVu.MaNhomDV));
                Selected_DichVu = null;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DichVu> ds_DichVu_Loc;
        public ObservableCollection<DichVu> DS_DichVu_Loc
        {
            get { return ds_DichVu_Loc; }
            set
            {
                ds_DichVu_Loc = value;
                OnPropertyChanged();
            }
        }

        private DichVu selected_DichVu;
        public DichVu Selected_DichVu
        {
            get { return selected_DichVu; }
            set
            {
                selected_DichVu = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChiTietDichVu> ds_ChiTietDV;
        public ObservableCollection<ChiTietDichVu> DS_ChiTietDV
        {
            get { return ds_ChiTietDV; }
            set
            {
                ds_ChiTietDV = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PhieuDichVu> ds_PhieuDV;
        public ObservableCollection<PhieuDichVu> DS_PhieuDV
        {
            get { return ds_PhieuDV; }
            set
            {
                ds_PhieuDV = value;
                OnPropertyChanged();
            }
        }

        private string hoTenKH;
        public string HoTenKH
        {
            get { return hoTenKH; }
            set
            {
                hoTenKH = value;
                OnPropertyChanged();
            }
        }

        private string SDT;
        public string SoDienThoai
        {
            get { return SDT; }
            set
            {
                SDT = value;
                OnPropertyChanged();
            }
        }

        private string diaChi;
        public string DiaChi
        {
            get { return diaChi; }
            set
            {
                diaChi = value;
                OnPropertyChanged();
            }
        }

        private string soPhong;
        public string SoPhong
        {
            get { return soPhong; }
            set
            {
                soPhong = value;
                OnPropertyChanged();
            }
        }

        private int soLanSuDung;
        public int SoLanSuDung
        {
            get { return soLanSuDung; }
            set
            {
                soLanSuDung = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChiTietDichVu> DSDV
        {
            get { return DS_ChiTietDV; }
            set { DS_ChiTietDV = value; }
        }

        public int TongTien
        {
            get { return DSDV.Sum(o => o.ThanhTien); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private bool isValid()
        {
            if (string.IsNullOrWhiteSpace(HoTenKH) ||
                    string.IsNullOrWhiteSpace(SoDienThoai) ||
                    string.IsNullOrWhiteSpace(DiaChi) ||
                    string.IsNullOrWhiteSpace(SoPhong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thiếu dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DSDV.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 dịch vụ", "Thiếu dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false ;
            }
            return true;
        }

        public Bai2_ViewModel()
        {
            var duLieu = new KhoDuLieu();
            DS_NhomDV = duLieu.DS_NhomDV;
            DS_DichVu = duLieu.DS_DichVu;
            DS_DichVu_Loc = new ObservableCollection<DichVu>();
            DS_ChiTietDV = new ObservableCollection<ChiTietDichVu>();
            DS_PhieuDV = new ObservableCollection<PhieuDichVu>();
            SoLanSuDung = 1;

            AddCommand = new RelayCommand((o) =>
            {
                if (Selected_DichVu == null) return;

                var ct = new ChiTietDichVu(Selected_DichVu.MaDV, Selected_DichVu.TenDV, SoLanSuDung, Selected_DichVu.DonGia);
                DSDV.Add(ct);
                OnPropertyChanged(nameof(DSDV));
                OnPropertyChanged(nameof(TongTien));
                SoLanSuDung = 1;
            }, (o) => Selected_DichVu != null && SoLanSuDung > 0);

            ConfirmCommand = new RelayCommand((o) =>
            {
                if (!isValid())
                    return;
                string message =
                    "Họ tên : " + HoTenKH + "\n" +
                    "SĐT : " + SoDienThoai + "\n" +
                    "Địa chỉ : " + DiaChi + "\n" +
                    "Số phòng : " + SoPhong + "\n";
                foreach (var ct in DSDV)
                {
                    message += ct.MaDV + " - " + ct.TenDV + " - " + ct.SoLanSD + " - " + ct.ThanhTien + "\n";
                }

                message += "Tổng tiền: " + TongTien;
                MessageBox.Show(message, "Kết quả", MessageBoxButton.OK, MessageBoxImage.Information);
                resetState();
            });
        }

        public void resetState()
        {
            HoTenKH = "";
            DiaChi = "";
            SoDienThoai = "";
            SoPhong = "";
            selected_NhomDichVu = null;
            Selected_DichVu = null;
            SoLanSuDung = 0;
            DSDV.Clear();
        }
    }
}
