using CommonProject.DAL.Queries;
using CommonReg.Common.Models;
using CommonReg.DAL.Repositories.Interfaces;
using Dapper;

namespace CommonReg.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBaseContext _dataBaseContext;

        public UserRepository(IDataBaseContext dataBaseContext) {
            _dataBaseContext = dataBaseContext;
        }

        public Task DeleteUser(Guid userId)
        {
            return _dataBaseContext.Connection.ExecuteAsync(
              UserQueries.DELETE_USER,
                new { Id = userId },transaction: _dataBaseContext.Transaction
            );
        }

        public Task<IEnumerable<AccountEntity>> GetAllUsers()
        {
            return _dataBaseContext.Connection.QueryAsync<AccountEntity>(
                         UserQueries.GET_ALL_USERS,
                         transaction: _dataBaseContext.Transaction
                     );
        }

        public Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code)
        {
            return _dataBaseContext.Connection.QueryFirstOrDefaultAsync<UserForgotPasswordEntity>(
                   UserQueries.SELECT_FORGOT_PASSWORD_BY_ID_AND_CODE,
                   new { id,  code },
                   transaction: _dataBaseContext.Transaction
               );
        }

        public Task<AccountEntity> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                return null;
            }
            email = email.ToLowerInvariant();

            return _dataBaseContext.Connection.QueryFirstOrDefaultAsync<AccountEntity>(
                UserQueries.GET_USER_BY_EMAIL,
                new { email },
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<AccountEntity> GetUserById(Guid id)
        {
            return _dataBaseContext.Connection.QueryFirstOrDefaultAsync<AccountEntity>(
                UserQueries.GET_USER_BY_ID,
                new { id },
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<int> InsertUser(AccountEntity user)
        {
           user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;
            return _dataBaseContext.Connection.ExecuteAsync(
                UserQueries.INSERT_USER,
                user,
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<int> InsertUserForgotPass(UserForgotPasswordEntity item)
        {
            return _dataBaseContext.Connection.ExecuteAsync(
                UserQueries.INSERT_USER_FORGOT_PASSWORD,
                item,
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<int> SetInactiveUserForgotPass(Guid userId)
        {
            return _dataBaseContext.Connection.ExecuteAsync(
                UserQueries.SET_INACTIVE_FORGOT_PASSWORD,
                new { UserId = userId },
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<int> UpdateActiveStatusById(Guid id, bool isActive)
        {
     var parameters = new { id, isActive };
            return _dataBaseContext.Connection.ExecuteAsync(
                UserQueries.UPDATE_ACTIVE_STATUS,
                parameters,
                transaction: _dataBaseContext.Transaction
            );
        }

        public Task<int> UpdatePassword(Guid userId, Guid passwordSalt, string passwordHash)
        {
            return _dataBaseContext.Connection.ExecuteAsync(
                 UserQueries.UPDATE_PASSWORD,
                 new { userId, passwordSalt, passwordHash, UpdatedAt = DateTime.UtcNow },
                 transaction: _dataBaseContext.Transaction
             );
        }

        public Task<int> UpdateUser(AccountEntity entity)
        {
            return _dataBaseContext.Connection.ExecuteAsync(
                     UserQueries.UPDATE_USER,
                     entity,
                     transaction: _dataBaseContext.Transaction
                 );
        }
    }
}
