using CommonReg.MigrationRunner.Migrations.Queries;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.MigrationRunner.Migrations
{

    [Migration(202412011332)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Execute.Sql(InitialQueries.CREATE_EXTENSIONS);
            Execute.Sql(InitialQueries.CREATE_USERS);
            Execute.Sql(InitialQueries.CREATE_ROLE);
            Execute.Sql(InitialQueries.CREATE_USER_ROLES);
            Execute.Sql(InitialQueries.CREATE_USER_SESSIONS);
            Execute.Sql(InitialQueries.CREATE_USER_FORGOT_PASSWORD);
            Execute.Sql(InitialQueries.INSERT_ROLES_DATA);
        }

        public override void Down()
        {
            Execute.Sql(InitialQueries.DROP_TABLES);
            Execute.Sql(InitialQueries.DROP_EXTENSIONS);
        }
    }

}
