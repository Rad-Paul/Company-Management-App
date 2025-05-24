using CompanyManagementAPI.DTOs;
using Contracts.Shared.RequestFeatures;
using Entities.Models;

namespace CompanyManagementAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        public Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync
            (Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        public Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
        public Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto newEmployee, bool trackChanges);
        public Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges);
        public Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeUpdate, 
            bool compTrackChanges, bool empTrackChanges);
        public Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid employeeId,
            bool compTrackChanges, bool empTrackChanges);

        public Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
    }
}
