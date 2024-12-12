using CommonReg.Common.Models;

namespace CommonReg.DAL.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id);
        Task<int> InsertUserRole(Guid userId, int roleId);
        Task AddRoleForUser(Guid userId, int roleId);
        Task InsertRolesForUser(Guid userId, List<int> roleIds);
        Task DeleteRoleForUser(Guid userId, int roleId);
        Task DeleteRolesForUser(Guid userId, List<int> roleIds);
        Task<IEnumerable<RoleEntity>> GetAllRoles();
        Task<IEnumerable<RolePermissionModel>> GetAllRolePermissions();
        Task<IEnumerable<int>> GetUserRolePermissions(Guid userId);
    }
}
