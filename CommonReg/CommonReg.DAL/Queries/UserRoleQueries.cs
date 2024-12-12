namespace CommonReg.DAL.Queries
{
    public static class UserRoleQueries
    {
        public const string GET_ALL_ROLES = @"
SELECT *
FROM role 
;";

        public const string GET_ALL_ROLE_PERMISSIONS = @"SELECT 
ro.id as RoleId,
ro.system_name as RoleSystemName,
p.id as PermissionId,
p.system_Name as PermissionSystemName
FROM role as ro
INNER JOIN role_permission AS rp ON ro.id = rp.role_id
INNER JOIN permission AS p ON rp.permission_id = p.id 
WHERE ro.id = rp.role_id and p.id = rp.permission_id;";

        public const string GET_ALL_USER_PERMISSIONS = @"SELECT 
p.id
FROM role as ro
INNER JOIN role_permission AS rp ON ro.id = rp.role_id
INNER JOIN permission AS p ON rp.permission_id = p.id 
INNER JOIN user_roles as ur ON rp.role_id = ur.role_id
INNER JOIN users as u ON ur.user_id = u.id
WHERE ro.id = rp.role_id and p.id = rp.permission_id and u.id = @userId;";


        public const string INSERT_ROLE_FOR_USER = @"
INSERT INTO user_roles (user_id, role_id)
VALUES (@userId, @roleId)";

        public const string DELETE_ROLE_FOR_USER = @"
DELETE FROM user_roles
WHERE 
     user_id = @userId 
AND  role_id = @roleId;";

        public const string DELETE_ROLES_FOR_USER = @"
DELETE FROM user_roles
WHERE 
     user_id = @userId 
AND  role_id = ANY(@roleIds);";

        public const string INSERT_ROLES_FOR_USER = @"
INSERT INTO user_roles (user_id, role_id)
Select @userId, unnest(@roleIds)";

        public const string GET_USER_ROLES = @"
  SELECT roles.system_name as RoleName, ur.user_id, ur.role_id
  FROM user_roles AS ur
  INNER JOIN role AS roles ON roles.id = ur.role_id
  WHERE ur.user_id = @userId;";

        public const string INSERT_USER_ROLE = @"INSERT INTO user_roles (user_id, role_id) VALUES (@UserId, @RoleId);";
    }
}
