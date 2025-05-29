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
        UserVM GetUserById(int id);
        //List<UserVM> GetAll();
        Task<List<UserVM>> GetAllAsync();
        UserVM CheckAuthorize(string UserName,string Password);
		Task<List<UserDTO>> GetListAsync();
		UserDTO GetUserDetail(int Id);
		//int SaveUser(UserCreateDTO User, List<UserRoleCreateDTO> UserRoles);
		//bool Update(UserEditDTO User, List<UserRoleCreateDTO> UserRoles);
		bool Delete(int UserId);
        bool IsMobileRegistered(string Mobile);
	}
}
