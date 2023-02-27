using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Schedule : BaseEntities
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid DayId { get; set; }
        [ForeignKey("DayId")]
        public virtual Day Day { get; set; }
    }
}
