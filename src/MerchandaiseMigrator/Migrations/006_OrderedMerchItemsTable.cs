using FluentMigrator;


namespace MerchandaiseMigrator.Migrations
{
    [Migration(6)]
    public class OrderedMerchItemsTable:Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS orderedMerchItems
	                (id BIGSERIAL PRIMARY KEY,
			                skuId BIGSERIAL,
			                quantity INT, 
			                orderedMerchId BIGSERIAL,
			                CONSTRAINT FK_ORDEREDMERCH
		                FOREIGN KEY (orderedMerchId) 
                        REFERENCES ORDEREDMERCHES(MERCHID) 
                        ON DELETE CASCADE);
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists orderedMerchItems;");
        }
    }
}