using CommonReg.MigrationRunner.Helpers;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;


namespace CommonReg.MigrationRunner.Realizations.Conventionsэ;
public class ForeignKeyNameConvention : IForeignKeyConvention
{
    public IForeignKeyExpression Apply(
        IForeignKeyExpression expression
        )
    {
        if (string.IsNullOrEmpty(expression.ForeignKey.Name))
        {
            expression.ForeignKey.Name = ConventionHelper.GetForeignKeyName(expression.ForeignKey);
        }

        return expression;
    }
}
