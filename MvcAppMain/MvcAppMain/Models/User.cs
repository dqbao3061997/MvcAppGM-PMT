namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int ID_User { get; set; }

        [StringLength(255)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Roles { get; set; }
    }
}
