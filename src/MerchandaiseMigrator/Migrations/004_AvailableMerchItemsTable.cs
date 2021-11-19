using FluentMigrator;
namespace MerchandaiseMigrator.Migrations
{
    [Migration(4)]
    public class AvailableMerchItems:Migration  {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS availableMerchItems
	                (id BIGSERIAL PRIMARY KEY,
			                skuId BIGSERIAL,
			                quantity INT, 
			                availableMerchId BIGSERIAL,
			                CONSTRAINT FK_AVAILABLEMERCH
		                FOREIGN KEY (availableMerchId) 
		                REFERENCES availableMerches(MERCHID) 
		                ON DELETE CASCADE);
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists availableMerchItems;");
        }
    }
}