using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.JWTToken.Models
{
    public class TokenResult
    {
        public TokenResponse TokenModel { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
        public Boolean Success => string.IsNullOrEmpty(Error);

    }
}
