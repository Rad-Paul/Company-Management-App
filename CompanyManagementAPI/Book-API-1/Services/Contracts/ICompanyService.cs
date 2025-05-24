using CompanyManagementAPI.DTOs;

namespace CompanyManagementAPI.Services.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetCompaniesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto>, string ids)> CreateCompaniesAsync(IEnumerable<CompanyForCreationDto> companies);
        Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
        Task DeleteCompanyAsync(Guid companyId, bool trackChanges);

    }
}
