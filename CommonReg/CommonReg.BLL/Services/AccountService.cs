using CommonReg.BLL.Helpers;
using CommonReg.BLL.Services.Interfaces;
using CommonReg.Common;
using CommonReg.Common.Helpers;
using CommonReg.Common.JWTToken.Models;
using CommonReg.Common.JWTToken.Options;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels;
using CommonReg.Common.Utils;
using CommonReg.DAL.UnitOfWork;
using CommonReg.EmailSender.EmailTemplates;
using CommonReg.EmailSender.Models;
using CommonReg.EmailSender.Services.Interfaces;
using Microsoft.Extensions.Options;
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
        private readonly IEmailQueueService _emailQueueService;
        private readonly EmailOptions _emailOptions;

        public AccountService(IUnitOfWork unitOfWork,
            IEmailQueueService emailQueueService,
             IOptions<EmailOptions> emailOtions)
        {
            _emailOptions = emailOtions.Value;
            _unitOfWork = unitOfWork;
            _emailQueueService = emailQueueService;
        }
        public async Task DeleteSession(int sessionId, Guid userId)
        {
            await _unitOfWork.UserSessionRepository.DeleteSession(sessionId, userId);
            _unitOfWork.Commit();
        }

        public async Task<int> DeleteSessionsByUserId(Guid userId)
        {
            int result = await _unitOfWork.UserSessionRepository.DeleteSessionsByUserId(userId);
            _unitOfWork.Commit();
            return result;
        }

        public async Task<int> ForgotPassword(AccountEntity user)
        {
            Guid code = Guid.NewGuid();
            UserForgotPasswordEntity userForgotPassword = new UserForgotPasswordEntity
            {
                UserId = user.Id,
                Code = code,
                CreatedDate = DateTime.Now
            };
            int result = await _unitOfWork.UserRepository.InsertUserForgotPass(userForgotPassword);
            _unitOfWork.Commit();

            if (result > 0)
            {

                await SendForgotPasswordEmail(user.Id, code, user.Email);
            }
            return result;
        }

        private Task SendForgotPasswordEmail(
          Guid userId,
          Guid code,
          string email
          )
        {
            EmailEnvelopeModel emailEnvelope = new()
            {
                ToEmail = email,
                FromEmail = _emailOptions.InfoEmail,
                EmailTemplate = new ForgotPasswordEmailTemplate(userId, code, _emailOptions, email)
            };

            return _emailQueueService.PushAsync(emailEnvelope);
        }


        public Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code)
        {
            return _unitOfWork.UserRepository.GetForgotUserPassByIdAndCode(id, code);
        }

        public Task<TokenResult> GetToken(Guid userId, string userAgent)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResult> GetToken(AccountEntity accountEntity, string userAgent)
        {

            if (accountEntity == null)
            {
                return TokenResultHelper.ExpiredAccessToken;
            }

            if (!accountEntity.IsActive)
            {
                return TokenResultHelper.ActivationRequired;
            }

            List<string> userRoles = (await _unitOfWork.UserRoleRepository.GetUserRoles(accountEntity.Id)).Select(x => x.RoleName).ToList();

            List<int> userPermissions = (await _unitOfWork.UserRoleRepository.GetUserRolePermissions(accountEntity.Id)).ToList();

            Guid refreshToken = Guid.NewGuid();
            DateTime currentTime = DateTime.UtcNow;

            UserSessionsEntity session = await _unitOfWork.UserSessionRepository
                .AddSession(
                    new UserSessionsEntity
                    {
                        SessionId = 0,
                        UserId = accountEntity.Id,
                        RefreshToken = refreshToken,
                        CreatedDate = currentTime,
                        UpdatedDate = currentTime,
                        ExpireRefreshDate = currentTime.AddMinutes(AuthOptions.TOKEN_LIFE_TIME_MINUTES),
                        ExpireDate = currentTime.AddDays(AuthOptions.SESSION_LIFE_TIME_DAYS),
                        UserAgent = userAgent.Truncate(1024),
                    });

            _unitOfWork.Commit();

            TokenResult tokenResult = TokenResultHelper.InternalGenerateToken(session.SessionId, accountEntity.Id, refreshToken,
                session.ExpireRefreshDate, accountEntity.Email, userRoles, userPermissions);

            return tokenResult;
        }

        public async Task<TokenResult> Login(LoginRequestModel user, string userAgent)
        {
            AccountEntity accountEntity = GetUserByLoginAndPassword(user.Email, user.Password).Result;
            if (accountEntity == null)
            {
                return TokenResultHelper.InvalidEmailOrPassword;
            }
            return await GetToken(accountEntity, userAgent);
        }

        public Task<TokenResult> RefreshToken(RefreshTokenRequestModel refreshTokenRequest, string userAgent)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Registration(RegistrationRequestModel user)
        {
            AccountEntity existedUser = await _unitOfWork.UserRepository.GetUserByEmail(user.Email.ToLowerInvariant());

            if (existedUser != null) return false;

            int roleId = (int)UserRole.User;

            Guid passwordSalt = Guid.NewGuid();
            string passwordHash = HashPasswordHelper.CalculatePasswordHash(user.Password, passwordSalt);

            Guid userId = Guid.NewGuid();
            Guid activationCode = Guid.NewGuid();
            AccountEntity accountEntity = new AccountEntity
            {
                Id = userId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                ActivationCode = activationCode,
                IsActive = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Age = int.Parse(user.Age),
            };
            await _unitOfWork.UserRepository.InsertUser(accountEntity);
            await _unitOfWork.UserRoleRepository.InsertUserRole(userId, roleId);
            _unitOfWork.Commit();
            await SendConfirmationRegistrationEmail(accountEntity);
            return true;
        }
       

        public Task<int> RestoreUserPassword(Guid userId, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountEntity> SendConfirmationRegistrationAgain(string email)
        {
            AccountEntity existedUser = await _unitOfWork.UserRepository.GetUserByEmail(email.ToLowerInvariant());

            if (existedUser is { IsActive: false })
            {
                await SendConfirmationRegistrationEmail(existedUser);
            }

            return existedUser;
        }

        public Task<AccountEntity> SetActive(ConfirmRegistrationRequestModel model)
        {
            throw new NotImplementedException();
        }
    
        private async Task<AccountEntity> GetUserByLoginAndPassword(string email, string password)
        {
            AccountEntity result = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (result == null) return null;

            string passwordHash = HashPasswordHelper.CalculatePasswordHash(password, result.PasswordSalt);

            return string.Equals(passwordHash, result.PasswordHash, StringComparison.Ordinal) ? result : null;

        }

        private Task SendConfirmationRegistrationEmail(
     AccountEntity entity
     )
        {
            EmailEnvelopeModel email = new()
            {
                ToEmail = entity.Email,
                FromEmail = _emailOptions.InfoEmail,
                EmailTemplate = new ConfirmationRegistrationEmailTemplate(
                    entity,
                    _emailOptions.SupportEmail
                    )
            };

            return _emailQueueService.PushAsync(email);
        }
    }
}