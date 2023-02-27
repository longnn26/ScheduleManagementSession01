using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Gender { get; set; } = true;
    }
}
