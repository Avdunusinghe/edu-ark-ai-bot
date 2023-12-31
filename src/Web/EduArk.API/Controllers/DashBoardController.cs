using EduArk.Application.Pipelines.Dashboards.Queries.GetAdminDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashBoardController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getAdminDashboard")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var response = await _mediator.Send(new GetAdminDashboardQuery());

            return Ok(response);
        }
    }
}
