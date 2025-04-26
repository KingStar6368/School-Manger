using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School_Manager.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        // Append a query filter onto any entity class that implements the interface TInterface
        public static ModelBuilder AppendGlobalQueryFilter<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
        {
            // Gets a list of entities that implement the interface TInterface
            var entities = modelBuilder.Model
                .GetEntityTypes()
                .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
                .Select(e => e.ClrType);
            foreach (var entity in entities)
            {
                var parameterType = Expression.Parameter(modelBuilder.Entity(entity).Metadata.ClrType);
                var expressionFilter = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameterType, expression.Body);

                // get existing query filters of the entity(s)
                var currentQueryFilter = modelBuilder.Entity(entity).GetQueryFilter();
                if (currentQueryFilter != null)
                {
                    var currentExpressionFilter = ReplacingExpressionVisitor.Replace(currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);

                    // append new query filter with the existing filter
                    expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
                }

                var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);

                // applies the filter to the entity(s)
                modelBuilder.Entity(entity).HasQueryFilter(lambdaExpression);
            }

            return modelBuilder;
        }

        private static LambdaExpression GetQueryFilter(this EntityTypeBuilder builder)
        {
            return builder?.Metadata?.GetQueryFilter();
        }

    }
}
