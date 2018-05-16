SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER TG_UpdateHoaDonTienThuocDoanhThu
ON dbo.HoaDon
FOR INSERT,UPDATE
AS
BEGIN
	DECLARE @iD_PhieuKham INT
	DECLARE @iD_Thuoc INT
	DECLARE @donGia INT
	DECLARE @soLuongThuocLay INT 
	DECLARE @tienThuoc INT
	DECLARE @doanhThu INT
	DECLARE @tienKham INT
	DECLARE @ghiChu VARCHAR(255)
	

	SELECT @iD_PhieuKham = Inserted.ID_PhieuKham,@tienKham = Inserted.TienKham, @tienThuoc = Inserted.TienThuoc,@doanhThu = Inserted.DoanhThu,@ghiChu = Inserted.GhiChu FROM Inserted
	SELECT @iD_Thuoc = ID_Thuoc, @soLuongThuocLay = SoLuongThuocLay FROM dbo.CT_PhieuKhamBenh WHERE ID_PhieuKham = @iD_PhieuKham
	SELECT @donGia = DonGia FROM dbo.Thuoc WHERE ID_Thuoc = @iD_Thuoc

	SET @tienThuoc = @soLuongThuocLay*@donGia
	SET @doanhThu = @tienKham+@tienThuoc

	UPDATE dbo.HoaDon SET TienThuoc = @tienThuoc, DoanhThu = @doanhThu WHERE ID_PhieuKham = @iD_PhieuKham
	IF(@tienThuoc = 0)
		BEGIN
			SET @ghiChu = 'Do not have enough drugs'
			UPDATE dbo.HoaDon SET GhiChu = @ghiChu WHERE ID_PhieuKham = @iD_PhieuKham
		END
    

END
