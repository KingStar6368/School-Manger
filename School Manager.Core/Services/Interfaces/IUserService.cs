using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserById(long id);
        Task<List<UserDTO>> GetAllAsync();
        Task<List<UserDTO>> GetAllAsyncDrivers();
        Task<List<UserDTO>> GetAllAsyncParents();
        bool UserHaveNationalCode(string NationalCode);
        List<UserDTO> GetAllParents();
        UserDTO CheckAuthorize(string UserName,string Password);
		Task<List<UserDTO>> GetListAsync();
		UserDTO GetUserDetail(long Id);
        UserDTO GetUserByMobile(string Mobile);
        long CreateUser(UserCreateDTO User);
        bool UpdateUser(UserUpdateDTO User);
        bool DeleteUser(long UserId);
        bool IsMobileRegistered(string Mobile);
        /// <summary>
        /// گرفتن کد والد با کد یوزر
        /// </summary>
        /// <param name="id">کد یوزر</param>
        /// <returns>کد والد</returns>
        long GetPanretId(long id);
	}
}
