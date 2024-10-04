using System.Linq.Expressions;

namespace financialManagerAPI.Utilities
{
    public static class FilterPredicateBuilder
    {
        public static Expression<Func<T, bool>> BuildFilterPredicate<T>(Dictionary<string, string?> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            Expression predicateBody = Expression.Constant(true);

            foreach (var filter in filters)
            {
                var property = Expression.Property(parameter, filter.Key);
                var value = Expression.Constant(filter.Value);

                var equality = Expression.Equal(property, value);


                predicateBody = Expression.AndAlso(predicateBody, equality);
            }

            return Expression.Lambda<Func<T, bool>>(predicateBody, parameter);
        }
    }
}
