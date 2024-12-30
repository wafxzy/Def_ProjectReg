using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.UIModels
{
    public class ConfirmRegistrationRequestModel
    {
        public Guid UserId { get; set; }
        public Guid Code { get; set; }
    }
}
