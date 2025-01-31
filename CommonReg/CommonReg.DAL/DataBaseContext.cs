using Npgsql;
using System.Data;

namespace CommonReg.DAL
{
    public class DataBaseContext : IDataBaseContext
    {
        public Guid Id { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public IDbConnection Connection { get; private set; }

        public DataBaseContext(IConnectionStringResolver connectionStringResolver)
        {
            Id = Guid.NewGuid();
            Connect(connectionStringResolver.Resolve);
        }

        private void Connect(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void Commit()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("There is no opened transaction");
            }

            Transaction.Commit();
        }

        public void BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new InvalidOperationException("More than one transaction cannot be created");
            }

            Transaction = Connection.BeginTransaction();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();

            Transaction = null;
            Connection = null;

            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("There is no opened transaction");
            }

            Transaction.Rollback();
        }
    }
}