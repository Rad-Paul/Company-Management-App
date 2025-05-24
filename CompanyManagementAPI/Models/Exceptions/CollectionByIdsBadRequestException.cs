using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() : base("Failed to fetch one or more companies from the database.")
        {

        }
    }
}
