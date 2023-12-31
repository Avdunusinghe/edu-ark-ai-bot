using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.AuthenticationDTOs;
using EduArk.Application.Pipelines.Authentications.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
       
        public AuthenticationController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this._mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticationDTO model)
        {
            
            var response = await _mediator.Send(new AuthenticationCommand(model));

            return Ok(response);
        }

        


    }
}
