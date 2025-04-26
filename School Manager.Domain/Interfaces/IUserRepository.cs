using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Identity;

namespace School_Manager.Domain.Interfaces
{
    public interface IUserRepository : IUnitOfWork
    {
        int UserById(int id);

    }
}
