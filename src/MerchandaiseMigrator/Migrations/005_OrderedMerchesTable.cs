using FluentMigrator;


namespace MerchandaiseMigrator.Migrations
{
	[Migration(5)]
    public class OrderedMerchesTable:Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
				CREATE TABLE if not exists orderedMerches(
                    merchId BIGSERIAL PRIMARY KEY,
					name TEXT,
					merchTypeId BIGSERIAL,
					statusId INT,
					requestDate DATE,
					CONSTRAINT fk_merchType
					  FOREIGN KEY(merchTypeId) 
					  REFERENCES merchtypes(id));
");
        }

        public override void Down()
        {
	        Execute.Sql(@"DROP TABLE if exists orderedMerches;");
        }
    }
}