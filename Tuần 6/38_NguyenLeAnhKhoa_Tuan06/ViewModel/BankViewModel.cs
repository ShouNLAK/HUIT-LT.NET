using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _38_NguyenLeAnhKhoa_Tuan06.Model;

namespace _38_NguyenLeAnhKhoa_Tuan06.ViewModel
{
    internal class BankViewModel : BaseViewModel
    {
        private ObservableCollection<String> dsTP;
        public ObservableCollection<String> DSTP
        {
            get { return dsTP; }
            set { dsTP = value; }
        }
        private ObservableCollection<BankModel> dsAccount;
        public ObservableCollection<BankModel> DSAccount
        {
            get { return dsAccount; }
            set { dsAccount = value; }
        }
        private BankModel selectedAccount;
        public BankModel SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                selectedAccount = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private bool isAdding;
        public bool IsAdding
        {
            get { return isAdding; }
            set
            {
                isAdding = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(AddButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
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
                OnPropertyChanged(nameof(EditButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsInputEnabled => IsAdding || IsEditing;
        public string AddButtonText => IsAdding ? "Hủy" : "Thêm";
        public string EditButtonText => IsEditing ? "Hủy sửa" : "Sửa";
        public int TotalBalance
        {
            get { return DSAccount.Sum(a => a.SoTien); }
        }
        public string TongTien
        {
            get { return "Tổng tiền:    " + TotalBalance; }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }
        public BankViewModel()
        {
            DSAccount = new ObservableCollection<BankModel>
            {
                new BankModel(1,"AC001","Nguyễn Văn Tâm","140 Lê Trọng Tấn, P. Tây Thạnh","TP. Hồ Chí Minh",15000000),
                new BankModel(2,"AC002","Trần Văn Bình","140 Lê Trọng Tấn, P. Tây Thạnh","Cần Thơ",1200000),
                new BankModel(3,"AC003","Thanh Thức","234/1 Nguyễn Ảnh Thủ, P. Tân Thới Nhất","TP. Hồ Chí Minh",5000000)
            };
            DSTP = new ObservableCollection<string>
            {
                "TP. Hồ Chí Minh",
                "Hà Nội",
                "Đà Nẵng",
                "Cần Thơ",
                "Huế",
            };
            DSAccount.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TongTien));
            };
            AddCommand = new RelayCommand(
                _ => AddOrCancel(),
                _ => !IsEditing
                );
            EditCommand = new RelayCommand(
                _ => EditOrCancel(),
                _ => SelectedAccount != null && !IsAdding
                );
            SaveCommand = new RelayCommand(
                _ => Save(),
                _ => IsAdding || IsEditing
                );
            DeleteCommand = new RelayCommand(
                _ => Delete(),
                _ => SelectedAccount != null && !IsAdding
                );
            foreach (var Account in DSAccount)
            {
                Account.PropertyChanged += Account_PropertyChanged;
            }
        }
        private void Account_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BankModel.SoTien))
            {
                OnPropertyChanged(nameof(TongTien));
            }
        }
        private void AddOrCancel()
        {
            if (IsAdding)
            {
                ResetState();
                return;
            }
            SelectedAccount = new BankModel();
            IsAdding = true;
        }
        private void EditOrCancel()
        {
            if ((IsEditing))
            {
                ResetState();
                return ;
            }
            IsEditing = true;
        }

        private void Save()
        {
            if (!Validate())
            {
                return;
            }
            if (IsAdding)
            {
                SelectedAccount.STT = DSAccount.Count + 1;
                DSAccount.Add(SelectedAccount);
                SelectedAccount.PropertyChanged += Account_PropertyChanged;
            }
            ResetState();
        }

        private void Delete()
        {
            if (SelectedAccount == null)
                return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;
            SelectedAccount.PropertyChanged -= Account_PropertyChanged;
            DSAccount.Remove(SelectedAccount);
            for (int i = 0; i < DSAccount.Count; i++)
                DSAccount[i].STT = i + 1;
            SelectedAccount = null;
        }
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(SelectedAccount.SoTK))
            {
                MessageBox.Show("Số tài khoản không được để trống");
                return false;
            }
            if (string.IsNullOrWhiteSpace(SelectedAccount.TenKH))
            {
                MessageBox.Show("Tên khách hàng không được để trống");
                return false;
            }
            if (SelectedAccount.SoTien < 0)
            {
                MessageBox.Show("Số tiền không hợp lệ");
                return false;
            }
            bool isDuplicate = DSAccount.Any(a => a.SoTK == SelectedAccount.SoTK && a != SelectedAccount);
            if (isDuplicate)
            {
                MessageBox.Show("Số tài khoản đã tồn tại");
                return false;
            }
            return true;
        }
        private void ResetState()
        {
            IsAdding = false;
            IsEditing = false;
            SelectedAccount = null;
            OnPropertyChanged(nameof(TongTien));
        }
    }
}
