namespace MerchandaiseDomain.Models
{
    public enum EmployeeEventType
    {
        /// <summary>
        /// Устройство сотрудника на работу (найм).
        /// </summary>
        Hiring = 100,
    
        /// <summary>
        /// Окончание испытательного срока
        /// </summary>
        ProbationPeriodEnding = 200,
    
        /// <summary>
        /// Событие отправки сотрудника на конференцию
        /// </summary>
        ConferenceAttendance = 300,
    
        /// <summary>
        /// Выдача мерча.
        /// </summary>
        MerchDelivery = 400,
    
        /// <summary>
        /// Увольнение сотрудника.
        /// </summary>
        Dismissal = 1000
    }
}