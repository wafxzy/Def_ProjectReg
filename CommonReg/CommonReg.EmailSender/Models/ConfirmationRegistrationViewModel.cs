using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Models
{
    public class ConfirmationRegistrationViewModel
    {
        public string Link { get; set; }
        public string UserName { get; set; }
        public string SupportEmail { get; set; }
        public string LogoUrl { get; set; }
        public string HeadUrl { get; set; }
    }
}
