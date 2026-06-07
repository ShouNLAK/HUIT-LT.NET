CREATE DATABASE QL_Karaoke
USE QL_Karaoke

CREATE TABLE LOAIPHONG (
    MaNhom VARCHAR(10) PRIMARY KEY,
    TenNhom NVARCHAR(50)
);

CREATE TABLE PHONG (
    MaPhong VARCHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(50),
    SucChua INT,
    GiaPhong MONEY,
    KieuPhong NVARCHAR(50),
    MaNhom VARCHAR(10),
    FOREIGN KEY (MaNhom) REFERENCES LOAIPHONG(MaNhom)
);

CREATE TABLE KHACHHANG (
    MaKhachHang VARCHAR(10) PRIMARY KEY,
    TenKH NVARCHAR(50),
    SoDT VARCHAR(20)
);

CREATE TABLE PHUTHU (
    MaPhuThu VARCHAR(10) PRIMARY KEY,
    TenPhuThu NVARCHAR(50),
    GiaPT MONEY
);

CREATE TABLE DATPHONG (
    MaDatPhong VARCHAR(10) PRIMARY KEY,
    MaPh VARCHAR(10),
    MaKH VARCHAR(10),
    NgayDat DATE,
    NgayTra DATE,
    FOREIGN KEY (MaPh) REFERENCES PHONG(MaPhong),
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKhachHang)
);

CREATE TABLE CHITIETDATPHONG (
    MaCT VARCHAR(10) PRIMARY KEY,
    MaDP VARCHAR(10),
    MaPT VARCHAR(10),
    SL INT,
    FOREIGN KEY (MaDP) REFERENCES DATPHONG(MaDatPhong),
    FOREIGN KEY (MaPT) REFERENCES PHUTHU(MaPhuThu)
);

INSERT INTO LOAIPHONG (MaNhom, TenNhom) VALUES 
('NH01', N'Phòng Tiêu Chuẩn'),
('NH02', N'Phòng Superior'),
('NH03', N'Phòng Deluxe'),
('NH04', N'Phòng Suite (VIP)'),
('NH05', N'Phòng Gia Đình');

INSERT INTO PHONG (MaPhong, TenPhong, SucChua, GiaPhong, KieuPhong, MaNhom) VALUES 
('PH101', N'Phòng 101', 2, 500000, N'Giường Đôi', 'NH01'),
('PH102', N'Phòng 102', 2, 500000, N'Giường Đôi', 'NH01'),
('PH103', N'Phòng 103', 2, 500000, N'2 Giường Đơn', 'NH01'),
('PH201', N'Phòng 201', 2, 800000, N'Giường King Size', 'NH02'),
('PH202', N'Phòng 202', 2, 800000, N'Giường King Size', 'NH02'),
('PH203', N'Phòng 203', 2, 800000, N'2 Giường Đơn', 'NH02'),
('PH301', N'Phòng 301', 3, 1200000, N'Giường King + Đơn', 'NH03'),
('PH302', N'Phòng 302', 3, 1200000, N'Giường King + Đơn', 'NH03'),
('PH401', N'Phòng VIP 401', 4, 2500000, N'2 Giường King Size', 'NH04'),
('PH402', N'Phòng VIP 402', 4, 2500000, N'2 Giường King Size', 'NH04'),
('PH501', N'Phòng Family 501', 6, 1800000, N'3 Giường Đôi', 'NH05'),
('PH502', N'Phòng Family 502', 6, 1800000, N'3 Giường Đôi', 'NH05');

INSERT INTO KHACHHANG (MaKhachHang, TenKH, SoDT) VALUES 
('KH001', N'Nguyễn Văn A', '0901234567'),
('KH002', N'Trần Thị B', '0912345678'),
('KH003', N'Lê Văn C', '0987654321'),
('KH004', N'Phạm Thị D', '0909876543'),
('KH005', N'Hoàng Văn E', '0918273645'),
('KH006', N'Vũ Thị F', '0933445566'),
('KH007', N'Đặng Văn G', '0977889900'),
('KH008', N'Bùi Thị H', '0966554433'),
('KH009', N'Đỗ Văn I', '0922334455'),
('KH010', N'Ngô Thị K', '0944556677'),
('KH011', N'Dương Văn L', '0999888777'),
('KH012', N'Lý Thị M', '0988776655');

INSERT INTO PHUTHU (MaPhuThu, TenPhuThu, GiaPT) VALUES 
('PT01', N'Giặt ủi', 50000),
('PT02', N'Nước giải khát', 20000),
('PT03', N'Thuê xe máy', 150000),
('PT04', N'Ăn sáng buffet', 150000),
('PT05', N'Spa & Massage', 500000),
('PT06', N'Đưa đón sân bay', 300000),
('PT07', N'Giường phụ', 250000),
('PT08', N'Tiêu thụ Minibar', 200000),
('PT09', N'Thuê xe đạp', 50000),
('PT10', N'Dịch vụ gọi thức ăn tại phòng', 100000);

INSERT INTO DATPHONG (MaDatPhong, MaPh, MaKH, NgayDat, NgayTra) VALUES 
('DP001', 'PH101', 'KH001', '2024-01-10', '2024-01-12'),
('DP002', 'PH201', 'KH002', '2024-01-11', '2024-01-15'),
('DP003', 'PH301', 'KH003', '2024-01-15', '2024-01-18'),
('DP004', 'PH401', 'KH004', '2024-02-01', '2024-02-03'),
('DP005', 'PH501', 'KH005', '2024-02-05', '2024-02-10'),
('DP006', 'PH102', 'KH006', '2024-02-14', '2024-02-16'),
('DP007', 'PH202', 'KH007', '2024-03-01', '2024-03-05'),
('DP008', 'PH302', 'KH008', '2024-03-08', '2024-03-10'),
('DP009', 'PH402', 'KH009', '2024-04-20', '2024-04-25'),
('DP010', 'PH502', 'KH010', '2024-04-28', '2024-05-02'),
('DP011', 'PH103', 'KH011', '2024-05-15', '2024-05-18'),
('DP012', 'PH203', 'KH012', '2024-06-01', '2024-06-05');

INSERT INTO CHITIETDATPHONG (MaCT, MaDP, MaPT, SL) VALUES 
('CT001', 'DP001', 'PT01', 2),
('CT002', 'DP001', 'PT02', 5),
('CT003', 'DP002', 'PT04', 4),
('CT004', 'DP002', 'PT06', 1),
('CT005', 'DP003', 'PT03', 2),
('CT006', 'DP004', 'PT05', 2),
('CT007', 'DP004', 'PT08', 1),
('CT008', 'DP005', 'PT07', 1),
('CT009', 'DP005', 'PT04', 6),
('CT010', 'DP006', 'PT10', 2),
('CT011', 'DP007', 'PT01', 3),
('CT012', 'DP008', 'PT05', 1),
('CT013', 'DP009', 'PT06', 2),
('CT014', 'DP009', 'PT08', 3),
('CT015', 'DP010', 'PT04', 10),
('CT016', 'DP010', 'PT09', 4),
('CT017', 'DP011', 'PT02', 8),
('CT018', 'DP012', 'PT03', 4),
('CT019', 'DP012', 'PT01', 5),
('CT020', 'DP012', 'PT10', 3);