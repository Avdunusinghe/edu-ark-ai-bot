using EduArk.Application.Master.DTOs.TenantsDTO;
using EduArk.Application.Master.Pipelines.Tenants.Commands.DeleteTenant;
using EduArk.Application.Master.Pipelines.Tenants.Commands.SaveTenant;
using EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantById;
using EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantsByFilter;
using EduArk.Application.Master.Pipelines.Tenants.Queries.ValidateTenantDomain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveTenant")]
        public async Task<IActionResult> SaveTenant([FromBody] TenantDetailsDTO tenantDetails)
        {
            var response = await _mediator.Send(new SaveTenantCommand(tenantDetails));

            return Ok(response);
        }

        [HttpPost("getTenantsByFilter")]
        public async Task<IActionResult> GetTenantsByFilter([FromBody] TenantFilterDTO filter)
        {
            var response = await _mediator.Send(new GetTenantsByFilterQuery(filter));

            return Ok(response);
        }

        [HttpGet("getTenantById/{id}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var response = await _mediator.Send(new GetTenantByIdQuery(id));

            return Ok(response);
        }

        [HttpPost("validateTenantDomain")]
        public async Task<IActionResult> ValidateTenantDomain
            ([FromBody] ValidateTenantDomainQuery validateTenantDomainQuery)
        {
            var response = await _mediator.Send(validateTenantDomainQuery);

            return Ok(response);
        }

        [HttpDelete("deleteTenant/{id}")]
        public async Task<IActionResult> DeleteTenantCommand(int id)
        {
            var response = await _mediator.Send(new DeleteTenantCommand(id));

            return Ok(response);
        }

    }
}
