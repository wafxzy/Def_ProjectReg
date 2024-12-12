using CommonReg.Common.Models;

namespace CommonReg.DAL.Repositories.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<UserSessionsEntity> AddSession(UserSessionsEntity entity);
        Task<UserSessionsEntity> GetSession(int sessionId);
        Task<UserSessionsEntity> UpdateSession(UserSessionsEntity entity);
        Task<int> DeleteSession(int sessionId, Guid userId);
        Task<int> DeleteSessionsByUserId(Guid userId);
    }
}
