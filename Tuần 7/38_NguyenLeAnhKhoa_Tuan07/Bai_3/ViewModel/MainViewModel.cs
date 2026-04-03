using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using _38_NguyenLeAnhKhoa_Tuan07.Bai_3.Model;
using _38_NguyenLeAnhKhoa_Tuan07.Helper;

namespace _38_NguyenLeAnhKhoa_Tuan07.Bai_3.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private KhachHang khachHangHienTai;
        public KhachHang KhachHangHienTai
        {
            get { return khachHangHienTai; }
            set
            {
                khachHangHienTai = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CauHoiKhaoSat> DanhSachCauHoiMau { get; set; }
        public ObservableCollection<PhanHoiKhachHang> DanhSachPhanHoi { get; set; }

        public ThongTinKhachHangViewModel ThongTinKhachHangVM { get; set; }
        public KhaoSatGopYViewModel KhaoSatGopYVM { get; set; }
        public DanhSachPhanHoiThongKeViewModel DanhSachPhanHoiThongKeVM { get; set; }

        public ICommand ShowThongTinKhachHangCommand { get; set; }
        public ICommand ShowKhaoSatGopYCommand { get; set; }
        public ICommand ShowDanhSachPhanHoiThongKeCommand { get; set; }

        public MainViewModel()
        {
            DanhSachCauHoiMau = new ObservableCollection<CauHoiKhaoSat>();
            DanhSachPhanHoi = new ObservableCollection<PhanHoiKhachHang>();

            KhoiTaoCauHoiMau();
            KhoiTaoDuLieuGia();

            ThongTinKhachHangVM = new ThongTinKhachHangViewModel(this);
            KhaoSatGopYVM = new KhaoSatGopYViewModel(this);
            DanhSachPhanHoiThongKeVM = new DanhSachPhanHoiThongKeViewModel(this);

            ShowThongTinKhachHangCommand = new RelayCommand((p) => CurrentViewModel = ThongTinKhachHangVM, (p) => true);
            ShowKhaoSatGopYCommand = new RelayCommand((p) =>
            {
                KhaoSatGopYVM.DongBoThongTinKhachHang();
                CurrentViewModel = KhaoSatGopYVM;
            }, (p) => true);
            ShowDanhSachPhanHoiThongKeCommand = new RelayCommand((p) =>
            {
                DanhSachPhanHoiThongKeVM.CapNhatThongKe();
                CurrentViewModel = DanhSachPhanHoiThongKeVM;
            }, (p) => true);

            CurrentViewModel = ThongTinKhachHangVM;
        }

        public void DongBoDuLieuKhachHang()
        {
            KhaoSatGopYVM.DongBoThongTinKhachHang();
            ThongTinKhachHangVM.DongBoTuDuLieuHienTai();
        }

        public void DongBoThongKe()
        {
            DanhSachPhanHoiThongKeVM.CapNhatThongKe();
        }

        private void KhoiTaoCauHoiMau()
        {
            var luaChonMacDinh = new ObservableCollection<string>
            {
                "Rất hài lòng",
                "Hài lòng",
                "Bình thường",
                "Không hài lòng"
            };

            DanhSachCauHoiMau.Add(new CauHoiKhaoSat(1, "Bạn đánh giá thế nào về chất lượng đồ uống?", new ObservableCollection<string>(luaChonMacDinh)));
            DanhSachCauHoiMau.Add(new CauHoiKhaoSat(2, "Bạn hài lòng mức độ nào về thái độ phục vụ?", new ObservableCollection<string>(luaChonMacDinh)));
            DanhSachCauHoiMau.Add(new CauHoiKhaoSat(3, "Bạn đánh giá thế nào về tốc độ phục vụ?", new ObservableCollection<string>(luaChonMacDinh)));
            DanhSachCauHoiMau.Add(new CauHoiKhaoSat(4, "Mức độ hài lòng của bạn về không gian quán?", new ObservableCollection<string>(luaChonMacDinh)));
        }

        private void KhoiTaoDuLieuGia()
        {
            KhachHangHienTai = new KhachHang("Nguyễn Văn A", "0909000111", "vana@gmail.com");

            DanhSachPhanHoi.Add(new PhanHoiKhachHang(
                "PH001",
                DateTime.Today.AddDays(-2),
                new KhachHang("Trần Thị B", "0911222333", "thib@gmail.com"),
                new ObservableCollection<ChiTietTraLoi>
                {
                    new ChiTietTraLoi(1, "Bạn đánh giá thế nào về chất lượng đồ uống?", "Hài lòng", 3),
                    new ChiTietTraLoi(2, "Bạn hài lòng mức độ nào về thái độ phục vụ?", "Rất hài lòng", 4),
                    new ChiTietTraLoi(3, "Bạn đánh giá thế nào về tốc độ phục vụ?", "Hài lòng", 3),
                    new ChiTietTraLoi(4, "Mức độ hài lòng của bạn về không gian quán?", "Bình thường", 2)
                },
                "Nên thêm nhiều món theo mùa.",
                false));

            DanhSachPhanHoi.Add(new PhanHoiKhachHang(
                "PH002",
                DateTime.Today.AddDays(-1),
                new KhachHang("Lê Minh C", "0988777666", "minhc@gmail.com"),
                new ObservableCollection<ChiTietTraLoi>
                {
                    new ChiTietTraLoi(1, "Bạn đánh giá thế nào về chất lượng đồ uống?", "Rất hài lòng", 4),
                    new ChiTietTraLoi(2, "Bạn hài lòng mức độ nào về thái độ phục vụ?", "Rất hài lòng", 4),
                    new ChiTietTraLoi(3, "Bạn đánh giá thế nào về tốc độ phục vụ?", "Hài lòng", 3),
                    new ChiTietTraLoi(4, "Mức độ hài lòng của bạn về không gian quán?", "Hài lòng", 3)
                },
                "Không gian sạch và thoáng.",
                true));
        }
    }
}
