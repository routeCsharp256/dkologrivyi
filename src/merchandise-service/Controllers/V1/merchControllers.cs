using System;
using System.Collections.Generic;
using System.Threading;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
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
        public IActionResult RequestMerch([FromBody] MerchandiseRequest requestModel, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getIssuedMerches")]
        public ActionResult<List<MerchandiseResponse>> GetIssuedMerches(long employeeId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}