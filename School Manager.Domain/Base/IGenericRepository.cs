using System.Linq.Expressions;

namespace School_Manager.Domain.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommand(string sql, params object[] parameters);
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        IQueryable<TEntity> Query(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        List<Expression<Func<TEntity, object>>> includes = null,
        List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> thenIncludes = null,
        bool asNoTracking = false
        );

        IQueryable<TResult> Query<TResult>(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, TResult>> selector = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null
        );


        IQueryable<TResult> Query<TEntity, TResult>(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, TResult>> selector = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null
        ) where TEntity : class;

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        TEntity GetById(int id);
        TEntity GetById(long id);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> FindAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        public void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
