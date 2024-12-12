using System.Data;

namespace CommonReg.DAL
{
    public class DataBaseContext : IDataBaseContext
    {
        public Guid Id { get; private set; }
        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        private void Connect(string connectionString)
        {
            Connection = new Npgsql.NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public DataBaseContext(IConnectionStringResolver connectionStringResolver)
        {
            Id = Guid.NewGuid();
            Connect(connectionStringResolver.Resolve);
        }

        public void BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new InvalidOperationException("Transaction already exists");
            }

            Transaction = Connection.BeginTransaction();

        }
        public void Commit()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("Transaction does not exist");
            }

            Transaction.Commit();
        }

        public void RollBack()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("Transaction does not exist");
            }

            Transaction.Rollback();
        }

        public void Dispose()
        {

            Transaction?.Dispose();
            Connection?.Dispose();

            Transaction = null;
            Connection = null;

            GC.SuppressFinalize(this);
        }
    }
}
