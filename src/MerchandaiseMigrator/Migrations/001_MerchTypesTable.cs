using FluentMigrator;

namespace MerchandaiseMigrator.Migrations
{
    [Migration(1)]
    public class MerchTypesTable:Migration 
    {
        
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merchTypes(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL);
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists merchTypes;");
        }
    }
}