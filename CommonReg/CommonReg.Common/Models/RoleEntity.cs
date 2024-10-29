namespace CommonReg.Common.Models
{
    public class RoleEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleType { get; set; }
        public List<RolePermissionModel> Permissions { get; set; } = new List<RolePermissionModel>();

    }
}
