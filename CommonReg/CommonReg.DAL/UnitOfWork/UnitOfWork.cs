using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataBaseContext _databaseContext;

        public UnitOfWork(
          IDataBaseContext databaseContext,
          IUserRepository userRepository,
          IUserSessionRepository userSessionRepository,
          IUserRoleRepository userRoleRepository
          )
        {
            _databaseContext = databaseContext;
            UserRepository = userRepository;
            UserSessionRepository = userSessionRepository;
            UserRoleRepository = userRoleRepository;

            _databaseContext.BeginTransaction();
        }

        public void Commit()
        {
            _databaseContext.Commit();
        }

        public IUserRepository UserRepository { get; set; }
        public IUserSessionRepository UserSessionRepository { get; set; }
        public IUserRoleRepository UserRoleRepository { get; set; }
    }
}
