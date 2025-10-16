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
            var activeShift = source.DriverShifts
            ?.OrderByDescending(s => s.CreatedOn) 
            .FirstOrDefault();
            int seatNumber = activeShift?.Seats ?? source.AvailableSeats;
            int passangerCount =  source.DriverShifts?
            .SelectMany(ds => ds.Passenger)
            .Count(p => p.IsEnabled && p.EndDate > DateTime.Now)
            ?? 0;

            return seatNumber - passangerCount;
        }
    }
}
