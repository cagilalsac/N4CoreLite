using Microsoft.EntityFrameworkCore;
using N4CoreLite.Models;

namespace N4CoreLite.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// pageOrderModel's OrderExpression value not ending with "Desc" is used for ascending order.
        /// Add "Desc" at the end of the pageOrderModel's OrderExpression value for descending order.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageOrderModel"></param>
        /// <returns>IOrderedQueryable</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, PageOrderModel pageOrderModel) where TEntity : class
        {
            return pageOrderModel.OrderExpression.EndsWith("Desc") ? query.OrderByDescending(p => EF.Property<object>(p, pageOrderModel.OrderExpression.Remove(pageOrderModel.OrderExpression.Length - 4)))
                : query.OrderBy(p => EF.Property<object>(p, pageOrderModel.OrderExpression));
        }

        public static IQueryable<TModel> Paginate<TModel>(this IQueryable<TModel> query, PageOrderModel pageOrderModel) where TModel : class
        {
            pageOrderModel.TotalRecordsCount = query.Count();
            int recordsPerPageCount;
            if (int.TryParse(pageOrderModel.RecordsPerPageCount, out recordsPerPageCount))
                query = query.Skip((pageOrderModel.PageNumber - 1) * recordsPerPageCount).Take(recordsPerPageCount);
            return query;
        }
    }
}
