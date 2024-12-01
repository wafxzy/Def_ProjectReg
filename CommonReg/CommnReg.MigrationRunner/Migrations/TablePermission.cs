using CommonReg.Common.Helpers;
using CommonReg.MigrationRunner.Helpers;
using FluentMigrator;


namespace CommonReg.MigrationRunner.Migrations
{

    [Migration(202412011340)]
    public class TablePermission : Migration
    {

        private void SeedData()
        {
            Insert.IntoTable(TableName.Permission)
                 .Row(new
                 {
                     system_name = nameof(UserPermission.ViewUsers),
                     description = "Can View Users ",
                 })
                 .Row(new
                 {
                     system_name = nameof(UserPermission.ModifyUser),
                     description = "Can Modify Users ",
                 })
                .Row(new
                {
                    system_name = nameof(UserPermission.ViewUserRoles),
                    description = "Can View User Roles",
                })
                .Row(new
                {
                    system_name = nameof(UserPermission.ModifyUserRoles),
                    description = "Can Modify User Roles",
                })
                .Row(new
                {
                    system_name = nameof(UserPermission.DeleteUser),
                    description = "Can Delete User",
                });



            Insert.IntoTable(TableName.RolePermission)
                 .Row(new
                 {
                     role_id = 2,
                     permission_id = (int)UserPermission.DeleteUser
                 })
                 .Row(new
                 {
                     role_id = 2,
                     permission_id = (int)UserPermission.ModifyUserRoles
                 })
                 .Row(new
                 {
                     role_id = 2,
                     permission_id = (int)UserPermission.ViewUserRoles
                 })
                 .Row(new
                 {
                     role_id = 2,
                     permission_id = (int)UserPermission.ViewUsers
                 })
                 .Row(new
                 {
                     role_id = 2,
                     permission_id = (int)UserPermission.ModifyUser
                 })
                 ;
        }

        public override void Up()
        {
            Create.Table(TableName.Permission)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("system_name").AsString(120).NotNullable()
                .WithColumn("description").AsString(250).NotNullable();

            Create.Table(TableName.RolePermission)
              .WithColumn("role_id").AsInt32().NotNullable()
              .WithColumn("permission_id").AsInt32().NotNullable();

            SeedData();
        }


        public override void Down()
        {
            Delete.Table(TableName.RolePermission);
            Delete.Table(TableName.Permission);
        }
    }

}
