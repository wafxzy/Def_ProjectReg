using CommonReg.Common.Models;
using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountEntity>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertUser(AccountEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertUserForgotPass(UserForgotPasswordEntity item)
        {
            throw new NotImplementedException();
        }

        public Task<int> SetInactiveUserForgotPass(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateActiveStatusById(Guid id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePassword(Guid userId, Guid passwordSalt, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUser(AccountEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
