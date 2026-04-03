using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _38_NguyenLeAnhKhoa_Tuan05.Model
{
    internal class Student_BTTL
    {
        private string name;
        private string gioitinh;
        private int age;
        private string tp;

        public string Name
        { 
            get { return name; } 
            set { name = value; } 
        }
        public string Gioitinh
        { 
            get { return gioitinh; } 
            set { gioitinh = value; } 
        }
        public int Age
        { 
            get { return age; } 
            set { age = value; } 
        }
        public string Tp
        { 
            get { return tp; } 
            set { tp = value; } 
        }
        public Student_BTTL(string ten,bool phai,int tuoi, string thanhpho)
        {
            Name = ten;
            if (phai == true) { Gioitinh = "Nam"; }
            else Gioitinh = "Nữ";
            Age = tuoi;
            Tp = thanhpho;
        }
    }
}
