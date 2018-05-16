USE [QLPhongMachTu]
GO
/****** Object:  Trigger [dbo].[TG_UpdateSoLuongThuoc]    Script Date: 5/14/2018 12:29:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[TG_UpdateSoLuongThuoc]
ON [dbo].[CT_PhieuKhamBenh]
FOR INSERT,UPDATE
AS
BEGIN
	DECLARE @iD_PhieuKham INT
	DECLARE @iD_Thuoc INT
	DECLARE @soLuongThuocLay INT
	DECLARE @soLuongTon INT
	DECLARE @TienThuoc INT 
	DECLARE @DonGia INT

	SELECT @iD_PhieuKham = Inserted.ID_PhieuKham, @iD_Thuoc = Inserted.ID_Thuoc, @soLuongThuocLay = Inserted.SoLuongThuocLay FROM Inserted
	SELECT @soLuongTon = SoLuong,@DonGia = DonGia FROM dbo.Thuoc WHERE ID_Thuoc = @iD_Thuoc
	
	

	IF(ISNULL(@soLuongTon,0) > ISNULL(@soLuongThuocLay,0) )
		BEGIN 
			SET @TienThuoc = ISNULL(@soLuongThuocLay,0)*@DonGia
		
			INSERT INTO dbo.HoaDon
			        ( ID_PhieuKham ,
			          TienKham ,
			          TienThuoc ,
			          DoanhThu ,
			          GhiChu
			        )
			VALUES  ( @iD_PhieuKham , -- ID_PhieuKham - int
			          30000 , -- TienKham - int
			          @TienThuoc , -- TienThuoc - int
			          0 , -- DoanhThu - int
			          'Nothing'  -- GhiChu - varchar(255)
			        )
		END
	ELSE
		BEGIN 
			SET @TienThuoc = ISNULL(@soLuongTon,0)*@DonGia
			INSERT INTO dbo.HoaDon
			        ( ID_PhieuKham ,
			          TienKham ,
			          TienThuoc ,
			          DoanhThu ,
			          GhiChu
			        )
			VALUES  ( @iD_PhieuKham , -- ID_PhieuKham - int
			          30000 , -- TienKham - int
			          @TienThuoc , -- TienThuoc - int
			          0 , -- DoanhThu - int
			          'Nothing'  -- GhiChu - varchar(255)
			        )
		END
        
        
	
	--SET @soLuongTon = @SoLuongTon - @soLuongThuocLay	
	
	--IF(@soLuongTon < 0)
	--	BEGIN
	--		UPDATE dbo.Thuoc SET SoLuong = 0 WHERE ID_Thuoc = @iD_Thuoc
	--	END
	--ELSE
	--	BEGIN
	--		UPDATE dbo.Thuoc SET SoLuong = @soLuongTon WHERE ID_Thuoc = @iD_Thuoc
			
	--	END
    
	SET @soLuongTon = @soLuongTon - @soLuongThuocLay
	UPDATE dbo.Thuoc SET SoLuong = @soLuongTon WHERE ID_Thuoc = @id_Thuoc
	
	IF(@soLuongTon < 0)
		BEGIN
			SET @soLuongTon = 0
			SET @soLuongThuocLay = 0
			UPDATE dbo.Thuoc SET SoLuong = @soLuongTon WHERE ID_Thuoc = @id_Thuoc
			UPDATE dbo.CT_PhieuKhamBenh SET SoLuongThuocLay = @soLuongThuocLay WHERE ID_PhieuKham = @id_PhieuKham
		END

END