using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Helpers
{
    public static class LinkGeneratorHelper
    {
        public static string GenerateConfirmRegistrationLink(string domaineSiteUrl
            ,Guid userId
            ,Guid secretCode)
        {
            return $"{EnvironmentManager.FrontServiceUrl}";
        }
        
        public static string GenerateResetPasswordLink(
         string domainSiteUrl,
         Guid userId,
         Guid secretCode
         )
        {
            return $"{EnvironmentManager.FrontServiceUrl}/reset-password?uuid={userId}&code={secretCode}";
        }
    }
}
