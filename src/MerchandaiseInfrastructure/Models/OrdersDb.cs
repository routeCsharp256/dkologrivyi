namespace MerchandaiseInfrastructure.Models
{
    public class OrdersDb
    {
        public long OrderId { get; set; }
        public long EmployeeId { get; set; }
        public long OrderedMerchId { get; set; }

        public OrdersDb(long orderId, long employeeId, long orderedMerchId)
        {
            OrderId = orderId;
            EmployeeId = employeeId;
            OrderedMerchId = orderedMerchId;
        }
            
    }
}