using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataBaseContext _context;
     
        public UnitOfWork(IDataBaseContext context, IUserRepository userRepository,
            IUserSessionRepository userSessionRepository, IUserRoleRepository userRoleRepository)
            
        {
            _context = context;
            UserRepository = userRepository;
            UserSessionRepository = userSessionRepository;
            UserRoleRepository = userRoleRepository;
        }
        public void Commit()
        {
            _context.Commit();
        }
        public IUserRepository UserRepository { get; set; }
        public IUserSessionRepository UserSessionRepository { get; set; }
        public IUserRoleRepository UserRoleRepository { get; set; }
    }
}
