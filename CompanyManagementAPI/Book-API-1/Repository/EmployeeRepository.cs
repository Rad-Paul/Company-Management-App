using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Contracts.Shared.RequestFeatures;
using CompanyManagementAPI.Extensions;
namespace CompanyManagementAPI.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {   
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Employee>> GetEmployeesAsync
            (Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            IEnumerable<Employee> employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm!)
                .Sort(employeeParameters.OrderBy!)
                .ToListAsync();

            //apparently this is faster with an extra call
            //int count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();
            //return new PagedList<Employee>(employees.ToList(), count, employeeParameters.PageNumber, employeeParameters.PageSize);

            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            Employee? employee = await FindByCondition(e => 
                e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges)
                .FirstOrDefaultAsync();

            return employee;
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
