using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Services : BaseEntities
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        
        
    }
}
