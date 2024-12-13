using CommonReg.Common.Models;
using CommonReg.DAL.Queries;
using CommonReg.DAL.Repositories.Interfaces;
using Dapper;

namespace CommonReg.DAL.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IDataBaseContext _context;
        public UserSessionRepository(IDataBaseContext context)
        {
            _context = context;
        }
        public Task<UserSessionsEntity> AddSession(UserSessionsEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSession(int sessionId, Guid userId)
        {
            return _context.Connection.ExecuteAsync(UserSessionQueries.DELETE_SESSION_BY_ID,
                 new { sessionId, userId }, transaction: _context.Transaction);
        }

        public Task<int> DeleteSessionsByUserId(Guid userId)
        {
            return _context.Connection.ExecuteAsync(UserSessionQueries.DELETE_SESSIONS_BY_USER_ID,
                 new { userId }, transaction: _context.Transaction);
        }

        public Task<UserSessionsEntity> GetSession(int sessionId)
        {
            if (sessionId < 1)
            {
                return null;
            }
            return _context.Connection.QueryFirstOrDefaultAsync<UserSessionsEntity>(UserSessionQueries.GET_SESSION_BY_ID,
                new { sessionId }, transaction: _context.Transaction);
        }

        public Task<UserSessionsEntity> UpdateSession(UserSessionsEntity entity)
        {
            if(entity.SessionId <= 0)
            {
                throw new ArgumentException("SessionId must be greater than 0");
            }
            if(string.IsNullOrWhiteSpace(entity.UserAgent))
            {
                entity.UserAgent = string.Empty;
            }
            entity.UpdatedDate = DateTime.UtcNow;

            return _context.Connection.QueryFirstOrDefaultAsync<UserSessionsEntity>(UserSessionQueries.UPDATE_SESSION,
               entity, transaction: _context.Transaction);
        }
    }
}
