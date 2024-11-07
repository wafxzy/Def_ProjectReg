using CommonReg.MigrationRunner.Realizations.Conventionsэ;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner;

namespace CommonReg.MigrationRunner.Realizations.Conventions
{

    public class NamingConventionSet : IConventionSet
    {
        public NamingConventionSet()
            : this(
                  new DefaultConventionSet(),
                  new ForeignKeyNameConvention(),
                  new IndexNameConvention(),
                  new PrimaryKeyConvention(),
                  new ConstraintConvention()
                  )
        {
        }

        public NamingConventionSet(
            IConventionSet innerConventionSet,
            ForeignKeyNameConvention foreignKeyConvention,
            IndexNameConvention indexConvention,
            PrimaryKeyConvention primaryKeyConvention,
            ConstraintConvention constraintConvention
            )
        {
            ForeignKeyConventions = new List<IForeignKeyConvention>
        {
                foreignKeyConvention,
                innerConventionSet.SchemaConvention,
            };

            IndexConventions = new List<IIndexConvention>
            {
                indexConvention,
                innerConventionSet.SchemaConvention,
            };

            ColumnsConventions = new List<IColumnsConvention>
            {
                primaryKeyConvention
            };

            ConstraintConventions = new List<IConstraintConvention>
            {
                constraintConvention,
                innerConventionSet.SchemaConvention,
            };

            SequenceConventions = innerConventionSet.SequenceConventions;
            AutoNameConventions = innerConventionSet.AutoNameConventions;
            SchemaConvention = innerConventionSet.SchemaConvention;
            RootPathConvention = innerConventionSet.RootPathConvention;
        }

        public IRootPathConvention RootPathConvention { get; }

        public DefaultSchemaConvention SchemaConvention { get; }

        public IList<IColumnsConvention> ColumnsConventions { get; }

        public IList<IConstraintConvention> ConstraintConventions { get; }

        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }

        public IList<IIndexConvention> IndexConventions { get; }

        public IList<ISequenceConvention> SequenceConventions { get; }

        public IList<IAutoNameConvention> AutoNameConventions { get; }
    }
}
