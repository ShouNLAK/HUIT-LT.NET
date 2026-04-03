using _38_NguyenLeAnhKhoa_Tuan06.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;


namespace _38_NguyenLeAnhKhoa_Tuan06.ViewModel
{
    internal class PBNVViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentModel> Departments { get; set; } = new ObservableCollection<DepartmentModel>();
        private DepartmentModel selectDepartment;
        public DepartmentModel SelectDepartment
        {
            get => selectDepartment;
            set
            {
                if (selectDepartment == value) return;
                selectDepartment = value;
                OnPropertyChanged();
                if (!IsAdding && !IsEditing)
                    SelectedDepartmentInForm = selectDepartment;
                SelectedEmployee = null;
                OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment));
                OnPropertyChanged(nameof(TotalEmployeeCount));
                AddCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
                RemoveDepartmentCommand?.RaiseCanExecuteChanged();
            }
        }
        private EmployeeModel selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                if (selectedEmployee == value) return;
                selectedEmployee = value;
                OnPropertyChanged();
                if (selectedEmployee != null && !IsAdding && !IsEditing)
                {
                    var dept = Departments.FirstOrDefault(d => d.Employees.Contains(selectedEmployee));
                    SelectedDepartmentInForm = dept ?? SelectDepartment;
                    EmployeeIDInput = selectedEmployee.IDNhanVien;
                    FullNameInput = selectedEmployee.NameNhanVien;
                    AddressInput = selectedEmployee.DiaChi;
                }
                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
                SaveCommand?.RaiseCanExecuteChanged();
            }
        }
        private DepartmentModel selectedDepartmentInForm;
        public DepartmentModel SelectedDepartmentInForm
        {
            get => selectedDepartmentInForm;
            set { selectedDepartmentInForm = value; OnPropertyChanged(); }
        }
        private string employeeIDInput;
        public string EmployeeIDInput
        {
            get { return employeeIDInput; }
            set
            {
                employeeIDInput = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string fullNameInput;
        public string FullNameInput
        {
            get { return fullNameInput; }
            set
            {
                fullNameInput = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private string addressInput;
        public string AddressInput
        {
            get { return addressInput; }
            set
            {
                addressInput = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private string newDepartmentName;
        public string NewDepartmentName
        {
            get { return newDepartmentName; }
            set
            {
                newDepartmentName = value;
                OnPropertyChanged();
                AddDepartmentCommand.RaiseCanExecuteChanged();
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged();
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
                OnPropertyChanged(nameof(AddButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
                OnPropertyChanged(nameof(IsEmployeeIdEnabled));
                SaveCommand.RaiseCanExecuteChanged();

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
                OnPropertyChanged(nameof(AddButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
                OnPropertyChanged(nameof(IsEmployeeIdEnabled));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsInputEnabled => IsAdding || IsEditing;
        public bool IsEmployeeIdEnabled => IsAdding;
        public string AddButtonText => IsAdding ? "Hủy" : "Thêm";
        public string EditButtonText => IsEditing ? "Hủy" : "Sửa";
        public int EmployeeCountInSelectedDepartment => SelectDepartment?.Employees.Count ?? 0;
        public int TotalEmployeeCount => Departments.Sum(d => d.Employees.Count);
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand AddDepartmentCommand { get; }
        public RelayCommand RemoveDepartmentCommand { get; }

        private void CreatedData()
        {
            var d1 = new DepartmentModel ( "Giám đốc" );
            d1.Employees.Add(new EmployeeModel("NV001", "Nguyễn Văn Bình", "TP. Hồ Chí Minh"));
            d1.Employees.Add(new EmployeeModel("NV002", "Trần Thị Dương", "Tây Ninh"));
            var d2 = new DepartmentModel("Quan trị hệ thống");
            var d3 = new DepartmentModel("Kế hoạch tài chính");
            Departments.Add(d1);
            Departments.Add(d2);
            Departments.Add(d3);
        }
        public PBNVViewModel()
        {
            CreatedData();
            AddCommand = new RelayCommand(_ => AddOrCancel(), _ => SelectDepartment != null && !IsEditing);
            EditCommand = new RelayCommand(_ => EditOrCancel(), _ => SelectedEmployee != null && !IsAdding);
            SaveCommand = new RelayCommand(_ => Save(), _ => (IsAdding || IsEditing));
            DeleteCommand = new RelayCommand(_ => Delete(), _ => SelectedEmployee != null && !IsAdding && !IsEditing);
            AddDepartmentCommand = new RelayCommand(_ => AddDepartment(), _ => !string.IsNullOrWhiteSpace(NewDepartmentName));
            RemoveDepartmentCommand = new RelayCommand(_ => RemoveDepartment(), _ => SelectDepartment != null && !IsAdding && !IsEditing);
            SelectDepartment = Departments.FirstOrDefault();
            SelectedDepartmentInForm = SelectDepartment;
            Departments.CollectionChanged += (s, e) => { OnPropertyChanged(nameof(TotalEmployeeCount)); };
            foreach (var d in Departments)
                AttachEmployeeHandler(d);
        }
        private void AddDepartment()
        {
            ErrorMessage = "";
            var name = (NewDepartmentName ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name)) return;
            bool dup = Departments.Any(d => d.Ten.Equals(name, StringComparison.OrdinalIgnoreCase));
            if(dup)
            {
                MessageBox.Show("Tên phòng ban đã tồn tại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dep = new DepartmentModel(name);
            Departments.Add(dep);
            AttachEmployeeHandler(dep);
            OnPropertyChanged(nameof(TotalEmployeeCount));
            NewDepartmentName = "";
        }

        private void AttachEmployeeHandler(DepartmentModel d)
        {
            if (d == null) return;
            d.Employees.CollectionChanged += (s, e) => { OnPropertyChanged(nameof(TotalEmployeeCount)); OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment)); };
        }
        private void RemoveDepartment()
        {
            if (SelectDepartment == null) return;
            var result = MessageBox.Show($"Bạn có chắc muốn xóa phòng ban '{SelectDepartment.Ten}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            var depToRemove = SelectDepartment;
            var index = Departments.IndexOf(depToRemove);
            Departments.Remove(depToRemove);
            SelectDepartment = Departments.Count == 0 ? null : Departments[Math.Min(index, Departments.Count - 1)];
            SelectedDepartmentInForm = SelectDepartment;
        }
        private void AddOrCancel()
        {
            if (IsAdding)
            {
                ResetState();
                return;
            }
            SelectedEmployee = null;
            EmployeeIDInput = "";
            FullNameInput = "";
            AddressInput = "";
            IsAdding = true;
            SelectedDepartmentInForm = SelectDepartment;
        }
        private void EditOrCancel()
        {
            if (IsEditing)
            {
                ResetState();
                return;
            }
            if (SelectedEmployee == null) return;
            IsEditing = true;
            EmployeeIDInput = SelectedEmployee.IDNhanVien;
            FullNameInput = SelectedEmployee.NameNhanVien;
            AddressInput = SelectedEmployee.DiaChi;
            SelectedDepartmentInForm = SelectDepartment;
        }
        private void Save()
        {
            if (!Validate()) return;
            if (IsAdding)
            {
                var emp = new EmployeeModel(EmployeeIDInput, FullNameInput, AddressInput);
                SelectedDepartmentInForm?.Employees.Add(emp);
            }
            else if (IsEditing)
            {
                var currentDept = Departments.FirstOrDefault(d => d.Employees.Contains(SelectedEmployee));
                if (currentDept != null)
                {
                    if (SelectedDepartmentInForm != null && SelectedDepartmentInForm != currentDept)
                    {
                        currentDept.Employees.Remove(SelectedEmployee);
                        SelectedDepartmentInForm.Employees.Add(SelectedEmployee);
                    }
                    SelectedEmployee.IDNhanVien = EmployeeIDInput;
                    SelectedEmployee.NameNhanVien = FullNameInput;
                    SelectedEmployee.DiaChi = AddressInput;
                }
            }
            ResetState();
            OnPropertyChanged(nameof(TotalEmployeeCount));
            OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment));
        }
        private void Delete()
        {
            if (SelectedEmployee == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            var dep = Departments.FirstOrDefault(d => d.Employees.Contains(SelectedEmployee));
            if (dep != null)
                dep.Employees.Remove(SelectedEmployee);
            SelectedEmployee = null;
            OnPropertyChanged(nameof(TotalEmployeeCount));
            OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment));
        }
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(EmployeeIDInput))
            {
                MessageBox.Show("Mã nhân viên không được để trống");
                return false;
            }
            if (string.IsNullOrWhiteSpace(FullNameInput))
            {
                MessageBox.Show("Họ tên không được để trống");
                return false;
            }
            if (SelectedDepartmentInForm == null)
            {
                MessageBox.Show("Phải chọn phòng ban");
                return false;
            }
            bool dup = SelectedDepartmentInForm.Employees.Any(e => e.IDNhanVien.Equals(EmployeeIDInput, StringComparison.OrdinalIgnoreCase) && (IsAdding || e != SelectedEmployee));
            if (dup)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại trong phòng ban này");
                return false;
            }
            return true;
        }
        private void ResetState()
        {
            IsAdding = false;
            IsEditing = false;
            SelectedEmployee = null;
            EmployeeIDInput = FullNameInput = AddressInput = "";
            SelectedDepartmentInForm = SelectDepartment;
            AddCommand?.RaiseCanExecuteChanged();
            EditCommand?.RaiseCanExecuteChanged();
            DeleteCommand?.RaiseCanExecuteChanged();
            SaveCommand?.RaiseCanExecuteChanged();
        }
    }
}
