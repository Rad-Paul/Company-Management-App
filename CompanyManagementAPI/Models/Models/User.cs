using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
