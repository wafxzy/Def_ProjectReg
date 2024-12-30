using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.UIModels
{
    public class RestorePasswordRequestModel
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid Code { get; set; }
    }
}
