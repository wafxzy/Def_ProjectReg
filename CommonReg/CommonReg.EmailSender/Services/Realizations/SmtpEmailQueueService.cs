using CommonReg.Common.Helpers;
using CommonReg.EmailSender.Models;
using CommonReg.EmailSender.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CommonReg.EmailSender.Services.Realizations
{
    public class SmtpEmailQueueService : IEmailQueueService
    {

        private readonly ILogger<SmtpEmailQueueService> _logger;
        private readonly BufferBlock<SmtpEmailSenderService> _buffer;
        private readonly SmtpClient _client;
        private readonly EmailOptions _emailOptions;

        public SmtpEmailQueueService(
            ILogger<SmtpEmailQueueService> logger,
            SmtpClient client,
            IOptions<EmailOptions> emailOptions
            )
        {
            _emailOptions = emailOptions.Value;

            _logger = logger;

            _client = client;

            DataflowBlockOptions bufferOptions = new()
            {
                BoundedCapacity = 10000,
                MaxMessagesPerTask = 1
            };

            _buffer = new BufferBlock<SmtpEmailSenderService>(bufferOptions);

            ExecutionDataflowBlockOptions actionOptions = new()
            {
                MaxMessagesPerTask = 1
            };

            ActionBlock<SmtpEmailSenderService> action = new(ProcessAsync, actionOptions);

            _buffer.LinkTo(action);
        }

        public async Task<bool> PushAsync(EmailEnvelopeModel emailEnvelope)
        {
            return await _buffer.SendAsync(new SmtpEmailSenderService(_logger, emailEnvelope, _client, _emailOptions));
        }

        private static async Task ProcessAsync(SmtpEmailSenderService emailSender)
        {
            await emailSender.SendConcreteEmailAsync();
        }
    }
}
