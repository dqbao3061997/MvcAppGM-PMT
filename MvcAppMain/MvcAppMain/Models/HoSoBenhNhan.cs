namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSoBenhNhan")]
    public partial class HoSoBenhNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoSoBenhNhan()
        {
            BK_PhieuKhamBenh = new HashSet<BK_PhieuKhamBenh>();
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
        }

        [Key]
        public int ID_BenhNhan { get; set; }

        [StringLength(255)]
        public string HoTen { get; set; }

        public bool? GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NamSinh { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BK_PhieuKhamBenh> BK_PhieuKhamBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }
    }
}
