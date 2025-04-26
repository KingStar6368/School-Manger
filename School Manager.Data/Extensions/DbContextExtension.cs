using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace School_Manager.Data.Extensions
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
            //return false;
        }

        public static void AppendGlobalQueryFilter(this ModelBuilder builder)
        {
            // پیمایش روی تمام موجودیت‌های مدل
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                // بررسی اینکه آیا ClrType پیاده‌سازی یکی از اینترفیس‌های generics ISoftDelete<...> را دارد یا خیر.
                var softDeleteInterface = entityType.ClrType.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISoftDelete<>));

                if (softDeleteInterface != null)
                {
                    // فرض بر این است که نام پراپرتی مربوط به حذف نرم "IsDeleted" است.
                    var propertyInfo = softDeleteInterface.GetProperty("IsDeleted");
                    if (propertyInfo == null)
                        continue;

                    // ساخت پارامتر lamda: e =>
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    // ابتدا موجودیت را به اینترفیس مورد نظر cast می‌کنیم.
                    // ((ISoftDelete<T>)e).IsDeleted
                    var castExpression = Expression.Convert(parameter, softDeleteInterface);
                    var propertyAccess = Expression.Property(castExpression, propertyInfo);

                    // شرط مورد نظر: ((ISoftDelete<T>)e).IsDeleted == false
                    var condition = Expression.Equal(propertyAccess, Expression.Constant(false));

                    // ساخت لامبدا نهایی: e => ((ISoftDelete<T>)e).IsDeleted == false
                    var lambda = Expression.Lambda(condition, parameter);

                    // اعمال فیلتر به مدل
                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

    }
}
