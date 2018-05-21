namespace MvcAppMain.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLPMContext : DbContext
    {
        public QLPMContext()
            : base("name=QLPMContext")
        {
        }

        public virtual DbSet<Benh> Benhs { get; set; }
        public virtual DbSet<BK_CT_PhieuKhamBenh> BK_CT_PhieuKhamBenh { get; set; }
        public virtual DbSet<BK_HoaDon> BK_HoaDon { get; set; }
        public virtual DbSet<BK_PhieuKhamBenh> BK_PhieuKhamBenh { get; set; }
        public virtual DbSet<CachDung> CachDungs { get; set; }
        public virtual DbSet<CT_PhieuKhamBenh> CT_PhieuKhamBenh { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<HoSoBenhNhan> HoSoBenhNhans { get; set; }
        public virtual DbSet<PhieuKhamBenh> PhieuKhamBenhs { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }
        public virtual DbSet<Thuoc> Thuocs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benh>()
                .Property(e => e.TenBenh)
                .IsUnicode(false);

            modelBuilder.Entity<Benh>()
                .HasMany(e => e.PhieuKhamBenhs)
                .WithRequired(e => e.Benh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Benh>()
                .HasMany(e => e.BK_PhieuKhamBenh)
                .WithRequired(e => e.Benh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BK_HoaDon>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<BK_PhieuKhamBenh>()
                .Property(e => e.TrieuChung)
                .IsUnicode(false);

            modelBuilder.Entity<CachDung>()
                .Property(e => e.TenCachDung)
                .IsUnicode(false);

            modelBuilder.Entity<CachDung>()
                .HasMany(e => e.BK_CT_PhieuKhamBenh)
                .WithRequired(e => e.CachDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CachDung>()
                .HasMany(e => e.CT_PhieuKhamBenh)
                .WithRequired(e => e.CachDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.TenDonVi)
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .HasMany(e => e.Thuocs)
                .WithRequired(e => e.DonVi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoBenhNhan>()
                .Property(e => e.HoTen)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoBenhNhan>()
                .Property(e => e.DiaChi)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoBenhNhan>()
                .HasMany(e => e.BK_PhieuKhamBenh)
                .WithRequired(e => e.HoSoBenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoSoBenhNhan>()
                .HasMany(e => e.PhieuKhamBenhs)
                .WithRequired(e => e.HoSoBenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .Property(e => e.TrieuChung)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .HasMany(e => e.BK_CT_PhieuKhamBenh)
                .WithRequired(e => e.PhieuKhamBenh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .HasMany(e => e.BK_HoaDon)
                .WithRequired(e => e.PhieuKhamBenh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .HasMany(e => e.CT_PhieuKhamBenh)
                .WithRequired(e => e.PhieuKhamBenh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.PhieuKhamBenh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThamSo>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<Thuoc>()
                .Property(e => e.TenThuoc)
                .IsUnicode(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.BK_CT_PhieuKhamBenh)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.CT_PhieuKhamBenh)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Roles)
                .IsUnicode(false);
        }
    }
}
