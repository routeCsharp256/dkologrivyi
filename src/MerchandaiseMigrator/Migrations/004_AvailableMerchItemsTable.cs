using FluentMigrator;



namespace MerchandaiseMigrator.Migrations
{
    public class AvailableMerchItems:Migration  {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists availableMerchItems(
                    id BIGSERIAL PRIMARY KEY,
                    merchId BIGSERIAL, 
                    sku TEXT NOT NULL,
                        CONSTRAINT fk_merch
                            FOREIGN KEY (merchId)
                                REFERENCES availableMerches(id)
                                ON DELETE CASCADE
                                );
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists availableMerchItems;");
        }
    }
}