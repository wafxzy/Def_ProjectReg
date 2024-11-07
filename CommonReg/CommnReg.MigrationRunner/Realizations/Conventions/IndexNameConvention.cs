using CommonReg.MigrationRunner.Helpers;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;

namespace CommonReg.MigrationRunner.Realizations.Conventions
{
    public class IndexNameConvention : IIndexConvention
    {
        public IIndexExpression Apply(
            IIndexExpression expression
            )
        {
            if (string.IsNullOrEmpty(expression.Index.Name))
            {
                expression.Index.Name = ConventionHelper.GetIndexName(expression.Index);
            }

            return expression;
        }
    }
}
