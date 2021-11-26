using FluentMigrator;

namespace MerchandaiseMigrator.Migrations
{
    [Migration(2)]
    public class EmployeesTable :Migration 
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists employees(
                    employeeId BIGSERIAL PRIMARY KEY,
					firstName TEXT,
					middleName TEXT,
					lastName TEXT,
					email TEXT NOT NULL);
");
        }
    
        public override void Down()             
        {
            Execute.Sql(@"DROP TABLE if exists employees;");
        }
    }
}