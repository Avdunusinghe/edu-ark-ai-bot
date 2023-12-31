using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.ClassNameDTOs;
using EduArk.Application.Pipelines.Classes.Commands.SaveClass;
using EduArk.Application.Pipelines.Classes.Queries.GetClassById;
using EduArk.Application.Pipelines.Classes.Queries.GetClassesByFilter;
using EduArk.Application.Pipelines.Classes.Queries.GetClassMasterData;
using EduArk.Application.Pipelines.Classes.Queries.GetClassSubjectsForSelectedAcademicLevel;
using EduArk.Application.Pipelines.Classes.Queries.GetTeacherClassesByFilter;
using EduArk.Application.Pipelines.ClassNames.Commands.DeleteClassName;
using EduArk.Application.Pipelines.ClassNames.Commands.SaveClassName;
using EduArk.Application.Pipelines.ClassNames.Queries.GetClassNamesByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveClassName")]
        public async Task<IActionResult> SaveClassName([FromBody] ClassNameDTO dto)
        {
            var respose = await _mediator.Send(new SaveClassNameCommand(dto));

            return Ok(respose);
        }

        [HttpDelete("deleteClassName/{id}")]
        public async Task<IActionResult> DeleteClassName(int id)
        {
            var response = await _mediator.Send(new DeleteClassNameCommand(id));

            return Ok(response);
        }

        [HttpPost("getClassNamesByFilter")]
        public async Task<IActionResult> GetSubjectsByFilter(ClassNameFilterDTO filter)
        {
            var response = await _mediator.Send(new GetClassNamesByFilterQuery(filter));

            return Ok(response);
        }

        [HttpPost("getClassesByFilter")]
        public async Task<IActionResult> GetClassesByFilter([FromBody] ClassFilterDTO filter)
        {
            var response = await _mediator.Send(new GetClassesByFilterQuery(filter));

            return Ok(response);
        }

        [HttpPost("saveClass")]
        public async Task<IActionResult> SaveClass([FromBody] ClassDTO dto)
        {
            var respose = await _mediator.Send(new SaveClassCommand(dto));

            return Ok(respose);
        }

        [HttpGet("getClassMasterData")]
        public async Task<IActionResult> GetClassMasterData()
        {
            var respose = await _mediator.Send(new GetClassMasterDataQuery());

            return Ok(respose);
        }

        [HttpPost("getClassSubjectsForSelectedAcademicLevel")]
        public async Task<IActionResult> GetClassSubjectsForSelectedAcademicLevel(GetClassSubjectsForSelectedAcademicLevelQuery query)
        {
            var respose = await _mediator.Send(query);

            return Ok(respose);
        }

        [HttpGet("getClassDetails/{academicYearId}/{academicLevelId}/{classNameId}")]
        public async Task<IActionResult> GetClassDetails(int academicYearId, int academicLevelId, int classNameId)
        {
            var respose = await _mediator.Send(new GetClassDataQuery(academicYearId, academicLevelId, classNameId));

            return Ok(respose);
        }

        [HttpPost("getTeacherClassesByFilter")]
        public async Task<IActionResult> GetTeacherClassesByFilter(GetTeacherClassesByFilterQuery teacherClassesByFilterQuery)
        {
            var respose = await _mediator.Send(teacherClassesByFilterQuery);

            return Ok(respose);
        }
    }
}
