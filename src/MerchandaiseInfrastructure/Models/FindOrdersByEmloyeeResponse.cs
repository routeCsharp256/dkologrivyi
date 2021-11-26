using MerchandaiseDomain.AggregationModels.MerchAgregate;

namespace MerchandaiseInfrastructure.Models
{
    public class FindOrdersByEmloyeeResponse
    {
        public OrdersDb OrdersDb { get; }
        public OrderedMerchesDb OrderedMerchesDb { get; }
        public OrderedMerchItemDb OrderedMerchItemDb { get; }
        public EmployeeDb EmployeeDb { get; }
        public MerchTypeDb MerchTypeDb { get; }

        public FindOrdersByEmloyeeResponse(OrdersDb ordersDb, OrderedMerchesDb orderedMerchesDb, OrderedMerchItemDb orderedMercItemDb, EmployeeDb employeeDb, MerchTypeDb merchTypeDb)
        {
            OrdersDb = ordersDb;
            OrderedMerchesDb = orderedMerchesDb;
            OrderedMerchItemDb = orderedMercItemDb;
            EmployeeDb = employeeDb;
            MerchTypeDb = merchTypeDb;
        }
    }
}