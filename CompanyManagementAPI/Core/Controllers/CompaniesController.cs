using CompanyManagementAPI.DTOs;
using CompanyManagementAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using CompanyManagementAPI.ModelBinders;
using CompanyManagementAPI.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using CompanyManagementAPI.Hubs;

namespace CompanyManagementAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHubContext<CompanyManagementHub> _hubContext;

        public CompaniesController(IServiceManager serviceManager, IHubContext<CompanyManagementHub> hubContext)
        {
            _serviceManager = serviceManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            IEnumerable<CompanyDto> companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);

            return Ok(companies);
        }

        [HttpGet("collection/{ids}", Name = "CompanyCollectionByIds")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            IEnumerable<CompanyDto> companies = await _serviceManager.CompanyService.GetCompaniesByIdsAsync(ids, trackChanges: false);

            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            CompanyDto company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);

            return Ok(company);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            CompanyDto createdCompany = await _serviceManager.CompanyService.CreateCompanyAsync(company);

            await _hubContext.Clients.All.SendAsync("NewCompanyCreated", createdCompany);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanies([FromBody] IEnumerable<CompanyForCreationDto> companies)
        {
            (IEnumerable<CompanyDto> createdCompanies, string ids) = await _serviceManager.CompanyService.CreateCompaniesAsync(companies);

            return CreatedAtRoute("CompanyCollectionByIds", new { ids = ids }, createdCompanies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _serviceManager.CompanyService.DeleteCompanyAsync(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

            return NoContent();
        }
    }

}
