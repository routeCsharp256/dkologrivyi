using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Models;
using MerchandaiseDomainServices.Interfaces;
using MerchandiseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merch")]
    public class merchControllers : ControllerBase
    {
        private readonly IMerchService _merchService;

        public merchControllers(IMerchService merchService)
        {
            _merchService = merchService;
        }

        [HttpPost("requestMerch")]
        public async Task<IActionResult> RequestMerch([FromBody] MerchandiseRequest requestModel,
            CancellationToken token)
        {
            try
            {
                await _merchService.RequestMerch(requestModel.Email, requestModel.MerchId, token);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getAvailableMerchList")]
        public async Task<List<Merch>> GetAvailableMerchList(CancellationToken token)
        {
            return await _merchService.GetAvailableMerchList(token);
        }

        [HttpGet("checkWasIssued")]
        public async Task<ActionResult> CheckWasIssued(
            string employeeEmail, int merchTypeId, CancellationToken token)
        {
            try
            {
                await _merchService.CheckWasIssued(employeeEmail, merchTypeId, token);
                return Ok("Merch was not issued yet");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}