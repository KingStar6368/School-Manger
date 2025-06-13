using Microsoft.EntityFrameworkCore;
using School_Manager.Data.Extensions;
using School_Manager.Data.Extensions.Auditing;
using School_Manager.Data.Identity;
using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.App;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Data.Context
{
    public class SchMSDBContext : DbContext
    {
		//public SchMSDBContext(DbContextOptions<SchMSDBContext> options)
		//    : base(options)
		//{

		//}       

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer(@"Password=Developers@Gamarak;Persist Security Info=True;User ID=sa;Initial Catalog=MaterialWHDB;Data Source=localhost\SQLSERVERDEV3;TrustServerCertificate=True;MultipleActiveResultSets=True;");            
		//}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Password=Developer@Gamarak;Persist Security Info=True;User ID=sa;Initial Catalog=SchMSDB;Data Source=DESKTOP-72A8ILV;TrustServerCertificate=True;MultipleActiveResultSets=True;");
		}

		#region -> [-- DBSETS Identity --]

		public DbSet<User> Users { get; set; }
        public DbSet<Trail> AuditTrails { get; set; }

        #endregion

        #region -> [-- DBSETS App --]

        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverChild> DriverChildren { get; set; }
        public DbSet<DriverContract> DriverContracts { get; set; }
        public DbSet<LocationData> LocationData { get; set; }
        public DbSet<LocationPair> LocationPair { get; set; }
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<PayBill> PayBills { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<ServiceContract> ServiceContracts { get; set; }

        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // configuration entities
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            // query filters
            modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(p => p.IsDeleted == false);
            modelBuilder.AppendGlobalQueryFilter();

        }

        // Handle audit fields (createdOn, createdBy, modifiedBy, modifiedOn) and handle soft delete on save changes
        //public override async Task<int> SaveChangesAsync()
        //{

        //    var auditEntries = HandleAuditingBeforeSaveChanges();

        //    var result = await base.SaveChangesAsync();

        //    await HandleAuditingAfterSaveChangesAsync(auditEntries);

        //    return result;
        //}

        public override int SaveChanges()
        {
            var auditEntries = HandleAuditingBeforeSaveChanges();

            var result = base.SaveChanges();

            HandleAuditingAfterSaveChanges(auditEntries);

            return result;
        }

        private List<AuditTrail> HandleAuditingBeforeSaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList()) // Auditable fields / soft delete on tables with IAuditableEntity
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = Environment.UserName != null ? Guid.Parse(Environment.UserName) : Guid.Empty;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = Environment.UserName != null ? Guid.Parse(Environment.UserName) : Guid.Empty;
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete softDelete) // intercept delete requests, forward as modified on tables with ISoftDelete
                        {
                            entry.State = EntityState.Modified;
                            softDelete.DeletedOn = DateTime.Now;
                            softDelete.DeletedBy = Environment.UserName != null ? Guid.Parse(Environment.UserName) : Guid.Empty;
                            softDelete.IsDeleted = true;
                        }

                        break;
                }
            }

            // for generic type
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                var entityType = entry.Entity.GetType();
                var isAuditable = entityType.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IAuditableEntity<>));

                if (isAuditable)
                {
                    dynamic auditableEntity = entry.Entity;
                    Type idType = entityType.GetInterface("IAuditableEntity`1")?.GetGenericArguments()[0];

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedOn = DateTime.Now;
                            auditableEntity.CreatedBy = UserSession.UserID; //ConvertCurrentUserId(idType);
                            break;
                        case EntityState.Modified:
                            auditableEntity.LastModifiedOn = DateTime.Now;
                            auditableEntity.LastModifiedBy = UserSession.UserID; //ConvertCurrentUserId(idType);
                            break;
                    }
                }

                var isSoftDelete = entityType.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(ISoftDelete<>));

                if (isSoftDelete && entry.State == EntityState.Deleted)
                {
                    dynamic softDelete = entry.Entity;
                    Type idType = entityType.GetInterface("ISoftDelete`1")?.GetGenericArguments()[0];

                    entry.State = EntityState.Modified;
                    softDelete.DeletedOn = DateTime.Now;
                    softDelete.DeletedBy = UserSession.UserID;  //ConvertCurrentUserId(idType);
                    softDelete.IsDeleted = true;
                }
            }


            ChangeTracker.DetectChanges();

            var trailEntries = new List<AuditTrail>();
            foreach (var entry in ChangeTracker.Entries<IAuditableEntitySaveChange>() // IAuditableEntity
                .Where(e => e.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
                .ToList())
            {
                var trailEntry = new AuditTrail(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = Environment.UserName != null ? int.Parse(Environment.UserName) : 0
                };
                trailEntries.Add(trailEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        trailEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        trailEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trailEntry.TrailType = TrailType.Create;
                            trailEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            trailEntry.TrailType = TrailType.Delete;
                            trailEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && entry.Entity is ISoftDelete && property.OriginalValue == null && property.CurrentValue != null)
                            {
                                trailEntry.ChangedColumns.Add(propertyName);
                                trailEntry.TrailType = TrailType.Delete;
                                trailEntry.OldValues[propertyName] = property.OriginalValue;
                                trailEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            else if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                            {
                                trailEntry.ChangedColumns.Add(propertyName);
                                trailEntry.TrailType = TrailType.Update;
                                trailEntry.OldValues[propertyName] = property.OriginalValue;
                                trailEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in trailEntries.Where(e => !e.HasTemporaryProperties))
            {
                AuditTrails.Add(auditEntry.ToAuditTrail());
            }

            return trailEntries.Where(e => e.HasTemporaryProperties).ToList();
        }

        private int HandleAuditingAfterSaveChanges(List<AuditTrail> trailEntries)
        {
            if (trailEntries == null || trailEntries.Count == 0)
            {
                return 0;
            }

            foreach (var entry in trailEntries)
            {
                foreach (var prop in entry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                AuditTrails.Add(entry.ToAuditTrail());
            }

            return SaveChanges();
        }

        private Task HandleAuditingAfterSaveChangesAsync(List<AuditTrail> trailEntries, CancellationToken cancellationToken = new())
        {
            if (trailEntries == null || trailEntries.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var entry in trailEntries)
            {
                foreach (var prop in entry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                AuditTrails.Add(entry.ToAuditTrail());
            }

            return SaveChangesAsync(cancellationToken);
        }

        private dynamic ConvertCurrentUserId(Type targetType)
        {
            if (string.IsNullOrEmpty(UserSession.UserID.ToString())) return null;

            if (targetType == typeof(Guid))
                return Guid.Parse(UserSession.UserID.ToString());

            if (targetType == typeof(int))
                return int.Parse(UserSession.UserID.ToString());

            throw new NotSupportedException($"نوع {targetType.Name} پشتیبانی نمیشود.");
        }

    }
}
