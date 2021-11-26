using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MerchandaiseDomainServices.Interfaces;
using MerchandiseService.Grpc;

namespace MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseGrpc.MerchandiseGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseGrpcService(IMerchService merchService)
        {
            _merchService = merchService;
        }


        public override Task<MerchandiseRequestResult> RequestMerch(MerchandiseRequest request,
            ServerCallContext context)
        {
            return Task.Run(() => new MerchandiseRequestResult() {Result = "test"});
        }


        public override Task<GetIssuedMerchesResponse> GetIssuedMerch(GetIssuedMerchesRequest request,
            ServerCallContext context)
        {
            return Task.Run(() => new GetIssuedMerchesResponse() {Response = "test2"});
        }
    }
}