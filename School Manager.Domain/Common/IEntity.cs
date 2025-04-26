using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Common
{
    public interface IEntity
    {

    }
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; }
    }
}
