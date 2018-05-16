USE master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'QLPhongMachTu')
	DROP DATABASE QLPhongMachTu
GO

CREATE DATABASE QLPhongMachTu
GO
USE QLPhongMachTu
GO



CREATE TABLE ThamSo(
	ID_ThamSo int Identity NOT NULL,
	GiaTri INT,
	GhiChu VARCHAR(255),

	CONSTRAINT PK_ThamSo PRIMARY KEY(ID_ThamSo)
)
GO

CREATE TABLE DonVi(
	ID_DonVi int identity NOT NULL,
	TenDonVi VARCHAR(255),

	CONSTRAINT PK_DonVi PRIMARY KEY(ID_DonVi)
)
GO

CREATE TABLE Benh(
	ID_Benh int identity NOT NULL,
	TenBenh VARCHAR(255),

	CONSTRAINT PK_Benh PRIMARY KEY(ID_Benh)
)
GO

CREATE TABLE CachDung(
	ID_CachDung int identity NOT NULL,
	TenCachDung VARCHAR(255),
	SoLanDung INT,

	CONSTRAINT PK_CachDung PRIMARY KEY(ID_CachDung)
)
GO

CREATE TABLE Users(
	ID_User INT IDENTITY NOT NULL PRIMARY KEY,
	Username VARCHAR(255),
	Password VARCHAR(255),
	Roles VARCHAR(255)
    
)

CREATE TABLE HoSoBenhNhan(
	ID_BenhNhan int identity NOT NULL,
	HoTen VARCHAR(255),
	GioiTinh BIT,
	NamSinh DATE,
	DiaChi VARCHAR(255),
	

	CONSTRAINT PK_HoSoBenhNhan PRIMARY KEY(ID_BenhNhan)
)
GO



CREATE TABLE Thuoc(
	ID_Thuoc int identity NOT NULL,
	ID_DonVi int NOT NULL,
	TenThuoc VARCHAR(255),
	SoLuong INT,
	DonGia INT,

	CONSTRAINT PK_Thuoc PRIMARY KEY(ID_Thuoc),
	CONSTRAINT FK_DonVi FOREIGN KEY(ID_DonVi) REFERENCES dbo.DonVi(ID_DonVi)
)
GO

CREATE TABLE PhieuKhamBenh(
	ID_PhieuKham int identity NOT NULL,
	ID_BenhNhan int NOT NULL,
	ID_Benh int NOT NULL,
	NgayKham DATE,
	TrieuChung VARCHAR(255),
	

	CONSTRAINT PK_PhieuKhamBenh PRIMARY KEY(ID_PhieuKham),
	CONSTRAINT FK_BenhNhan FOREIGN KEY(ID_BenhNhan) REFERENCES dbo.HoSoBenhNhan(ID_BenhNhan),
	CONSTRAINT FK_Benh FOREIGN KEY(ID_Benh) REFERENCES dbo.Benh(ID_Benh)
)
GO


CREATE TABLE CT_PhieuKhamBenh(
	ID_PhieuKham int NOT NULL,
	ID_Thuoc int NOT NULL ,
	ID_CachDung int NOT NULL,
	SoLuongThuocLay INT,

	CONSTRAINT PK_CTPhieuKhamBenh PRIMARY KEY(ID_PhieuKham,ID_Thuoc),
	CONSTRAINT FK_PhieuKham FOREIGN KEY(ID_PhieuKham) REFERENCES dbo.PhieuKhamBenh(ID_PhieuKham),
	CONSTRAINT FK_Thuoc FOREIGN KEY(ID_Thuoc) REFERENCES dbo.Thuoc(ID_Thuoc),
	CONSTRAINT FK_CachDung FOREIGN KEY(ID_CachDung) REFERENCES dbo.CachDung(ID_CachDung)
)
GO

CREATE TABLE HoaDon(
	ID_HoaDon int identity NOT NULL,
	ID_PhieuKham int NOT NULL,
	TienKham INT,
	TienThuoc INT,
	DoanhThu INT,
	GhiChu VARCHAR(255),

	CONSTRAINT PK_HoaDon PRIMARY KEY(ID_HoaDon,ID_PhieuKham),
	CONSTRAINT FK_PhieuKham_HoaDon FOREIGN KEY(ID_PhieuKham) REFERENCES dbo.PhieuKhamBenh(ID_PhieuKham)
)
GO

/*Tạo dữ liệu tham số*/
INSERT INTO dbo.ThamSo
        ( GiaTri, GhiChu )
VALUES  ( 
          40, -- GiaTri - int
          'Maximun of Patient'  -- GhiChu - varchar(255)
          )
INSERT INTO dbo.ThamSo
        ( GiaTri, GhiChu )
VALUES  ( 
          30000, -- GiaTri - int
          'Fee to Examination'  -- GhiChu - varchar(255)
          )

/*Tạo dữ liệu đơn vị*/
INSERT INTO dbo.DonVi
        ( TenDonVi )
VALUES  ( 
          'Vien'  -- TenDonVi - varchar(255)
          )
INSERT INTO dbo.DonVi
        ( TenDonVi )
VALUES  ( 
          'Chai'  -- TenDonVi - varchar(255)
          )
/*Tạo dũ liệu bệnh*/
INSERT INTO dbo.Benh
        ( TenBenh )
VALUES  ( 
          'Stomach Upset'  -- TenBenh - varchar(255)
          )
INSERT INTO dbo.Benh
        ( TenBenh )
VALUES  ( 
          'Cold'  -- TenBenh - varchar(255)
          )
INSERT INTO dbo.Benh
        ( TenBenh )
VALUES  ( 
          'Have a running nose'  -- TenBenh - varchar(255)
          )
INSERT INTO dbo.Benh
        ( TenBenh )
VALUES  ( 
          'Allergic rhinitis'  -- TenBenh - varchar(255)
          )
INSERT INTO dbo.Benh
        ( TenBenh )
VALUES  ( 
          'Headache'  -- TenBenh - varchar(255)
          )
/*Tạo dũ liệu cách dùng*/
INSERT INTO dbo.CachDung
        ( TenCachDung, SoLanDung )
VALUES  ( 'Taking before breakfast',  -- TenCachDung - varchar(255)
			3
          )
INSERT INTO dbo.CachDung
        ( TenCachDung, SoLanDung )
VALUES  ( 'Taking after lunch',  -- TenCachDung - varchar(255)
			4
          )
INSERT INTO dbo.CachDung
        ( TenCachDung, SoLanDung )
VALUES  ( 'Taking before dinner',  -- TenCachDung - varchar(255)
			2
          )
INSERT INTO dbo.CachDung
        ( TenCachDung, SoLanDung )
VALUES  ( 'Taking before sleep',  -- TenCachDung - varchar(255)
			5
          )

/*Chen du lieu User*/
INSERT INTO dbo.Users
        ( Username, Password, Roles )
VALUES  ( 
          'admin', -- Username - varchar(255)
          '123456', -- Password - varchar(255)
          'admin'  -- Roles - varchar(255)
          )
INSERT INTO dbo.Users
        ( Username, Password, Roles )
VALUES  ( 
          'user', -- Username - varchar(255)
          '123456', -- Password - varchar(255)
          'user'  -- Roles - varchar(255)
          )