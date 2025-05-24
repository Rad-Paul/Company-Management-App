using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : BadRequestException
    {
        public UserNotFoundException() : base("Authentication failed. Wrong username or password")
        {

        }
    }
}
