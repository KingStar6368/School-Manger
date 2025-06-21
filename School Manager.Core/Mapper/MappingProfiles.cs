using AutoMapper;
//using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Core.Classes;
using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Bill
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.ContractId, opt => opt.MapFrom(dest => dest.ServiceContractRef))
                .ForMember(dest => dest.BillExpiredTime, opt => opt.MapFrom(dest => dest.EstimateTime))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(dest => dest.PaidPrice, opt => opt.MapFrom<PaidPriceResolver>())
                .ForMember(dest => dest.PaidTime, opt => opt.MapFrom<PaidTimeResolver>())
                .ForMember(dest => dest.HasPaid, opt => opt.MapFrom<HasPaidResolver>())
                .ForMember(dest => dest.Status, opt => opt.MapFrom<StatusResolver>());
                
            CreateMap<BillCreateDto, Bill>();
            CreateMap<BillUpdateDto, Bill>();
            #endregion
            #region Car
            CreateMap<Car, CarInfoDto>()
                .ForMember(dest => dest.Color,opt => opt.MapFrom<ColorResolver>())
                .ForMember(dest=>dest.PlateNumber,opt=>opt.MapFrom(src=>src.FirstIntPlateNumber+src.ChrPlateNumber+src.SecondIntPlateNumber+src.ThirdIntPlateNumber));
            CreateMap<CarCreateDto, Car>()
                .ForMember(dest => dest.carType,opt => opt.MapFrom(src=> (CarType)src.carType));
            CreateMap<CarUpdateDto, Car>()
                .ForMember(dest => dest.carType, opt => opt.MapFrom(src => (CarType)src.carType));
            #endregion
            #region Cheque
            CreateMap<ChequeCreateDto, Cheque>();
            CreateMap<ChequeUpdateDto, Cheque>();
            #endregion
            #region Child
            CreateMap<Child, ChildInfo>()
                .ForMember(dest => dest.DriverId,opt => opt.MapFrom<ActiveDriverIdResolver>())
                .ForMember(dest => dest.SchoolId,opt => opt.MapFrom(src => src.SchoolRef))
                .ForMember(dest => dest.Class,opt => opt.MapFrom(src => src.Class.GetDisplayName()))
                .ForMember(dest => dest.Path,opt => opt.MapFrom(src => src.LocationPairs.FirstOrDefault(i => i.IsActive)));

            CreateMap<ChildCreateDto,Child>();
            CreateMap<ChildUpdateDto, Child>();
            #endregion
            #region Driver
            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Cars.FirstOrDefault(i => i.IsActive)))
                .ForMember(dest=>dest.Passanger, opt => opt.MapFrom(src => src.Passanger.Select(p => p.ChildRef).ToList()))
                .ForMember(dest => dest.BankAccount,opt => opt.MapFrom<BankAccountResolver>());
            CreateMap<DriverCreateDto, Driver>();
            CreateMap<DriverUpdateDto, Driver>();
            #endregion
            #region DriverContract
            CreateMap<DriverContractCreateDto, DriverContract>();
            CreateMap<DriverContractUpdateDto, DriverContract>();
            CreateMap<DriverContractChequeUpdateDto, DriverContractCheque>();
            #endregion
            #region Location Data
            CreateMap<LocationData, LocationDataDto>();

            #endregion
            #region LocationPair
            //CreateMap<LocationPair, LocationPairModel>();
            CreateMap<LocationPair, LocationPairModel>()
                .ForMember(dest=>dest.ChildId,opt=>opt.MapFrom(src=>src.ChildRef))
                .ForMember(dest => dest.Location1,opt => opt.MapFrom(src =>src.Locations.FirstOrDefault(l => l.LocationType == LocationType.Start)))
                .ForMember(dest => dest.Location2,opt => opt.MapFrom(src =>src.Locations.FirstOrDefault(l => l.LocationType == LocationType.End)));
            #endregion
            #region LookUp
            CreateMap<Lookup, LookupComboViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value));
            #endregion
            #region Parent
            CreateMap<Parent, ParentDto>()
                .ForMember(dest => dest.ParentFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.IsMale?"آقا":"خانم"))
                .ForMember(dest => dest.ParentLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ParentNationalCode, opt => opt.MapFrom(src => src.NationalCode))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
            CreateMap<ParentCreateDto, Parent>();
            CreateMap<ParentUpdateDto, Parent>();
            #endregion
            #region RawMaterial
            CreateMap<RawMaterial, RawMaterialCombo>()
                .ForMember(dest => dest.DisplayMember, opt => opt.MapFrom(src => src.MaterialCode + " " + src.Name))
                .ForMember(dest => dest.RawMaterialId, opt => opt.MapFrom(src => src.Id));
            CreateMap<RawMaterial, RawMaterialDTO>();
            CreateMap<RawMaterialDTO, RawMaterial>();
            //CreateMap<RawMaterial, RawMaterialDetail>()
            //.ForMember(dest => dest.SecondaryUnit, opt => opt.MapFrom(src => src.SecondaryUnitNavigation))
            //.AfterMap((src, dest) =>
            //{
            //    if (dest.Units == null)
            //        dest.Units = new List<UnitViewModel>();

            //    if(src.SecondaryUnitNavigation != null)
            //        dest.Units.Add(new UnitViewModel { Name = src.SecondaryUnitNavigation.Name, UnitId = src.SecondaryUnitNavigation.Id });

            //    if (src.MajorUnitNavigation != null)
            //        dest.Units.Add(new UnitViewModel { Name = src.MajorUnitNavigation.Name, UnitId = src.MajorUnitNavigation.Id});
            //});

            //CreateMap<RawMaterial, RawMaterialGrid>()
            //    .ForMember(dest => dest.RawMaterialId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.RawMaterialName, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.RawMaterialCode, opt => opt.MapFrom(src => src.MaterialCode))
            //    .ForMember(dest => dest.RawMaterialWeight, opt => opt.MapFrom(src => src.UnitConversion))
            //    .ForMember(dest => dest.MajorUnitName, opt => opt.MapFrom(src => src.MajorUnitNavigation.Name ?? ""))
            //    .ForMember(dest => dest.SecondaryUnitName, opt => opt.MapFrom(src => src.SecondaryUnitNavigation.Name));

            #endregion
            #region school
            CreateMap<School, LocationDataDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));
            CreateMap<School, SchoolDto>()
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=> src));
            CreateMap<School, SchoolDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.StudentIds,
                    opt => opt.MapFrom(src => src.Childs.Select(x=>x.Id).ToList()))
                .ForMember(dest => dest.DriverIds,
                    opt => opt.MapFrom(src =>src.Childs.SelectMany(c => c.DriverChilds).Where(dc => dc.DriverNavigation != null)
                    .Select(dc => dc.DriverRef).Distinct()));
            CreateMap<SchoolCreateDto, School>();
            CreateMap<SchoolUpdateDto, School>();
            #endregion
            #region Service
            CreateMap<ServiceContract, ServiceContractDto>();
            #endregion
            #region SrviceContract
            CreateMap<ServiceContractCreateDto, ServiceContract>();
            CreateMap<ServiceContractUpdateDto, ServiceContract>();
            CreateMap<ServiceContractChequeUpdateDto, ServiceContractCheque>();
            #endregion
            #region User
            //CreateMap<User, UserVM>()
            //    .ForMember(dest => dest.formOprs, opt => opt.MapFrom(src => src.UserRoles.SelectMany(x=>x.RoleNavigation.AppRoleOperationAccess)));
            //CreateMap<AppRoleOperationAccess, FormOpr>()
            //    .ForMember(dest => dest.FormName, opt => opt.MapFrom(src => src.AppFormsOperationNavigation.AppFormNavigation.Name))
            //    .ForMember(dest => dest.OprName, opt => opt.MapFrom(src => src.AppFormsOperationNavigation.Name));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Status,opt => opt.MapFrom(src => src.IsActive?"فعال":"غیر فعال"))
                .ForMember(dest => dest.FullName,opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
            CreateMap<UserCreateDTO, User>();
			CreateMap<UserUpdateDTO, User>();
			#endregion
        }
    }
}

