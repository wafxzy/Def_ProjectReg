using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.UIModels.User.Response
{
    public class UserProfileResponseModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public IEnumerable<RoleResponseModel> Roles { get; set; }
        public byte[] Avatar { get; set; }
    }
}
