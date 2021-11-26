using System.Collections.Generic;
using System.Linq;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Status:Enumeration
    {
        public static Status Processing = new(10, nameof(Processing));
        public static Status Waiting = new(20, nameof(Waiting));
        public static Status Issued = new(30, nameof(Issued));

        public Status(int id, string name) : base(id, name)
        {
            
        }
       
        public static Status FromId(int StatusId)
        {
            return List().Single(r => int.Equals(r.Id, StatusId));
        }
        
        
        public static IEnumerable<Status> List()
        {
            // alternately, use a dictionary keyed by value
            return new[]{Processing,Waiting,Issued};
        }
    }
}