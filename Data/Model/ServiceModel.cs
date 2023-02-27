using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class ServiceCreateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
    public class ServiceUpdateModel
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
    public class ServiceModel : ServiceUpdateModel
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
