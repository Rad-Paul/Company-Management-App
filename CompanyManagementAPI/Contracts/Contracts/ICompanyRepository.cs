using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        public Task<IEnumerable<Company>> GetCompaniesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        public void DeleteCompany(Company company);
    }
}
