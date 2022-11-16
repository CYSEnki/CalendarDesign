namespace CalendarDesign.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("CalendarDT")]
    public partial class CalendarDT
    {
        [Key]
        public int UID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(20)]
        public string Sort { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime? StartTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime? EndTime { get; set; }

        public string Article { get; set; }
    }

    public partial class CalendarModel
    {
        public string MonthTitle { get; set; }

        public int StartDayOfWeak { get; set; }

        public int EndDay { get; set; }

        public List<CalendarDT> CalendarContent { get; set; }

        public CalendarDT AddSchedule { get; set; }

        public DateTime SetDate { get; set; }

    }
}
