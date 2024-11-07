using CommonReg.MigrationRunner.Helpers;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;

namespace CommonReg.MigrationRunner.Realizations.Conventions
{
    public class ConstraintConvention : IConstraintConvention
    {
        public IConstraintExpression Apply(
            IConstraintExpression expression
            )
        {
            if (expression.Constraint.IsPrimaryKeyConstraint && string.IsNullOrEmpty(expression.Constraint.ConstraintName))
            {
                expression.Constraint.ConstraintName = ConventionHelper.GetPrimaryKeyName(expression.Constraint.TableName);
            }

            return expression;
        }
    }
}
