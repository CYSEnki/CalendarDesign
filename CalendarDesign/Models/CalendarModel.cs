using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CalendarDesign.Models
{
    public partial class CalendarModel : DbContext
    {
        public CalendarModel()
            : base("name=CalendarModel")
        {
        }

        public virtual DbSet<CalendarDT> CalendarDT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
