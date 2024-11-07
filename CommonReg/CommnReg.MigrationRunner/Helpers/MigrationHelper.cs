using CommonReg.MigrationRunner.Migrations;
using System.Reflection;

namespace CommonReg.MigrationRunner.Helpers
{
    internal static class MigrationHelper
    {
        public static Assembly MigrationAssembly => typeof(DatabaseMigrationAssemblyMarker).Assembly;
    }
}