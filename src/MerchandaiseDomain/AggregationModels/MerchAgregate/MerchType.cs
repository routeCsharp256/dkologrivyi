using System.Collections.Generic;
using System.Linq;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.Exceptions.MerchValidation;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchType : Enumeration
    {
        public static MerchType WelcomePack = new(10, nameof(WelcomePack));
        public static MerchType ConferenceListenerPack = new(20, nameof(ConferenceListenerPack));
        public static MerchType ConferenceSpeakerPack = new(30, nameof(ConferenceSpeakerPack));
        public static MerchType ProbationPeriodEndingPack = new(40, nameof(ProbationPeriodEndingPack));
        public static MerchType VeteranPack = new(50, nameof(VeteranPack));

        public MerchType(int id, string name) : base(id, name)
        {
        }
        
        

        private bool ValidateType(int id, string name)
        {
            if (id <= 0)
                throw new MerchTypeInvalidException("id cannot be smaller then zero ");


            if (name is null)
                throw new MerchTypeInvalidException("Name cannot be null");

            return true;
        }
        
        public static MerchType FromId(int MerchTypeId)
        {
            return List().Single(r => int.Equals(r.Id, MerchTypeId));
        }
        
        public static IEnumerable<MerchType> List()
        {
            // alternately, use a dictionary keyed by value
            return new[]{WelcomePack,ConferenceListenerPack,ConferenceSpeakerPack,ProbationPeriodEndingPack,VeteranPack};
        }
        
    }
}