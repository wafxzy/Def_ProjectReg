using System.Data;

namespace CommonReg.DAL
{
    public interface IDataBaseContext : IDisposable
    {
        Guid Id { get; }

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
