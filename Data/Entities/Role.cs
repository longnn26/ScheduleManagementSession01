using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
/*        [Column(TypeName = "varchar(350")]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }*/
/*        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }*/
    }
}
