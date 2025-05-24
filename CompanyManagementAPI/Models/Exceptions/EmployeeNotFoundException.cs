using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid employeeId) : base($"Could not find employee with id: {employeeId}")
        {

        }
    }
}
