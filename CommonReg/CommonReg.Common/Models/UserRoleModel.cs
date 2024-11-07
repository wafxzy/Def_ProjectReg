using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Models
{
    public class UserRoleModel
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
