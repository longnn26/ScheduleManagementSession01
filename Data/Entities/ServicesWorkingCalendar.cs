using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ServicesWorkingCalendar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid WorkingCalendarId { get; set; }
        [ForeignKey("WorkingCalendarId")]
        public virtual WorkingCalendar WorkingCalendar { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Services Services { get; set; }
    }
}
