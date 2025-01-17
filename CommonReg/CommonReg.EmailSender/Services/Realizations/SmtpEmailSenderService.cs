using CommonReg.Common.Helpers;
using CommonReg.EmailSender.EmailTemplates;
using CommonReg.EmailSender.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Services.Realizations
{
    public  class SmtpEmailSenderService
    {
        private const string EMAIL_WAS_NOT_SENT_ERR_MSG = "The email was not sent.";

        private readonly ILogger _logger;
        private readonly EmailEnvelopeModel _email;
        private readonly SmtpClient _client;
        private readonly EmailOptions _emailOptions;

        public SmtpEmailSenderService(
            ILogger logger,
            EmailEnvelopeModel email,
            SmtpClient client,
            EmailOptions emailOptions
            )
        {
            _logger = logger;
            _client = client;
            _email = email;
            _emailOptions = emailOptions;
        }

        private LinkedResource GetLinkedResource(string cid, string imageName)
        {
            string assemblyLocation = Path.GetDirectoryName(Assembly.GetAssembly(GetType()).Location);
            string imagePath = Path.Combine(assemblyLocation, "Images", imageName);

            LinkedResource imageResource = new LinkedResource(imagePath, "image/png");
            imageResource.ContentId = cid;
            return imageResource;
        }

        public async Task SendConcreteEmailAsync()
        {
            try
            {
                BaseEmailTemplate concreteEmailTemplate = await _email.EmailTemplate.GetTemplateData();

                string fromEmail = string.IsNullOrWhiteSpace(_email.FromEmail) ? _emailOptions.InfoEmail : _email.FromEmail;

                MailMessage sendRequest = new(fromEmail, _email.ToEmail, concreteEmailTemplate.Subject, concreteEmailTemplate.Body);

                ContentType mimeType = new("text/html");
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(concreteEmailTemplate.Body, mimeType);
                sendRequest.AlternateViews.Add(htmlBody);

                htmlBody.LinkedResources.Add(GetLinkedResource("signature", "image.png"));
                htmlBody.LinkedResources.Add(GetLinkedResource("logo", "logo.png"));
                sendRequest.AlternateViews.Add(htmlBody);

                await _client.SendMailAsync(sendRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, EMAIL_WAS_NOT_SENT_ERR_MSG);
            }
        }
    }
}
