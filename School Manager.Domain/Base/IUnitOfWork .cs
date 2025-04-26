using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Interfaces;


namespace School_Manager.Domain.Base
{
    public interface IUnitOfWork : IDisposable
    {      
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync(); 
        void Rollback();
        void BeginTransaction();
    }
}
