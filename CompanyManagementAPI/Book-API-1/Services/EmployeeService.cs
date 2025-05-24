using AutoMapper;
using CompanyManagementAPI.DTOs;
using CompanyManagementAPI.Services.Contracts;
using Contracts;
using Entities.Models;
using Entities.Exceptions;
using Contracts.Shared.RequestFeatures;

namespace CompanyManagementAPI.Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync
            (Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();

            await CheckIfCompanyExists(companyId, trackChanges);

            PagedList<Employee> employeesAndMetadata = 
                await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);

            IEnumerable<EmployeeDto> employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesAndMetadata);

            return (employees: employeesDto, metaData: employeesAndMetadata.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            Employee employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);

            EmployeeDto employeeDto = _mapper.Map<Employee, EmployeeDto>(employee!);
            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto newEmployee, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            Employee employeeEntity = _mapper.Map<Employee>(newEmployee);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            EmployeeDto employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            Employee employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);

            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeUpdate, 
            bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);

            Employee employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, empTrackChanges);

            _mapper.Map(employeeUpdate, employee);
            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);

            Employee employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, empTrackChanges);

            EmployeeForUpdateDto employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            Company? company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);
        }

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists
            (Guid companyId, Guid employeeId, bool trackChanges)
        {
            Employee? employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);

            if (employee is null)
                throw new EmployeeNotFoundException(employeeId);

            return employee;
        }
    }
}
