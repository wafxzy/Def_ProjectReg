using CommonReg.Common.JWTToken.Models;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Services.Interfaces
{
    public interface IAccountService
    {

        Task<TokenResult> Login(LoginRequestModel user, string userAgent);
        Task<TokenResult> RefreshToken(RefreshTokenRequestModel refreshTokenRequest, string userAgent);
        Task<TokenResult> GetToken(Guid userId, string userAgent);
        Task<TokenResult> GetToken(AccountEntity accountEntity, string userAgent);
        Task<bool> Registration(RegistrationRequestModel user);
        Task<AccountEntity> SetActive(ConfirmRegistrationRequestModel model);
        Task<AccountEntity> SendConfirmationRegistrationAgain(string email);
        Task<int> ForgotPassword(AccountEntity user);
        Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code);
        Task<int> RestoreUserPassword(Guid userId, string password);
        Task DeleteSession(int sessionId, Guid userId);
        Task<int> DeleteSessionsByUserId(Guid userId);
    }
}
