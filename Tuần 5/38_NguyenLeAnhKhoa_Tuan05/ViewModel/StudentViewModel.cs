using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan05.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Net.Http.Headers;
    using System.Windows.Data;
    using _38_NguyenLeAnhKhoa_Tuan05.Model;
    internal class StudentViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students 
        { 
            get => _students;
            set {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                if (_selectedStudent != null)
                {
                    NewName=_selectedStudent.Name;
                    NewAge=_selectedStudent.Age;
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

        public StudentViewModel() 
        {
            Students = new ObservableCollection<Student>
            {
                new Student ("Nguyễn Văn An",20),
                new Student ("Trần Ngọc Bình",20),
                new Student ("Hoàng Ngọc Chi",21)
            };
            StudentsView = CollectionViewSource.GetDefaultView(Students);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ClearInput()
        {
            NewName = string.Empty;
            NewAge = 0;
        }

        public void ThemSV()
        {
            if (string.IsNullOrWhiteSpace(NewName) || NewAge <= 0)
                return;
            Students.Add(new Student (NewName, NewAge));
            ClearInput();
        }

        public void XoaSV()
        {
            if (SelectedStudent != null)
                Students.Remove(SelectedStudent);
        }

        private ICollectionView _studentsView;
        public ICollectionView StudentsView
        {
            get { return _studentsView; }
            set
            {
                _studentsView = value;
                OnPropertyChanged(nameof(StudentsView));
            }
        }


        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set { 
                _filterText = value; 
                OnPropertyChanged(nameof(FilterText)); 
            }
        }

        private bool _isSortAscending = true;
        
        public void ApplyFilter()
        {
            if (StudentsView == null) return;
            StudentsView.Filter = obj =>
            {
                if (string.IsNullOrWhiteSpace(FilterText))
                    return true;
                Student sv = obj as Student;
                return sv != null && sv.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
            };
            StudentsView.Refresh();
        }

        public void SortByAge()
        {
            if (StudentsView == null) return;
            StudentsView.SortDescriptions.Clear();
            if (_isSortAscending)
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            else
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Descending));
            _isSortAscending =!_isSortAscending;
        }
    }
}
