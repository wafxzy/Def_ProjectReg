using CommonReg.Common.Models;
using CommonReg.DAL.Queries;
using CommonReg.DAL.Repositories.Interfaces;
using Dapper;

namespace CommonReg.DAL.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDataBaseContext _context;

        public UserRoleRepository(IDataBaseContext context)
        {
            _context = context;
        }

        public Task AddRoleForUser(Guid userId, int roleId)
        {
        return _context.Connection.ExecuteAsync(UserRoleQueries.INSERT_ROLE_FOR_USER,
            new { userId, roleId }, transaction: _context.Transaction);
        }

        public Task DeleteRoleForUser(Guid userId, int roleId)
        {
            return _context.Connection.ExecuteAsync(UserRoleQueries.DELETE_ROLE_FOR_USER,
                new { userId, roleId }, transaction: _context.Transaction);
        }

        public Task DeleteRolesForUser(Guid userId, List<int> roleIds)
        {
            return _context.Connection.ExecuteAsync(UserRoleQueries.DELETE_ROLES_FOR_USER,
                new { userId, roleIds }, transaction: _context.Transaction);
        }

        public Task<IEnumerable<RolePermissionModel>> GetAllRolePermissions()
        {
            return _context.Connection.QueryAsync<RolePermissionModel>(
                UserRoleQueries.GET_ALL_ROLE_PERMISSIONS , transaction: _context.Transaction);
        }

        public Task<IEnumerable<RoleEntity>> GetAllRoles()
        {
            return _context.Connection.QueryAsync<RoleEntity>(UserRoleQueries.GET_ALL_ROLES, transaction: _context.Transaction);
        }

        public Task<IEnumerable<int>> GetUserRolePermissions(Guid userId)
        {
            return _context.Connection.QueryAsync<int>(UserRoleQueries.GET_ALL_USER_PERMISSIONS,
                 new { userId }, transaction: _context.Transaction);
        }

        public Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id)
        {
            return _context.Connection.QueryAsync<UserRoleModel>(UserRoleQueries.GET_USER_ROLES,
                 new { id }, transaction: _context.Transaction);
        }

        public Task InsertRolesForUser(Guid userId, List<int> roleIds)
        {
            return _context.Connection.ExecuteAsync(UserRoleQueries.INSERT_ROLES_FOR_USER,
                 new { userId, roleIds }, transaction: _context.Transaction);
        }

        public Task<int> InsertUserRole(Guid userId, int roleId)
        {
            return _context.Connection.ExecuteAsync(UserRoleQueries.INSERT_USER_ROLE,
                new { UserId = userId, RoleId = roleId }, transaction: _context.Transaction);
        }
    }
}
