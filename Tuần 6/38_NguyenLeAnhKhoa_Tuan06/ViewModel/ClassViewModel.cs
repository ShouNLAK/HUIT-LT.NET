using _38_NguyenLeAnhKhoa_Tuan06.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace _38_NguyenLeAnhKhoa_Tuan06.ViewModel
{
    internal class ClassViewModel : BaseViewModel
    {
        public ObservableCollection<ClassModel> Classes { get; set; } = new ObservableCollection<ClassModel>();
        public ObservableCollection<StudentModel> FilteredStudents { get; set; } = new ObservableCollection<StudentModel>();
        private ClassModel selectedClass;
        public ClassModel SelectedClass
        {
            get => selectedClass;
            set
            {
                if (selectedClass == value) return;
                selectedClass = value;
                OnPropertyChanged();
                SelectedStudent = null;
                UpdateFilteredStudents();
                OnPropertyChanged(nameof(StudentCountInSelectedClass));
            }
        }
        private StudentModel selectedStudent;
        public StudentModel SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged();
                if (selectedStudent != null && !IsAdding && !IsEditing)
                {
                    MaSVInput = selectedStudent.MaSV;
                    HoTenInput = selectedStudent.HoTen;
                    DiaChiInput = selectedStudent.DiaChi;
                    Diem1Input = selectedStudent.Diem1.ToString();
                    Diem2Input = selectedStudent.Diem2.ToString();
                    Diem3Input = selectedStudent.Diem3.ToString();
                }
                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }
        private string maSVInput;
        public string MaSVInput { get => maSVInput; set { maSVInput = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string hoTenInput;
        public string HoTenInput { get => hoTenInput; set { hoTenInput = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string diaChiInput;
        public string DiaChiInput { get => diaChiInput; set { diaChiInput = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string diem1Input;
        public string Diem1Input { get => diem1Input; set { diem1Input = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string diem2Input;
        public string Diem2Input { get => diem2Input; set { diem2Input = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string diem3Input;
        public string Diem3Input { get => diem3Input; set { diem3Input = value; OnPropertyChanged(); SaveCommand?.RaiseCanExecuteChanged(); } }
        private string newClassName;
        public string NewClassName { get => newClassName; set { newClassName = value; OnPropertyChanged(); AddClassCommand?.RaiseCanExecuteChanged(); } }
        private string searchKeyword;
        public string SearchKeyword { get => searchKeyword; set { searchKeyword = value; OnPropertyChanged(); } }
        public int StudentCountInSelectedClass => SelectedClass?.Students.Count ?? 0;
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand AddClassCommand { get; }
        public RelayCommand RemoveClassCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ShowAllCommand { get; }
        private bool isAdding;
        public bool IsAdding { get => isAdding; set { isAdding = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsInputEnabled)); OnPropertyChanged(nameof(AddButtonText)); SaveCommand?.RaiseCanExecuteChanged(); } }
        private bool isEditing;
        public bool IsEditing { get => isEditing; set { isEditing = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsInputEnabled)); OnPropertyChanged(nameof(EditButtonText)); SaveCommand?.RaiseCanExecuteChanged(); } }
        public bool IsInputEnabled => IsAdding || IsEditing;
        public string AddButtonText => IsAdding ? "Hủy" : "Thêm";
        public string EditButtonText => IsEditing ? "Hủy" : "Sửa";

        private void CreatedData()
        {
            var c1 = new ClassModel("Lớp A") {
                Students = { new StudentModel("SV001","Nguyễn Văn A","HCM",8,7.5,9), new StudentModel("SV002","Trần Thị B","HCM",6,7,6.5) }
            };
            var c2 = new ClassModel("Lớp B");
            Classes.Add(c1);
            Classes.Add(c2);
        }
        public ClassViewModel()
        {
            CreatedData();
            AddCommand = new RelayCommand(_ => AddOrCancel(), _ => SelectedClass != null && !IsEditing);
            EditCommand = new RelayCommand(_ => EditOrCancel(), _ => SelectedStudent != null && !IsAdding);
            SaveCommand = new RelayCommand(_ => Save(), _ => IsAdding || IsEditing);
            DeleteCommand = new RelayCommand(_ => Delete(), _ => SelectedStudent != null && !IsAdding && !IsEditing);
            AddClassCommand = new RelayCommand(_ => AddClass(), _ => !string.IsNullOrWhiteSpace(NewClassName));
            RemoveClassCommand = new RelayCommand(_ => RemoveClass(), _ => SelectedClass != null && !IsAdding && !IsEditing);
            SearchCommand = new RelayCommand(_ => SearchStudents(), _ => SelectedClass != null);
            ShowAllCommand = new RelayCommand(_ => ShowAllStudents(), _ => SelectedClass != null);
            SelectedClass = Classes.FirstOrDefault();
        }
        private void AddOrCancel()
        {
            if (IsAdding) { ResetState(); return; }
            SelectedStudent = null;
            MaSVInput = HoTenInput = DiaChiInput = Diem1Input = Diem2Input = Diem3Input = "";
            IsAdding = true;
        }
        private void EditOrCancel()
        {
            if (IsEditing) { ResetState(); return; }
            if (SelectedStudent == null) return;
            IsEditing = true;
            MaSVInput = SelectedStudent.MaSV;
            HoTenInput = SelectedStudent.HoTen;
            DiaChiInput = SelectedStudent.DiaChi;
            Diem1Input = SelectedStudent.Diem1.ToString();
            Diem2Input = SelectedStudent.Diem2.ToString();
            Diem3Input = SelectedStudent.Diem3.ToString();
        }
        private void Save()
        {
            if (!Validate()) return;
            double d1 = double.Parse(string.IsNullOrWhiteSpace(Diem1Input) ? "0" : Diem1Input);
            double d2 = double.Parse(string.IsNullOrWhiteSpace(Diem2Input) ? "0" : Diem2Input);
            double d3 = double.Parse(string.IsNullOrWhiteSpace(Diem3Input) ? "0" : Diem3Input);
            if (IsAdding)
            {
                var s = new StudentModel(MaSVInput, HoTenInput, DiaChiInput, d1, d2, d3);
                SelectedClass?.Students.Add(s);
            }
            else if (IsEditing)
            {
                var cur = SelectedClass?.Students.FirstOrDefault(x => x == SelectedStudent);
                if (cur != null)
                {
                    cur.MaSV = MaSVInput;
                    cur.HoTen = HoTenInput;
                    cur.DiaChi = DiaChiInput;
                    cur.Diem1 = d1;
                    cur.Diem2 = d2;
                    cur.Diem3 = d3;
                }
            }
            ResetState();
            UpdateFilteredStudents();
            OnPropertyChanged(nameof(StudentCountInSelectedClass));
        }
        private void Delete()
        {
            if (SelectedStudent == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?","Xác nhận",MessageBoxButton.YesNo,MessageBoxImage.Question)!=MessageBoxResult.Yes) return;
            SelectedClass?.Students.Remove(SelectedStudent);
            SelectedStudent = null;
            UpdateFilteredStudents();
            OnPropertyChanged(nameof(StudentCountInSelectedClass));
        }
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(MaSVInput)) { MessageBox.Show("Mã sinh viên không được để trống"); return false; }
            if (string.IsNullOrWhiteSpace(HoTenInput)) { MessageBox.Show("Họ tên không được để trống"); return false; }
            if (!double.TryParse(Diem1Input, out double d1) || d1 < 0 || d1 > 10) { MessageBox.Show("Điểm 1 không hợp lệ"); return false; }
            if (!double.TryParse(Diem2Input, out double d2) || d2 < 0 || d2 > 10) { MessageBox.Show("Điểm 2 không hợp lệ"); return false; }
            if (!double.TryParse(Diem3Input, out double d3) || d3 < 0 || d3 > 10) { MessageBox.Show("Điểm 3 không hợp lệ"); return false; }
            bool dup = SelectedClass?.Students.Any(s => s.MaSV.Equals(MaSVInput, StringComparison.OrdinalIgnoreCase) && (IsAdding || s != SelectedStudent)) ?? false;
            if (dup) { MessageBox.Show("Mã sinh viên đã tồn tại"); return false; }
            return true;
        }
        private void ResetState()
        {
            IsAdding = false;
            IsEditing = false;
            SelectedStudent = null;
            MaSVInput = HoTenInput = DiaChiInput = Diem1Input = Diem2Input = Diem3Input = "";
            AddCommand?.RaiseCanExecuteChanged();
            EditCommand?.RaiseCanExecuteChanged();
            DeleteCommand?.RaiseCanExecuteChanged();
            SaveCommand?.RaiseCanExecuteChanged();
        }
        private void AddClass()
        {
            var name = (NewClassName ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name)) return;
            bool dup = Classes.Any(c => c.TenLop.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (dup) { MessageBox.Show("Tên lớp đã tồn tại"); return; }
            Classes.Add(new ClassModel(name));
            NewClassName = "";
        }
        private void RemoveClass()
        {
            if (SelectedClass == null) return;
            if (MessageBox.Show($"Bạn muốn xóa lớp '{SelectedClass.TenLop}'?","Xác nhận",MessageBoxButton.YesNo,MessageBoxImage.Question)!=MessageBoxResult.Yes) return;
            var idx = Classes.IndexOf(SelectedClass);
            Classes.Remove(SelectedClass);
            SelectedClass = Classes.Count == 0 ? null : Classes[Math.Min(idx, Classes.Count - 1)];
        }

        private void SearchStudents()
        {
            UpdateFilteredStudents();
        }

        private void ShowAllStudents()
        {
            SearchKeyword = "";
            UpdateFilteredStudents();
        }

        private void UpdateFilteredStudents()
        {
            FilteredStudents.Clear();
            if (SelectedClass == null) return;
            var keyword = (SearchKeyword ?? "").Trim().ToLower();
            foreach (var s in SelectedClass.Students)
            {
                if (string.IsNullOrEmpty(keyword) || s.MaSV.ToLower().Contains(keyword) || s.HoTen.ToLower().Contains(keyword))
                {
                    FilteredStudents.Add(s);
                }
            }
        }
    }
}
