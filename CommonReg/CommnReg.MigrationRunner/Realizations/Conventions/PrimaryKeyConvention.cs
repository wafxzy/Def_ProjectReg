using CommonReg.MigrationRunner.Helpers;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Conventions;

namespace CommonReg.MigrationRunner.Realizations.Conventions
{

    public class PrimaryKeyConvention : IColumnsConvention
    {
        public IColumnsExpression Apply(
            IColumnsExpression expression
            )
        {
            ColumnDefinition columnDefinition = expression.Columns.FirstOrDefault(column => column.IsPrimaryKey && string.IsNullOrEmpty(column.PrimaryKeyName));

            if (columnDefinition != null)
            {
                string tableName = string.IsNullOrEmpty(columnDefinition.TableName)
                    ? expression.TableName
                    : columnDefinition.TableName;
                columnDefinition.PrimaryKeyName = ConventionHelper.GetPrimaryKeyName(tableName);
            }

            return expression;
        }
    }
}