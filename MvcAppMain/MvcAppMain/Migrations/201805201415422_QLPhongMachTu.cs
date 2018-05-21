namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLPhongMachTu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Benh",
                c => new
                    {
                        ID_Benh = c.Int(nullable: false, identity: true),
                        TenBenh = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_Benh);
            
            CreateTable(
                "dbo.BK_PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false),
                        ID_BenhNhan = c.Int(nullable: false),
                        ID_Benh = c.Int(nullable: false),
                        NgayKham = c.DateTime(storeType: "date"),
                        TrieuChung = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_PhieuKham)
                .ForeignKey("dbo.HoSoBenhNhan", t => t.ID_BenhNhan)
                .ForeignKey("dbo.Benh", t => t.ID_Benh)
                .Index(t => t.ID_BenhNhan)
                .Index(t => t.ID_Benh);
            
            CreateTable(
                "dbo.HoSoBenhNhan",
                c => new
                    {
                        ID_BenhNhan = c.Int(nullable: false, identity: true),
                        HoTen = c.String(maxLength: 255, unicode: false),
                        GioiTinh = c.Boolean(),
                        NamSinh = c.DateTime(storeType: "date"),
                        DiaChi = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_BenhNhan);
            
            CreateTable(
                "dbo.PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false, identity: true),
                        ID_BenhNhan = c.Int(nullable: false),
                        ID_Benh = c.Int(nullable: false),
                        NgayKham = c.DateTime(storeType: "date"),
                        TrieuChung = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_PhieuKham)
                .ForeignKey("dbo.HoSoBenhNhan", t => t.ID_BenhNhan)
                .ForeignKey("dbo.Benh", t => t.ID_Benh)
                .Index(t => t.ID_BenhNhan)
                .Index(t => t.ID_Benh);
            
            CreateTable(
                "dbo.BK_CT_PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false),
                        ID_Thuoc = c.Int(nullable: false),
                        ID_CachDung = c.Int(nullable: false),
                        SoLuongThuocLay = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID_PhieuKham, t.ID_Thuoc })
                .ForeignKey("dbo.CachDung", t => t.ID_CachDung)
                .ForeignKey("dbo.Thuoc", t => t.ID_Thuoc)
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham)
                .Index(t => t.ID_PhieuKham)
                .Index(t => t.ID_Thuoc)
                .Index(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.CachDung",
                c => new
                    {
                        ID_CachDung = c.Int(nullable: false, identity: true),
                        TenCachDung = c.String(maxLength: 255, unicode: false),
                        SoLanDung = c.Int(),
                    })
                .PrimaryKey(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.CT_PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false),
                        ID_Thuoc = c.Int(nullable: false),
                        ID_CachDung = c.Int(nullable: false),
                        SoLuongThuocLay = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID_PhieuKham, t.ID_Thuoc })
                .ForeignKey("dbo.Thuoc", t => t.ID_Thuoc)
                .ForeignKey("dbo.CachDung", t => t.ID_CachDung)
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham)
                .Index(t => t.ID_PhieuKham)
                .Index(t => t.ID_Thuoc)
                .Index(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.Thuoc",
                c => new
                    {
                        ID_Thuoc = c.Int(nullable: false, identity: true),
                        ID_DonVi = c.Int(nullable: false),
                        TenThuoc = c.String(maxLength: 255, unicode: false),
                        SoLuong = c.Int(),
                        DonGia = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Thuoc)
                .ForeignKey("dbo.DonVi", t => t.ID_DonVi)
                .Index(t => t.ID_DonVi);
            
            CreateTable(
                "dbo.DonVi",
                c => new
                    {
                        ID_DonVi = c.Int(nullable: false, identity: true),
                        TenDonVi = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_DonVi);
            
            CreateTable(
                "dbo.BK_HoaDon",
                c => new
                    {
                        ID_HoaDon = c.Int(nullable: false),
                        ID_PhieuKham = c.Int(nullable: false),
                        TienKham = c.Int(),
                        TienThuoc = c.Int(),
                        DoanhThu = c.Int(),
                        GhiChu = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => new { t.ID_HoaDon, t.ID_PhieuKham })
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham)
                .Index(t => t.ID_PhieuKham);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        ID_HoaDon = c.Int(nullable: false),
                        ID_PhieuKham = c.Int(nullable: false),
                        TienKham = c.Int(),
                        TienThuoc = c.Int(),
                        DoanhThu = c.Int(),
                        GhiChu = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => new { t.ID_HoaDon, t.ID_PhieuKham })
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham)
                .Index(t => t.ID_PhieuKham);
            
            CreateTable(
                "dbo.ThamSo",
                c => new
                    {
                        ID_ThamSo = c.Int(nullable: false, identity: true),
                        GiaTri = c.Int(),
                        GhiChu = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_ThamSo);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID_User = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 255, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                        Roles = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_User);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhieuKhamBenh", "ID_Benh", "dbo.Benh");
            DropForeignKey("dbo.BK_PhieuKhamBenh", "ID_Benh", "dbo.Benh");
            DropForeignKey("dbo.PhieuKhamBenh", "ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropForeignKey("dbo.HoaDon", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.BK_HoaDon", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung");
            DropForeignKey("dbo.Thuoc", "ID_DonVi", "dbo.DonVi");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_Thuoc", "dbo.Thuoc");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_Thuoc", "dbo.Thuoc");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung");
            DropForeignKey("dbo.BK_PhieuKhamBenh", "ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropIndex("dbo.HoaDon", new[] { "ID_PhieuKham" });
            DropIndex("dbo.BK_HoaDon", new[] { "ID_PhieuKham" });
            DropIndex("dbo.Thuoc", new[] { "ID_DonVi" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_CachDung" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_Thuoc" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_PhieuKham" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_CachDung" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_Thuoc" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_PhieuKham" });
            DropIndex("dbo.PhieuKhamBenh", new[] { "ID_Benh" });
            DropIndex("dbo.PhieuKhamBenh", new[] { "ID_BenhNhan" });
            DropIndex("dbo.BK_PhieuKhamBenh", new[] { "ID_Benh" });
            DropIndex("dbo.BK_PhieuKhamBenh", new[] { "ID_BenhNhan" });
            DropTable("dbo.Users");
            DropTable("dbo.ThamSo");
            DropTable("dbo.HoaDon");
            DropTable("dbo.BK_HoaDon");
            DropTable("dbo.DonVi");
            DropTable("dbo.Thuoc");
            DropTable("dbo.CT_PhieuKhamBenh");
            DropTable("dbo.CachDung");
            DropTable("dbo.BK_CT_PhieuKhamBenh");
            DropTable("dbo.PhieuKhamBenh");
            DropTable("dbo.HoSoBenhNhan");
            DropTable("dbo.BK_PhieuKhamBenh");
            DropTable("dbo.Benh");
        }
    }
}
