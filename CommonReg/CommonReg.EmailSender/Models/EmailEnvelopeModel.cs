using CommonReg.EmailSender.EmailTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Models
{
    public  class EmailEnvelopeModel
    {
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public BaseEmailTemplate EmailTemplate { get; set; }
    }
}
