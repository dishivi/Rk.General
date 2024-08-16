using General.Models.Http;
using System.Linq.Expressions;
using System.Reflection;

namespace Rk.General.Utility.Common.Extensions
{
    public static class PageAndSort
    {
        public static IQueryable<T> PageWithSort<T>(this IQueryable<T> query, PaginationRequest listRequest)
        {
            if (!string.IsNullOrWhiteSpace(listRequest.SortBy))
            {
                List<string> sortData;

                if (listRequest.SortBy.Contains(','))
                {
                    sortData = listRequest.SortBy.Split(',').ToList();
                }
                else
                {
                    sortData = new List<string>();
                    sortData.Add(listRequest.SortBy);
                }

                IOrderedQueryable<T> sortedData = null;

                foreach (var sort in sortData)
                {
                    var isDescending = sort.Contains(" desc", StringComparison.OrdinalIgnoreCase);
                    var sortBy = sort.Contains(' ') ? sort.Split(' ')[0] : sort;
                    var sortByColumn = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);


                    sortedData = sortedData == null ? (isDescending ? query.OrderByDescending(ToLambda<T>(sortBy))
                                                                                                   : query.OrderBy(ToLambda<T>(sortBy)))
                                                        : (isDescending ? sortedData.ThenByDescending(ToLambda<T>(sortBy))
                                                                                            : sortedData.ThenBy(ToLambda<T>(sortBy)));
                }

                query = sortedData;
            }

            if (listRequest.PageSize != -1)
            {
                if (listRequest.PageNumber > 1)
                {
                    query = query.Skip((listRequest.PageNumber - 1) * listRequest.PageSize);
                }

                query = query.Take(listRequest.PageSize);
            }

            return query;
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}