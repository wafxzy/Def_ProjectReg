using CommonReg.Common.Helpers;
using CommonReg.Common.Models;
using CommonReg.EmailSender.Helpers;
using CommonReg.EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.EmailTemplates
{
    public class ConfirmationRegistrationEmailTemplate : BaseEmailTemplate
    {
        private readonly ConfirmationRegistrationViewModel _viewModel;
        public ConfirmationRegistrationEmailTemplate(
                    AccountEntity user,
                string supportEmail)
        {
            _viewModel = new ConfirmationRegistrationViewModel
            {
                SupportEmail = supportEmail,
                UserName = user.FirstName,
                Link = LinkGeneratorHelper.GenerateConfirmRegistrationLink(GetDomainSiteUrl(), user.Id, user.ActivationCode),
            };
        }
        protected override Task<string> GetBody()
        {
            return EmailTemplateGeneratorHelper.GenerateTemplateAsync(nameof(ConfirmationRegistrationEmailTemplate), _viewModel);
        }
   private static string EmailSubject => "Confirm your account";
        protected override string GetSubject()
        {
            return EmailSubject;
        }
     
    }
}
