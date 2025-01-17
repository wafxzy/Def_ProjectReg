using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Models
{
    public  class ForgotPasswordViewModel
    {
        public string Link { get; set; }
        public string  LogoImgUrl { get; set; }
        public string PasswordImgUrl { get; set; }
        public string HelpEmail { get; set; }
        public string EmailReceiver { get; set; }
    }
}
