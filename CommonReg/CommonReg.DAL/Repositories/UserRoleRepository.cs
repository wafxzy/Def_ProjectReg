using CommonReg.Common.Models;
using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
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

        public Task<IEnumerable<RolePermissionModel>> GetAllRolePermissions()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleEntity>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> GetUserRolePermissions(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertRolesForUser(Guid userId, List<int> roleIds)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertUserRole(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
