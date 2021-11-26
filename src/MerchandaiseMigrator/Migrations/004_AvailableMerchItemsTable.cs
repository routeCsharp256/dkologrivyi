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
            Execute.Sql(@"
				INSERT INTO public.availablemerchitems(
					skuid, quantity, availablemerchid)
					VALUES (7, 2, 1),
					(4, 1, 1),
					(3, 3, 1),
					(5, 1, 1),
					(7, 2, 2),
					(2, 1, 2),
					(16, 3, 2),
					(51, 1, 2),
					(31, 1, 3),
					(42, 1, 3),
					(3, 3, 3),
					(5, 2, 3),
					(73, 1, 4),
					(46, 1, 4),
					(13, 2, 5),
					(45, 1, 5);
					");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists availableMerchItems;");
        }
    }
}