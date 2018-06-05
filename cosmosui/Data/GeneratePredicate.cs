using System;
using System.Linq.Expressions;

namespace cosmosui.Data
{
    public class GeneratePredicate
    {
        public static Expression<Func<T, bool>> GenerateEqualFieldExpression<T>(string fieldName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            // m
            var fieldAccess = Expression.PropertyOrField(parameter, fieldName);
            // m.[fieldName]
            var nullValue = Expression.Constant(value);
            // null
            var body = Expression.Equal(fieldAccess, nullValue);
            // m.[fieldName] == value
            var expr = Expression.Lambda<Func<T, bool>>(body, parameter);
            // m => m.[fieldName] == value
            return expr;
        }

    }
}