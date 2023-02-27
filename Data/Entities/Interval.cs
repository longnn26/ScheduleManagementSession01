using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Interval : BaseEntities
    {
        public DateTime From{ get; set; }
        public DateTime To { get; set; }
        public bool IsAvailable { get; set; }
        public Guid ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public virtual Schedule Schedule { get; set; }
    }
}
