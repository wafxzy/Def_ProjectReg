using CommonReg.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.EmailTemplates
{
    public abstract class BaseEmailTemplate
    {
        public async Task<BaseEmailTemplate> GetTemplateData()
        {
            Subject = GetSubject();
            Body = await GetBody();

            return this;

        }

        protected abstract string GetSubject();
        protected abstract Task<string> GetBody();

        protected static string GetDomainSiteUrl()
        {
            return EnvironmentManager.FrontServiceUrl;
        }


        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
