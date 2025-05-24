
using CompanyManagementAPI.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace CompanyManagementAPI.Hubs
{
    public class CompanyManagementHub : Hub
    {
        public async Task CreateNewCompany(CompanyDto newCompany)
        {
            await Clients.All.SendAsync("NewCompanyCreated", newCompany);
        }
    }
}
