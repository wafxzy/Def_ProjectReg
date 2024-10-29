namespace CommonReg.Common.Models
{
    public class RolePermissionModel
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        // Связь с RoleEntity
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
