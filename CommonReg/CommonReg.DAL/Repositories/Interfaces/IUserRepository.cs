using CommonReg.Common.Models;

namespace CommonReg.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<AccountEntity> GetUserByEmail(string email);
        Task<AccountEntity> GetUserById(Guid id);
        Task<IEnumerable<AccountEntity>> GetAllUsers();
        Task<int> InsertUser(AccountEntity user);
        Task<int> UpdateUser(AccountEntity entity);
        Task<int> UpdateActiveStatusById(Guid id, bool isActive);
        Task<int> InsertUserForgotPass(UserForgotPasswordEntity item);
        Task<UserForgotPasswordEntity> GetForgotUserPassByIdAndCode(Guid id, Guid code);
        Task<int> UpdatePassword(Guid userId, Guid passwordSalt, string passwordHash);
        Task<int> SetInactiveUserForgotPass(Guid userId);
        Task DeleteUser(Guid userId);

    }
}
