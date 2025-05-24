using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Shared.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.
                Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
