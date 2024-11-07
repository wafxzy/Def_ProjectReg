namespace CommonReg.Common.Models
{
    public class RolePermissionModel
    {
        public int RoleId { get; set; }
        public string RoleSystemName { get; set; }
        public int PermissionId { get; set; }
        public string PermissionSystemName { get; set; }
    }
}
