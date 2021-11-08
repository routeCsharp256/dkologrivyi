using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchType:Enumeration
    {
        public static MerchType WelcomePack = new(10, nameof(WelcomePack));
        public static MerchType ConferenceListenerPack = new(20, nameof(ConferenceListenerPack));
        public static MerchType ConferenceSpeakerPack = new(30, nameof(ConferenceSpeakerPack));
        public static MerchType ProbationPeriodEndingPack  = new(40, nameof(ProbationPeriodEndingPack));
        public static MerchType VeteranPack = new(50, nameof(VeteranPack));
        
        public MerchType(int id, string name) : base(id, name)
        {
        }
    }
}