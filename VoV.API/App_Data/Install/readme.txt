You can execute this .sql file to insert sample records.

Or During Migration

Add-Migration SQLScript_Migration
Inside up function write these lines

protected override void Up(MigrationBuilder migrationBuilder)
{
    var sqlFile = Path.Combine("App_Data/Install/SqlServer.StoredProcedures.sql"); 
    migrationBuilder.Sql(File.ReadAllText(sqlFile));
}

Note : Don't shift this file to other project. File path is considered only from main project hence.