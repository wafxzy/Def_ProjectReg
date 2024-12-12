using CommonReg.DAL.Repositories.Interfaces;

namespace CommonReg.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IUserSessionRepository UserSessionRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        void Commit();
    }
}
