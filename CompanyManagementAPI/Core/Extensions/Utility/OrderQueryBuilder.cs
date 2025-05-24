using Entities.Models;
using System.Reflection;
using System.Text;

namespace CompanyManagementAPI.Extensions.Utility
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderByQuery<T>(string orderByString)
        {
            string[] orderByParams = orderByString.Trim().Split(',');
            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder orderQueryBuilder = new StringBuilder();

            foreach (string param in orderByParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                string propertyFromQueryName = param.Split(" ")[0];
                PropertyInfo objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase))!;

                if (objectProperty == null)
                    continue;

                string direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;

        }
    }
}
