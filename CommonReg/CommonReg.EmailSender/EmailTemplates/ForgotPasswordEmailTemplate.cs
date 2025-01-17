using CommonReg.Common.Helpers;
using CommonReg.EmailSender.Helpers;
using CommonReg.EmailSender.Models;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.EmailTemplates
{
    public class ForgotPasswordEmailTemplate : BaseEmailTemplate
    {
        private readonly ForgotPasswordViewModel _viewModel;

        public ForgotPasswordEmailTemplate(
            Guid userId,
            Guid code,
            EmailOptions emailOptions,
            string userEmail
            )
        {
            _viewModel = new ForgotPasswordViewModel
            {
                EmailReceiver = userEmail,
                HelpEmail = emailOptions.Help,
                Link = LinkGeneratorHelper.GenerateResetPasswordLink(GetDomainSiteUrl(), userId, code),
            };
        }

        protected override Task<string> GetBody()
        {
            return EmailTemplateGeneratorHelper.GenerateTemplateAsync(nameof(ForgotPasswordEmailTemplate), _viewModel);
        }

        protected override string GetSubject()
        {
            return SubjectEmail;
        }

        private static string SubjectEmail => "Forgot Password";
    }
}
