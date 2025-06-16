using AutoMapper;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Mapper
{
    public class ActiveDriverIdResolver : IValueResolver<Child, ChildInfo, long?>
    {
        public long? Resolve(Child source, ChildInfo destination, long? destMember, ResolutionContext context)
        {
            return source.DriverChilds?.FirstOrDefault(x => x.IsEnabled)?.DriverRef;
        }
    }
}
