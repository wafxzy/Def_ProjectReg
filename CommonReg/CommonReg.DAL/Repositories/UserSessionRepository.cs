using CommonReg.Common.Models;
using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        public Task<UserSessionsEntity> AddSession(UserSessionsEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSession(int sessionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSessionsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserSessionsEntity> GetSession(int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<UserSessionsEntity> UpdateSession(UserSessionsEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
