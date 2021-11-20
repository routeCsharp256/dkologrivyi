using FluentMigrator;

namespace MerchandaiseMigrator.Migrations
{
    [Migration(7)]
    public class OrdersTable:Migration 
    {
        public override void Up()
        {
	        Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS orders
	                (orderId BIGSERIAL PRIMARY KEY,
			                employeeId BIGSERIAL,
			                orderedMerchId BIGSERIAL,
			                CONSTRAINT FK_ORDEREDMERCH
		                FOREIGN KEY (orderedMerchId) 
                        REFERENCES ORDEREDMERCHES(MERCHID) 
                        ON DELETE CASCADE,
                    	CONSTRAINT fk_employee
						FOREIGN KEY(employeeId)
				    	REFERENCES employees(employeeId));
                    ");
        }
    
        public override void Down()
        {
	        Execute.Sql(@"DROP TABLE if exists orders;");
        }
    }
}