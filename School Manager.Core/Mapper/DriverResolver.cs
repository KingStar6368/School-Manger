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
    public class AvailableSeatsResolver : IValueResolver<Driver, DriverDto, int>
    {
        public int Resolve(Driver source, DriverDto destination, int destMember, ResolutionContext context)
        {
            var car = source.Cars?.FirstOrDefault(x => x.IsActive);
            int seatNumber = car?.SeatNumber ?? source.AvailableSeats;
            int passangerCount = source.Passanger?.Where(x=>x.IsEnabled).ToList().Count ?? 0;

            return seatNumber - passangerCount;
        }
    }
}
