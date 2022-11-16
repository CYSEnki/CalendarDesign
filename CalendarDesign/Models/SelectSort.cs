namespace CalendarDesign.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectSort")]
    public partial class SelectSort
    {
        [Key]
        public int UID { get; set; }

        [Column("SelectSort")]
        [StringLength(20)]
        public string SelectSort1 { get; set; }
    }
}
