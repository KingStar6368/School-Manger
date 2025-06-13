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

namespace School_Manager.Core.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Bill
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.ServiceContractRef));
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
            #region Service
            CreateMap<ServiceContract, ServiceContractDto>();
            #endregion
            #region Parent
            CreateMap<Parent, ParentDto>();
            #endregion
            #region Child
            CreateMap<Child, ChildInfo>();
            #endregion
            #region LocationPair
            CreateMap<LocationPair, LocationPairModel>();
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
			CreateMap<UserEditDTO, User>();
			#endregion
            #region LookUp
            CreateMap<Lookup, LookupComboViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value));
            #endregion
        }
    }
}

