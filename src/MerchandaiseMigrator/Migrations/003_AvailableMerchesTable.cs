using FluentMigrator;

namespace MerchandaiseMigrator.Migrations
{
    public class ExistingMerchesTable:Migration 
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE if not exists



");
        }
    
        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}