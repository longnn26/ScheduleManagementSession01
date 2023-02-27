using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Token
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string UserId { get; set; }
        public int Expires_in { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
