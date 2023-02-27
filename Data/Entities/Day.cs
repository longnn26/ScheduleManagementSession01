using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Day : BaseEntities
    {
        public DateTime Date { get; set; }

        public bool Status { get; set; }
        public Guid WorkingCalendarId { get; set; }
        [ForeignKey("WorkingCalendarId")]
        public virtual WorkingCalendar WorkingCalendar { get; set; }
    }
}
