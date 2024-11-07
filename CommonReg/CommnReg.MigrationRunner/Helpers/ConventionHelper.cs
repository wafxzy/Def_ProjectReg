using FluentMigrator.Model;

namespace CommonReg.MigrationRunner.Helpers
{
    public static class ConventionHelper
    {
        public static string GetForeignKeyName(
        ForeignKeyDefinition foreignKey
        )
        {
            string keyName = $"{foreignKey.PrimaryTable}_{foreignKey.ForeignTable}_fkey";

            return keyName;
        }

        public static string GetIndexName(
            IndexDefinition index
            )
        {
            string indexColumns = string.Join('_', index.Columns.Select(column => column.Name));

            string indexName = $"{index.TableName}_{indexColumns}_idx";

            return indexName;
        }

        public static string GetPrimaryKeyName(
            string tableName
            )
        {
            string primaryKeyName = $"{tableName}_pkey";

            return primaryKeyName;
        }
    }
}
