using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.MigrationRunner.Interfaces
{
    public interface IDatabaseMigrator
    {
        void MigrateDatabase(long? version);
    }
}
