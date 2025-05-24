using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementAPI.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            IEnumerable<Company> companies = await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

            return companies;
        }

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            Company? company = await FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();

            return company;
        }

        public async Task<IEnumerable<Company>> GetCompaniesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            IEnumerable<Company> companies = await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();

            return companies;
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
    }

}
