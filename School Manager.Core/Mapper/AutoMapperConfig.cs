using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper MapperInstance { get; private set; }

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            // انتخاب نمونه mapper
            MapperInstance = config.CreateMapper();
        }
    }

}
