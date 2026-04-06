using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_NguyenLeAnhKhoa.Model
{
    internal class KhoDuLieu
    {
        private ObservableCollection<NhomDichVu> ds_NhomDV;
        public ObservableCollection<NhomDichVu> DS_NhomDV
        {
            get {  return ds_NhomDV; }
            set { ds_NhomDV = value; }
        }
        private ObservableCollection <DichVu> ds_DichVu;
        public ObservableCollection<DichVu> DS_DichVu
        {
            get { return ds_DichVu; }
            set { ds_DichVu = value; }
        }
        public KhoDuLieu()
        {
            DS_NhomDV = new ObservableCollection<NhomDichVu>()
            {
                new NhomDichVu("DV01","Hỗ trợ lưu trú"),
                new NhomDichVu("DV02","Tiện ích và thư giãn"),
                new NhomDichVu("DV03","Di chuyển và tham quan"),
                new NhomDichVu("DV04","Dịch vụ khác")
            };

            DS_DichVu = new ObservableCollection<DichVu>()
            {
                new DichVu("D01","Giặt ủi",50000,DS_NhomDV[0]),
                new DichVu("D02","Giường phụ",150000,DS_NhomDV[0]),
                new DichVu("D03","Thuê thêm chăn / gối",70000,DS_NhomDV[0]),
                new DichVu("D04","Giữ hành lý",30000,DS_NhomDV[0]),
                new DichVu("D05","Hồ bơi",100000,DS_NhomDV[1]),
                new DichVu("D06","Spa thư giãn",300000,DS_NhomDV[1]),
                new DichVu("D07","Xe đưa đón sân bay",250000,DS_NhomDV[2]),
                new DichVu("D08","Đặt tour tham quan thành phố",400000,DS_NhomDV[2]),
                new DichVu("D09","In tài liệu",20000,DS_NhomDV[3]),
            };
        }
    }
}
