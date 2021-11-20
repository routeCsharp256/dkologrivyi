using FluentMigrator;

namespace MerchandaiseMigrator.Migrations
{
    [Migration(3)]
    public class AvailableMerchesTable:Migration 
    {
        public override void Up()
        {
            Execute.Sql(@"
				CREATE TABLE if not exists availableMerches(
                    merchId BIGSERIAL PRIMARY KEY,
					name TEXT,
					merchTypeId BIGSERIAL,
					CONSTRAINT fk_merchType
					  FOREIGN KEY(merchTypeId) 
					  REFERENCES merchtypes(id));
					  ");
            
            Execute.Sql(@"
				INSERT INTO public.availablemerches(name, merchtypeid)
					VALUES ('Мерч для нового сотрудника', 10),
					('Слушатель конференции', 20),
					('Спикер на конференции', 30),
					('Прошел испытательный', 40),
					('5 лет в компании', 50);
					");
        }
    
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists availableMerches;");
        }
    }
}