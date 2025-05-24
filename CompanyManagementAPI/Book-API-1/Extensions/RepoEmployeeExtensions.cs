using Entities.Models;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;
using CompanyManagementAPI.Extensions.Utility;

namespace CompanyManagementAPI.Extensions
{
    public static class RepoEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
        {
            return employees.Where(e => e.Age <= maxAge && e.Age >= minAge);
        }

        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;

            string lowerCaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByString)
        {
            if (string.IsNullOrWhiteSpace(orderByString))
                return employees.OrderBy(e => e.Name);

            string orderQuery = OrderQueryBuilder.CreateOrderByQuery<Employee>(orderByString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Name);

            return employees.OrderBy(orderQuery);
        }
    }
}
