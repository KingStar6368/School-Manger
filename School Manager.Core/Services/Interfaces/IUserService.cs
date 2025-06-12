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
        UserVM GetUserById(long id);
        //List<UserVM> GetAll();
        Task<List<UserVM>> GetAllAsync();
        UserVM CheckAuthorize(string UserName,string Password);
		Task<List<UserDTO>> GetListAsync();
		UserDTO GetUserDetail(long Id);
        long SaveUser(UserCreateDTO User);
        bool Update(UserEditDTO User);
        bool Delete(long UserId);
        bool IsMobileRegistered(string Mobile);
        /// <summary>
        /// گرفتن کد والد با کد یوزر
        /// </summary>
        /// <param name="id">کد یوزر</param>
        /// <returns>کد والد</returns>
        long GetPanretId(long id);
	}
}
