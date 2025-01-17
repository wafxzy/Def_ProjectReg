using CommonReg.BLL.Services.Interfaces;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Request;
using CommonReg.Common.UIModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Services
{
    public class UserService : IUserService
    {
        public Task AddRoleForUser(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleForUser(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRolesForUser(Guid userId, List<int> roleIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleResponseModel>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileResponseModel> GetUserProfileById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> GetUserRolePermissions(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UpdateUserRequestModel updateUserRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
