using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class WorkingCalendar : BaseEntities
    {
        public int BookingBeforeDate { get; set; }
        public int BookingAfterDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ShiftCount { get; set; }
    }
}
