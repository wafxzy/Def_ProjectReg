using CommonReg.Common.Helpers;
using CommonReg.EmailSender.Services.Interfaces;
using CommonReg.EmailSender.Services.Realizations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Extensions
{
    public static  class ServiceCollectionExtension
    {

        public static IServiceCollection AddSmtpEmailServices(
        this IServiceCollection services, IConfiguration configuration
        )
        {

            IConfigurationSection smtpEmailConfigSection = configuration.GetSection(SmtpEmailOptions.SMTP_EMAIL);

            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.EMAIL));
            services.Configure<SmtpEmailOptions>(smtpEmailConfigSection);

            services.AddSingleton<IEmailQueueService, SmtpEmailQueueService>();

            services.AddSingleton(_ =>
            {
                SmtpEmailOptions emailSettings = smtpEmailConfigSection.Get<SmtpEmailOptions>();

                bool useDefaultCredentials = string.IsNullOrWhiteSpace(emailSettings.Login)
                                             || string.IsNullOrWhiteSpace(emailSettings.Password);

                NetworkCredential credential = !useDefaultCredentials
                    ? new NetworkCredential(emailSettings.Login, emailSettings.Password)
                    : null;

                SmtpClient smtpClient = new()
                {
                    Host = emailSettings.Host,
                    Port = emailSettings.Port,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = credential,
                    EnableSsl = emailSettings.EnableSsl
                };

                return smtpClient;
            });

            return services;
        }
    }
}
