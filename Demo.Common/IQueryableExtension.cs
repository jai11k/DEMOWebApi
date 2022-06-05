using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> GetPaged<TEntity>(this IQueryable<TEntity> query, int page = 1,
            int pageSize = 5)
        {
            if (pageSize > -1)
            {
                query = query.Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }

            return query;
        }
    }
}
