using AutoMapper;
using CompanyManagementAPI.DTOs;
using CompanyManagementAPI.Services.Contracts;
using Contracts;
using Entities.Models;
using Entities.Exceptions;

namespace CompanyManagementAPI.Services
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
        {
            IEnumerable<Company> companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);

            IEnumerable<CompanyDto> companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
            
            CompanyDto companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            IEnumerable<Company> companies = await _repository.Company.GetCompaniesByIdsAsync(ids, trackChanges);

            if (ids.Count() != companies.Count())
                throw new CollectionByIdsBadRequestException();

            IEnumerable<CompanyDto> companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesToReturn;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company)
        {
            Company companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();

            CompanyDto companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDto>, string ids)> CreateCompaniesAsync(IEnumerable<CompanyForCreationDto> companies)
        {
            if (companies is null)
                throw new CompanyCollectionBadRequest();

            IEnumerable<Company> companyEntities = _mapper.Map<IEnumerable<Company>>(companies);

            foreach(Company company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }

            await _repository.SaveAsync();

            IEnumerable<CompanyDto> companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            string ids = string.Join(',', companiesToReturn.Select(c => c.Id));

            return (companiesToReturn, ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);

            _mapper.Map(companyForUpdate, company);
            await _repository.SaveAsync();
        }

        private async Task<Company> GetCompanyAndCheckIfItExists(Guid companyId, bool trackChanges)
        {
            Company? company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            return company;
        }
    }
}
