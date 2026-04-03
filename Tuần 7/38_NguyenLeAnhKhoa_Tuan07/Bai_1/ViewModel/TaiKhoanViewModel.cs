using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_1.Model;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_1.ViewModel
{
    internal class TaiKhoanViewModel : BaseViewModel
    {
        private readonly ObservableCollection<TaiKhoan> dsTaiKhoan;
        private readonly ObservableCollection<GiaoDich> dsGiaoDich;
        private readonly Action sauKhiDuLieuThayDoi;

        public ObservableCollection<TaiKhoan> DSTK
        {
            get { return dsTaiKhoan; }
        }

        public ObservableCollection<string> DSLTK { get; set; }
        public ObservableCollection<string> DSTT { get; set; }

        private TaiKhoan selectedAccount;
        public TaiKhoan SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount == value) return;
                selectedAccount = value;
                OnPropertyChanged();
                if (selectedAccount != null && !IsAdding && !IsEditing)
                {
                    SoTK = selectedAccount.SoTK;
                    ChuTK = selectedAccount.ChuTK;
                    SoDu = selectedAccount.SoDu.ToString();
                    LoaiTK = selectedAccount.LoaiTK;
                    TrangThai = selectedAccount.TrangThai;
                }
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private string soTK;
        public string SoTK
        {
            get { return soTK; }
            set
            {
                soTK = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string chuTK;
        public string ChuTK
        {
            get { return chuTK; }
            set
            {
                chuTK = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string soDu;
        public string SoDu
        {
            get { return soDu; }
            set
            {
                soDu = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string loaiTK;
        public string LoaiTK
        {
            get { return loaiTK; }
            set
            {
                loaiTK = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string trangThai;
        public string TrangThai
        {
            get { return trangThai; }
            set
            {
                trangThai = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private TaiKhoan taiKhoanDangSua;

        private bool isAdding;
        public bool IsAdding
        {
            get { return isAdding; }
            set
            {
                isAdding = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsInputEnabled));
                OnPropertyChanged(nameof(IsSoTKEnable));
                OnPropertyChanged(nameof(AddButtonText));
                SaveCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsInputEnabled));
                OnPropertyChanged(nameof(IsSoTKEnable));
                OnPropertyChanged(nameof(EditButtonText));
                SaveCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsInputEnabled
        {
            get { return IsAdding || IsEditing; }
        }

        public bool IsSoTKEnable
        {
            get { return IsAdding; }
        }

        public string AddButtonText
        {
            get { return IsAdding ? "Hủy" : "Thêm"; }
        }

        public string EditButtonText
        {
            get { return IsEditing ? "Hủy" : "Sửa"; }
        }

        public string TongSoTaiKhoanText
        {
            get { return dsTaiKhoan.Count.ToString(); }
        }

        public string TongSoDuHeThongText
        {
            get { return dsTaiKhoan.Sum(x => x.SoDu).ToString("N0"); }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public TaiKhoanViewModel(ObservableCollection<TaiKhoan> danhSachTaiKhoan, ObservableCollection<GiaoDich> danhSachGiaoDich, Action callbackDuLieuThayDoi)
        {
            dsTaiKhoan = danhSachTaiKhoan;
            dsGiaoDich = danhSachGiaoDich;
            sauKhiDuLieuThayDoi = callbackDuLieuThayDoi;

            DSLTK = new ObservableCollection<string> { "Thanh toán", "Tiết kiệm" };
            DSTT = new ObservableCollection<string> { "Hoạt động", "Khóa" };

            AddCommand = new RelayCommand(o => AddOrCancel(), o => !IsEditing);
            EditCommand = new RelayCommand(o => EditOrCancel(), o => SelectedAccount != null && !IsAdding);
            SaveCommand = new RelayCommand(o => SaveTaiKhoan(), o => IsAdding || IsEditing);
            DeleteCommand = new RelayCommand(o => DeleteTaiKhoan(), o => SelectedAccount != null && !IsAdding && !IsEditing);

            dsTaiKhoan.CollectionChanged += DsTaiKhoan_CollectionChanged;
            foreach (var item in dsTaiKhoan)
            {
                item.PropertyChanged += TaiKhoan_PropertyChanged;
            }

            CapNhatThongKe();
        }

        private void DsTaiKhoan_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (TaiKhoan item in e.OldItems)
                {
                    item.PropertyChanged -= TaiKhoan_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (TaiKhoan item in e.NewItems)
                {
                    item.PropertyChanged += TaiKhoan_PropertyChanged;
                }
            }

            CapNhatThongKe();
        }

        private void TaiKhoan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaiKhoan.SoDu))
            {
                CapNhatThongKe();
            }
        }

        public void CapNhatThongKe()
        {
            OnPropertyChanged(nameof(TongSoTaiKhoanText));
            OnPropertyChanged(nameof(TongSoDuHeThongText));
        }

        private void AddOrCancel()
        {
            if (IsAdding)
            {
                ResetForm();
                return;
            }

            SelectedAccount = null;
            SoTK = "";
            ChuTK = "";
            SoDu = "";
            LoaiTK = DSLTK.FirstOrDefault();
            TrangThai = DSTT.FirstOrDefault();
            IsAdding = true;
        }

        private void EditOrCancel()
        {
            if (IsEditing)
            {
                ResetForm();
                return;
            }

            if (SelectedAccount == null) return;

            taiKhoanDangSua = SelectedAccount;
            SoTK = SelectedAccount.SoTK;
            ChuTK = SelectedAccount.ChuTK;
            SoDu = SelectedAccount.SoDu.ToString();
            LoaiTK = SelectedAccount.LoaiTK;
            TrangThai = SelectedAccount.TrangThai;
            IsEditing = true;
        }

        private void SaveTaiKhoan()
        {
            int soDuValue;
            if (!ValidateTaiKhoan(out soDuValue)) return;

            if (IsAdding)
            {
                var tk = new TaiKhoan(SoTK.Trim(), ChuTK.Trim(), soDuValue, LoaiTK, TrangThai);
                dsTaiKhoan.Add(tk);
            }
            else if (IsEditing && taiKhoanDangSua != null)
            {
                taiKhoanDangSua.ChuTK = ChuTK.Trim();
                taiKhoanDangSua.SoDu = soDuValue;
                taiKhoanDangSua.LoaiTK = LoaiTK;
                taiKhoanDangSua.TrangThai = TrangThai;
            }

            CapNhatThongKe();
            ResetForm();
            sauKhiDuLieuThayDoi?.Invoke();
        }

        private bool ValidateTaiKhoan(out int soDuValue)
        {
            soDuValue = 0;

            if (string.IsNullOrWhiteSpace(SoTK))
            {
                MessageBox.Show("Số tài khoản không được để trống");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ChuTK))
            {
                MessageBox.Show("Chủ tài khoản không được để trống");
                return false;
            }

            if (!int.TryParse(SoDu, out soDuValue) || soDuValue < 0)
            {
                MessageBox.Show("Số dư ban đầu phải là số nguyên và >= 0");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LoaiTK))
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản");
                return false;
            }

            if (LoaiTK == "Tiết kiệm" && soDuValue < 100000)
            {
                MessageBox.Show("Tài khoản tiết kiệm yêu cầu số dư tối thiểu 100000");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TrangThai))
            {
                MessageBox.Show("Vui lòng chọn trạng thái tài khoản");
                return false;
            }

            var soTKTrim = SoTK.Trim();
            bool duplicate = dsTaiKhoan.Any(x => x.SoTK.Equals(soTKTrim, StringComparison.OrdinalIgnoreCase) && x != taiKhoanDangSua);
            if (duplicate)
            {
                MessageBox.Show("Số tài khoản đã tồn tại");
                return false;
            }

            return true;
        }

        private void DeleteTaiKhoan()
        {
            if (SelectedAccount == null) return;

            bool daPhatSinh = dsGiaoDich.Any(x =>
                (x.TKNguon != null && x.TKNguon.SoTK == SelectedAccount.SoTK) ||
                (x.TKDich != null && x.TKDich.SoTK == SelectedAccount.SoTK));

            if (daPhatSinh)
            {
                MessageBox.Show("Tài khoản đã phát sinh giao dịch, không thể xóa");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            dsTaiKhoan.Remove(SelectedAccount);
            ResetForm();
            sauKhiDuLieuThayDoi?.Invoke();
        }

        private void ResetForm()
        {
            IsAdding = false;
            IsEditing = false;
            taiKhoanDangSua = null;
            SelectedAccount = null;
            SoTK = "";
            ChuTK = "";
            SoDu = "";
            LoaiTK = "";
            TrangThai = "";
        }
    }
}
