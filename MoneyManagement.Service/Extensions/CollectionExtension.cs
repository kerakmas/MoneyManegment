using MoneyManagement.Domain.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Extensions
{
    public static class CollectionExtentions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParamas @params)
        {
            int numberOfItemsToSkip = (@params.PageIndex - 1) * @params.PageSize;
            int totalCount = source.Count();

            if (numberOfItemsToSkip >= totalCount && totalCount > 0)
            {
                numberOfItemsToSkip = totalCount - totalCount % @params.PageSize;
            }

            return source.Skip(numberOfItemsToSkip).Take(@params.PageSize);
        }
    }
}
