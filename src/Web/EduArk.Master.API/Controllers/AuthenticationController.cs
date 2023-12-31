using EduArk.Application.Master.DTOs.AuthenticationDTOs;
using EduArk.Application.Master.Pipelines.MasterUsers.Command.MasterUserAuthentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("masterLogin")]
        [AllowAnonymous]
        public async  Task<IActionResult> MasterLogin(AuthenticationDTO dto)
        {
            var response = await _mediator.Send(new MasterUserAuthenticationCommand(dto));
            return Ok(response);
        } 

    }
}
