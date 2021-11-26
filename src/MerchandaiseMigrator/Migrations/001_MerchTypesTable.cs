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
                    id INT PRIMARY KEY,
                    name TEXT NOT NULL);
                    ");
            Execute.Sql(@"
                INSERT INTO public.merchtypes(
	                id, name)
	                VALUES (10, 'WelcomePack'),
	                (20, 'ConferenceListenerPack'),
	                (30, 'ConferenceSpeakerPack'),
	                (40, 'ProbationPeriodEndingPack'),
	                (50, 'VeteranPack');
                    ");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists merchTypes;");
        }
    }
}