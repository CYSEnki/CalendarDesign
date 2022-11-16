namespace CalendarDesign.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelectStatus
    {
        [Key]
        public int UID { get; set; }

        [Column("SelectStatus")]
        [StringLength(20)]
        public string SelectStatus1 { get; set; }
    }
}
