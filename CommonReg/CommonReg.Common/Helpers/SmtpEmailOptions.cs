using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Helpers
{
    public class SmtpEmailOptions
    {

        public const string SMTP_EMAIL = "SmtpEmail";
        public string MailFrom { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }
    }
}
