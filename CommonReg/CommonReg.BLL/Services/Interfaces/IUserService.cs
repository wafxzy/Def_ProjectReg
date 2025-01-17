using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Request;
using CommonReg.Common.UIModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AccountEntity> GetUserByEmail(string email);
        Task<AccountEntity> GetUserById(Guid id);
        Task<UserProfileResponseModel> GetUserProfileById(Guid userId);
        Task<bool> UpdateUser(UpdateUserRequestModel updateUserRequestModel);
        Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id);
        Task<IEnumerable<RoleResponseModel>> GetAllRoles();
        Task AddRoleForUser(Guid userId, int roleId);
        Task DeleteRoleForUser(Guid userId, int roleId);
        Task DeleteRolesForUser(Guid userId, List<int> roleIds);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task DeleteUser(Guid userId);
        Task<List<int>> GetUserRolePermissions(Guid userId);
    }
}
