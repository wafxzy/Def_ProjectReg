using CommonReg.BLL.Services.Interfaces;
using CommonReg.Common.JWTToken.Models;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels;
using CommonReg.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        public Task DeleteSession(int sessionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSessionsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> ForgotPassword(AccountEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> GetToken(Guid userId, string userAgent)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> GetToken(AccountEntity accountEntity, string userAgent)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> Login(LoginRequestModel user, string userAgent)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> RefreshToken(RefreshTokenRequestModel refreshTokenRequest, string userAgent)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Registration(RegistrationRequestModel user)
        {
            throw new NotImplementedException();
        }

        public Task<int> RestoreUserPassword(Guid userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> SendConfirmationRegistrationAgain(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> SetActive(ConfirmRegistrationRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
