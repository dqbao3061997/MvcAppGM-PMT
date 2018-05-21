namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CachDung")]
    public partial class CachDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CachDung()
        {
            BK_CT_PhieuKhamBenh = new HashSet<BK_CT_PhieuKhamBenh>();
            CT_PhieuKhamBenh = new HashSet<CT_PhieuKhamBenh>();
        }

        [Key]
        public int ID_CachDung { get; set; }

        [StringLength(255)]
        public string TenCachDung { get; set; }

        public int? SoLanDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BK_CT_PhieuKhamBenh> BK_CT_PhieuKhamBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PhieuKhamBenh> CT_PhieuKhamBenh { get; set; }
    }
}
