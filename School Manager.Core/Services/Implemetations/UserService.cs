using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Utilities.Values;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Domain.Interfaces;
using System.Reflection;

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

        public UserDTO CheckAuthorize(string UserName, string Password)
        {
            var data = _unitOfWork.GetRepository<User>().Query(p=>p.UserName == UserName && p.PasswordHash == Password).FirstOrDefault();
            return _mapper.Map<UserDTO>(data);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var results = await _unitOfWork.GetRepository<User>().GetAllAsync();
            return results.Select(a=>new UserDTO { Id = a.Id}).ToList();
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

		public long SaveUser(UserCreateDTO User)
		{
			try
			{
				if(_unitOfWork.GetRepository<User>().Query(x=>x.UserName == User.UserName).Any())
				{
					return _unitOfWork.GetRepository<User>().Query(x => x.UserName == User.UserName).FirstOrDefault()?.Id ?? 0;
				}
				_unitOfWork.BeginTransaction();
				var newUser = _mapper.Map<User>(User);
				_unitOfWork.GetRepository<User>().Add(newUser);
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


		public bool UpdateUser(UserUpdateDTO User)
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

        public UserDTO GetUserById(long id)
        {
            var d = _unitOfWork.GetRepository<User>().GetById(id);
            return _mapper.Map<UserDTO>(d);
        }

        public UserDTO GetUserDetail(long Id)
        {
            var d = _unitOfWork.GetRepository<User>().GetById(Id);
            return _mapper.Map<UserDTO>(d);
        }

        long IUserService.CreateUser(UserCreateDTO User)
        {
            return SaveUser(User);
        }

        public bool DeleteUser(long UserId)
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

        public long GetPanretId(long id)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query(x=>x.UserRef == id).FirstOrDefault();
			if(ds == null) return 0;
			return ds.Id;
        }

        public async Task<List<UserDTO>> GetAllAsyncDrivers()
        {
            var results = await _unitOfWork.GetRepository<User>().Query(x=>x.Drivers.Any()).ToListAsync();
            return _mapper.Map<List<UserDTO>>(results);
        }

        public async Task<List<UserDTO>> GetAllAsyncParents()
        {
            var results = await _unitOfWork.GetRepository<User>().Query(x => x.Parents.Any()).ToListAsync();
            return _mapper.Map<List<UserDTO>>(results);
        }

        public List<UserDTO> GetAllParents()
        {
            var results =  _unitOfWork.GetRepository<User>().Query(x => x.Parents.Any()).ToList();
            return _mapper.Map<List<UserDTO>>(results);
        }

        public bool UserHaveNationalCode(string NationalCode)
        {
            var results =  _unitOfWork.GetRepository<User>().Query(x => x.UserName == NationalCode.Trim());
			return results.Any();
        }

        public UserDTO GetUserByMobile(string Mobile)
        {
            var d = _unitOfWork.GetRepository<User>().Query(x=>x.Mobile == Mobile);
            return _mapper.Map<UserDTO>(d);
        }

        public long GetDriverId(long id)
        {
            var d = _unitOfWork.GetRepository<Driver>().Query(x => x.UserRef == id).Select(x=>x.Id).FirstOrDefault();
            return d;
        }
    }
}
