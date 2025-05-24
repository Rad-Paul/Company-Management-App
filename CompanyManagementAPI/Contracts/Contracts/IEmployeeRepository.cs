using Entities.Models;
using Contracts.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        public Task<PagedList<Employee>> GetEmployeesAsync
            (Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        public Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
        public void CreateEmployeeForCompany(Guid companyId, Employee employee);
        public void DeleteEmployee(Employee employee);
    }
}
