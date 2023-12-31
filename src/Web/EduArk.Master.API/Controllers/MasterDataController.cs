using EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantMasterData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MasterDataController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getTenantMasterData")]
        public async Task<IActionResult> GetTenantMasterData()
        {
            var response = await _mediator.Send(new GetTenantMasterDataQuery());

            return Ok(response);
        }
    }
}
