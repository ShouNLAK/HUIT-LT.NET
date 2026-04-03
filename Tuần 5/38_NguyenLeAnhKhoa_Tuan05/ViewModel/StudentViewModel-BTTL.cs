using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using _38_NguyenLeAnhKhoa_Tuan05.Model;


namespace _38_NguyenLeAnhKhoa_Tuan05.ViewModel
{
    internal class StudentViewModel_BTTL
    {
        private readonly List<Student_BTTL> _initialStudents;

        private ObservableCollection<Student_BTTL> _students;
        public ObservableCollection<Student_BTTL> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }
        public StudentViewModel_BTTL()
        {
            Students = new ObservableCollection<Student_BTTL>
            {
                new Student_BTTL ("Nguyễn Văn An",true,20,"Hà Nội"),
                new Student_BTTL ("Trần Ngọc Bình",true,20,"Đà Nẵng"),
                new Student_BTTL ("Hoàng Ngọc Chi",false,21,"TP. Hồ Chí Minh")
            };

            _initialStudents = Students
                .Select(s => new Student_BTTL(s.Name, s.Gioitinh == "Nam", s.Age, s.Tp))
                .ToList();

            StudentsViewBTTL = CollectionViewSource.GetDefaultView(Students);
            UpdateStat();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICollectionView _studentsViewBTTL;
        public ICollectionView StudentsViewBTTL
        {
            get { return _studentsViewBTTL; }
            set
            {
                _studentsViewBTTL = value;
                OnPropertyChanged(nameof(StudentsViewBTTL));
            }
        }

        private Student_BTTL _selectedStudent;
        public Student_BTTL SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                if (_selectedStudent != null)
                {
                    NewName = _selectedStudent.Name;
                    NewAge = _selectedStudent.Age;
                    NewPhai = _selectedStudent.Gioitinh == "Nam";
                    NewCity = _selectedStudent.Tp;
                }
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }


        private string _newName;
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }

        private int _newAge;
        public int NewAge
        {
            get => _newAge;
            set
            {
                _newAge = value;
                OnPropertyChanged(nameof(NewAge));
            }
        }

        private bool _newPhai;
        public bool NewPhai
        {
            get => _newPhai;
            set
            {
                if (_newPhai == value)
                    return;
                _newPhai = value;
                OnPropertyChanged(nameof(NewPhai));
                OnPropertyChanged(nameof(NewNu));
            }
        }

        public bool NewNu
        {
            get => !_newPhai;
            set
            {
                if (value)
                    NewPhai = false;
            }
        }

        private string _newCity;
        public string NewCity
        {
            get => _newCity;
            set
            {
                _newCity = value;
                OnPropertyChanged(nameof(NewCity));
            }
        }

        private int _stat;
        public int Stat
        {
            get => _stat;
            private set
            {
                _stat = value;
                OnPropertyChanged(nameof(Stat));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ApplyFilter();
            }
        }

        private void UpdateStat()
        {
            Stat = Students.Count;
        }

        public void ApplyFilter()
        {
            if (StudentsViewBTTL == null)
                return;

            StudentsViewBTTL.Filter = obj =>
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                    return true;

                var sv = obj as Student_BTTL;
                return sv != null && sv.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            StudentsViewBTTL.Refresh();
        }

        public void TaiLai()
        {
            Students.Clear();
            foreach (var sv in _initialStudents)
                Students.Add(new Student_BTTL(sv.Name, sv.Gioitinh == "Nam", sv.Age, sv.Tp));

            UpdateStat();
            NewName = string.Empty;
            NewAge = 0;
            NewPhai = true;
            NewCity = string.Empty;
            SearchText = string.Empty;
            SelectedStudent = null;
        }

        public void ThemSV()
        {
            if (string.IsNullOrWhiteSpace(NewName) || NewAge <= 0 || string.IsNullOrWhiteSpace(NewCity))
                return;

            var newStudent = new Student_BTTL(NewName.Trim(), NewPhai, NewAge, NewCity.Trim());
            Students.Add(newStudent);
            SelectedStudent = newStudent;
            UpdateStat();
        }

        public void XoaSV()
        {
            if (SelectedStudent != null)
            {
                Students.Remove(SelectedStudent);
                SelectedStudent = Students.FirstOrDefault();
                UpdateStat();
            }
        }
    }
}
