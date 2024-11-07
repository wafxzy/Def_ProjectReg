using FluentMigrator.Runner.VersionTableInfo;

namespace CommonReg.MigrationRunner
{

    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }

        public bool OwnsSchema => false;

        public string SchemaName => string.Empty;

        public string TableName => "version_info";

        public string ColumnName => "version";

        public string DescriptionColumnName => "description";

        public string UniqueIndexName => "uc_version";

        public string AppliedOnColumnName => "applied_on";
        public bool CreateWithPrimaryKey => false;
    }   
}