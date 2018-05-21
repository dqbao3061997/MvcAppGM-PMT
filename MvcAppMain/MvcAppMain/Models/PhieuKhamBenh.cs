namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuKhamBenh")]
    public partial class PhieuKhamBenh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuKhamBenh()
        {
            BK_CT_PhieuKhamBenh = new HashSet<BK_CT_PhieuKhamBenh>();
            BK_HoaDon = new HashSet<BK_HoaDon>();
            CT_PhieuKhamBenh = new HashSet<CT_PhieuKhamBenh>();
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int ID_PhieuKham { get; set; }

        public int ID_BenhNhan { get; set; }

        public int ID_Benh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKham { get; set; }

        [StringLength(255)]
        public string TrieuChung { get; set; }

        public virtual Benh Benh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BK_CT_PhieuKhamBenh> BK_CT_PhieuKhamBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BK_HoaDon> BK_HoaDon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PhieuKhamBenh> CT_PhieuKhamBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual HoSoBenhNhan HoSoBenhNhan { get; set; }
    }
}
