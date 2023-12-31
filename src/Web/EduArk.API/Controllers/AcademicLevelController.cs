using EduArk.Application.DTOs.AcademicLevelDTOs;
using EduArk.Application.Pipelines.AcademicLevels.Commands.DeleteAcademicLevel;
using EduArk.Application.Pipelines.AcademicLevels.Commands.SaveAcademicLevel;
using EduArk.Application.Pipelines.AcademicLevels.Queries.GetAcademicLevelsByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcademicLevelController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for AcademicLevelController class
        /// </summary>
        /// <param name="mediator">Mediator for handling commands and queries</param>
        public AcademicLevelController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Endpoint for saving an academic level
        /// </summary>
        /// <param name="dto">AcademicLevelDTO containing the details of the academic level to be saved</param>
        /// <returns>Action result indicating the result of the save operation</returns>
        [HttpPost("saveAcademicLevel")]
        public async Task<IActionResult> SaveAcademicLevel([FromBody] AcademicLevelDTO dto)
        {
            var respose = await _mediator.Send(new SaveAcademicLevelCommand(dto));

            return Ok(respose);
        }

        /// <summary>
        /// Endpoint for retrieving academic levels based on filter criteria
        /// </summary>
        /// <param name="dto">AcademicLevelFilterDTO containing the filter criteria</param>
        /// <returns>Action result containing the paginated academic levels</returns>
        [HttpPost("getAcademicLevels")]
        public async Task<IActionResult> GetAcademicLevels([FromBody] AcademicLevelFilterDTO dto)
        {
            var respose = await _mediator.Send(new GetAcademicLevelsByFilterQuery(dto));

            return Ok(respose);
        }

        /// <summary>
        /// Endpoint for deleting an academic level
        /// </summary>
        /// <param name="id">Id of the academic level to be deleted</param>
        /// <returns>Action result indicating the result of the delete operation</returns>
        [HttpDelete("deleteAcademicLevel/{id}")]
        public async Task<IActionResult> DeleteAcademicLevel(int id)
        {
            var respose = await _mediator.Send(new DeleteAcademicLevelCommand(id));

            return Ok(respose);
        }
    }
}
