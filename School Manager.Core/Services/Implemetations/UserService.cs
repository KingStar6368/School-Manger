using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Utilities.Values;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Domain.Interfaces;

namespace School_Manager.Core.Services.Implemetations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserVM CheckAuthorize(string UserName, string Password)
        {
            var data = _unitOfWork.GetRepository<User>().Query(
				predicate: p=>p.UserName == UserName && p.PasswordHash == EntityHelper.Encrypt(Password),
				orderBy:null,
				includes:new List<System.Linq.Expressions.Expression<Func<User, object>>>{
					//c=>c.UserRoles
				},
                new List<Func<IQueryable<User>, IQueryable<User>>> 
				{ 
					//c=> c.Include(x=>x.UserRoles)
					//.ThenInclude(x=>x.RoleNavigation)
					//.ThenInclude(x=>x.AppRoleOperationAccess)
					//.ThenInclude(x=>x.AppFormsOperationNavigation)
					//.ThenInclude(x=>x.AppFormNavigation)
				}).FirstOrDefault();
            return _mapper.Map<UserVM>(data);
        }

        public async Task<List<UserVM>> GetAllAsync()
        {
            var results = await _unitOfWork.GetRepository<User>().GetAllAsync();
            return results.Select(a=>new UserVM { Id = a.Id}).ToList();
        }

        /// <summary>
        /// لیست اطلاعات کاربران
        /// </summary>
        /// <returns></returns>
		public async Task<List<UserDTO>> GetListAsync()
		{
			var data = await _unitOfWork.GetRepository<User>().GetAllAsync();			
			return _mapper.Map<List<UserDTO>>(data);
		}




		public UserVM GetUserById(int id)
        {
            var d = _unitOfWork.GetRepository<User>().GetByIdAsync(id).Result;
            return new UserVM
            {
                Id = d.Id,
            }; 
        }


		public UserDTO GetUserDetail(int Id)
		{
			var result = _unitOfWork.GetRepository<User>().GetByIdAsync(Id).Result;
			return _mapper.Map<UserDTO>(result);
		}


		public int SaveUser(UserCreateDTO User)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				var newUser = _mapper.Map<User>(User);
				_unitOfWork.GetRepository<User>().Add(newUser);
				_unitOfWork.SaveChanges();
				_unitOfWork.SaveChanges();
				_unitOfWork.Commit();
				return newUser.Id;
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}


		public bool Update(UserEditDTO User)
		{
			try
			{
				_unitOfWork.BeginTransaction();

				var existingUser = _unitOfWork.GetRepository<User>().GetById(User.Id);
				if (existingUser == null)
				{
					return false;
				}
				// بروزرسانی اطلاعات کاربر
				_mapper.Map(User, existingUser);
				_unitOfWork.GetRepository<User>().Update(existingUser);

				_unitOfWork.SaveChanges();
				_unitOfWork.Commit();
				return true;
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public bool Delete(int UserId)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				var user = _unitOfWork.GetRepository<User>().Query(
				predicate: p => p.Id == UserId).FirstOrDefault();
			
				if (user != null)
				{
					_unitOfWork.GetRepository<User>().Remove(user);
					//var existingRoles = _unitOfWork.GetRepository<UserRole>().Query(r => r.UserRef == UserId).ToList();
					//_unitOfWork.GetRepository<UserRole>().RemoveRange(existingRoles);
				}
			

				_unitOfWork.SaveChanges();

				_unitOfWork.Commit();

				return true;
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

        public bool IsMobileRegistered(string Mobile)
        {
            var ds =  _unitOfWork.GetRepository<User>().Query(
                predicate: p => p.Mobile == Mobile
                ).Any();
			return ds;
        }
    }
}
