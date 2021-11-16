using MerchandaiseDomain.AggregationModels.MerchAgregate;

namespace MerchandaiseDomain.Models
{
    public class NotificationEvent
    {
        /// <summary>
        /// Email сотрудника.
        /// </summary>
        public string EmployeeEmail { get; set; }
    
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string EmployeeName { get; set; }
    
        /// <summary>
        /// Тип события.
        /// </summary>
        public EmployeeEventType EventType { get; set; }
    
        /// <summary>
        /// Дополнительные данные по событию.
        /// </summary>
        public object? Payload { get; set; }
    }

    public class MerchDeliveryEventPayload
    {
        /// <summary>
        /// Тип выдаваемого мерча.
        /// </summary>
        public MerchType MerchType { get; set; }
    }
}