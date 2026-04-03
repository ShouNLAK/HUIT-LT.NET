using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using _38_NguyenLeAnhKhoa_Tuan05.Model;
using Newtonsoft.Json;

namespace _38_NguyenLeAnhKhoa_Tuan05.ViewModel
{
    internal class TodoViewModel_BTVN : INotifyPropertyChanged
    {
        public ObservableCollection<Todo_BTVN> DanhSachCongViec { get; private set; }

        public ObservableCollection<string> DanhSachMucDoUuTien { get; private set; }

        private ICollectionView danhSachCongViecView;
        public ICollectionView DanhSachCongViecView
        {
            get { return danhSachCongViecView; }
            set
            {
                danhSachCongViecView = value;
                OnPropertyChanged(nameof(DanhSachCongViecView));
            }
        }

        private Todo_BTVN congViecDuocChon;
        public Todo_BTVN CongViecDuocChon
        {
            get { return congViecDuocChon; }
            set
            {
                congViecDuocChon = value;
                if (congViecDuocChon != null)
                {
                    TenCongViecMoi = congViecDuocChon.TenCongViec;
                    MucDoUuTienMoi = congViecDuocChon.MucDoUuTien;
                    DaHoanThanhMoi = congViecDuocChon.DaHoanThanh;
                    GhiChuMoi = congViecDuocChon.GhiChu;
                }
                OnPropertyChanged(nameof(CongViecDuocChon));
            }
        }

        private string tenCongViecMoi;
        public string TenCongViecMoi
        {
            get { return tenCongViecMoi; }
            set
            {
                tenCongViecMoi = value;
                OnPropertyChanged(nameof(TenCongViecMoi));
            }
        }

        private string mucDoUuTienMoi;
        public string MucDoUuTienMoi
        {
            get { return mucDoUuTienMoi; }
            set
            {
                mucDoUuTienMoi = value;
                OnPropertyChanged(nameof(MucDoUuTienMoi));
            }
        }

        private bool daHoanThanhMoi;
        public bool DaHoanThanhMoi
        {
            get { return daHoanThanhMoi; }
            set
            {
                daHoanThanhMoi = value;
                OnPropertyChanged(nameof(DaHoanThanhMoi));
            }
        }

        private string ghiChuMoi;
        public string GhiChuMoi
        {
            get { return ghiChuMoi; }
            set
            {
                ghiChuMoi = value;
                OnPropertyChanged(nameof(GhiChuMoi));
            }
        }

        private string tuKhoaTimKiem;
        public string TuKhoaTimKiem
        {
            get { return tuKhoaTimKiem; }
            set
            {
                tuKhoaTimKiem = value;
                OnPropertyChanged(nameof(TuKhoaTimKiem));
                ApDungLoc();
            }
        }

        private int tongCongViec;
        public int TongCongViec
        {
            get { return tongCongViec; }
            private set
            {
                tongCongViec = value;
                OnPropertyChanged(nameof(TongCongViec));
            }
        }

        public TodoViewModel_BTVN()
        {
            DanhSachMucDoUuTien = new ObservableCollection<string>
            {
                "Cao",
                "Trung bình",
                "Thấp"
            };

            DanhSachCongViec = new ObservableCollection<Todo_BTVN>
            {
                new Todo_BTVN("Làm bài WPF", "Cao", false, "Bài tập tuần này"),
                new Todo_BTVN("Đọc tài liệu MVVM", "Trung bình", true, string.Empty)
            };

            DanhSachCongViecView = CollectionViewSource.GetDefaultView(DanhSachCongViec);
            MucDoUuTienMoi = DanhSachMucDoUuTien.FirstOrDefault();
            CapNhatThongKe();
        }

        public bool ThemCongViec()
        {
            if (string.IsNullOrWhiteSpace(TenCongViecMoi))
                return false;

            var congViecMoi = new Todo_BTVN(
                TenCongViecMoi.Trim(),
                string.IsNullOrWhiteSpace(MucDoUuTienMoi) ? DanhSachMucDoUuTien.FirstOrDefault() : MucDoUuTienMoi,
                DaHoanThanhMoi,
                string.IsNullOrWhiteSpace(GhiChuMoi) ? string.Empty : GhiChuMoi.Trim());

            DanhSachCongViec.Add(congViecMoi);
            CongViecDuocChon = congViecMoi;
            CapNhatThongKe();
            return true;
        }

        public void XoaCongViecDuocChon()
        {
            if (CongViecDuocChon == null)
                return;

            DanhSachCongViec.Remove(CongViecDuocChon);
            CongViecDuocChon = DanhSachCongViec.FirstOrDefault();
            CapNhatThongKe();
        }

        public void LamMoiForm()
        {
            TenCongViecMoi = string.Empty;
            MucDoUuTienMoi = DanhSachMucDoUuTien.FirstOrDefault();
            DaHoanThanhMoi = false;
            GhiChuMoi = string.Empty;
            CongViecDuocChon = null;
        }

        public void ApDungLoc()
        {
            if (DanhSachCongViecView == null)
                return;

            DanhSachCongViecView.Filter = obj =>
            {
                if (string.IsNullOrWhiteSpace(TuKhoaTimKiem))
                    return true;

                var congViec = obj as Todo_BTVN;
                return congViec != null && congViec.TenCongViec.IndexOf(TuKhoaTimKiem, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DanhSachCongViecView.Refresh();
        }

        public void LuuJson(string duongDanFile)
        {
            var noiDungJson = JsonConvert.SerializeObject(DanhSachCongViec, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(duongDanFile, noiDungJson);
        }

        public void TaiJson(string duongDanFile)
        {
            if (!File.Exists(duongDanFile))
                return;

            var noiDungJson = File.ReadAllText(duongDanFile);
            var duLieu = JsonConvert.DeserializeObject<ObservableCollection<Todo_BTVN>>(noiDungJson);
            if (duLieu == null)
                return;

            DanhSachCongViec.Clear();
            foreach (var item in duLieu)
                DanhSachCongViec.Add(item);

            CapNhatThongKe();
            ApDungLoc();
            LamMoiForm();
        }

        private void CapNhatThongKe()
        {
            TongCongViec = DanhSachCongViec.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string tenThuocTinh)
        {
            var xuLy = PropertyChanged;
            if (xuLy != null)
                xuLy(this, new PropertyChangedEventArgs(tenThuocTinh));
        }
    }
}
