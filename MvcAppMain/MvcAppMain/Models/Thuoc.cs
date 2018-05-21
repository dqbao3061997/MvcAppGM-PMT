namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thuoc")]
    public partial class Thuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thuoc()
        {
            BK_CT_PhieuKhamBenh = new HashSet<BK_CT_PhieuKhamBenh>();
            CT_PhieuKhamBenh = new HashSet<CT_PhieuKhamBenh>();
        }

        [Key]
        public int ID_Thuoc { get; set; }

        public int ID_DonVi { get; set; }

        [StringLength(255)]
        public string TenThuoc { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BK_CT_PhieuKhamBenh> BK_CT_PhieuKhamBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PhieuKhamBenh> CT_PhieuKhamBenh { get; set; }

        public virtual DonVi DonVi { get; set; }
    }
}
