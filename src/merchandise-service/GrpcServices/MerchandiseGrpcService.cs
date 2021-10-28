using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MerchandiseService.Grpc;
using MerchandiseService.Services.Interfaces;

namespace MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService:MerchandiseGrpc.MerchandiseGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseGrpcService(IMerchService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<Empty> RequestMerch(MerchandiseRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<GetIssuedMerchesResponse> GetIssuedMerch(GetIssuedMerchesRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}