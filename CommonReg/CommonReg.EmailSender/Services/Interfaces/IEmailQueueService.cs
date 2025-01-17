using CommonReg.EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Services.Interfaces
{
    public interface IEmailQueueService
    {
         Task<bool> PushAsync(EmailEnvelopeModel emailEnvelope);
    }
}
