using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Application.DTOs.SubjectTeachersDTOs;
using EduArk.Application.Pipelines.Subjects.Commands.DeleteSubject;
using EduArk.Application.Pipelines.Subjects.Commands.SaveSubject;
using EduArk.Application.Pipelines.Subjects.Queries.GetSubjectsByFilter;
using EduArk.Application.Pipelines.SubjectTeachers.Commands.SaveSubjectTeachers;
using EduArk.Application.Pipelines.SubjectTeachers.Queries.GetAllSubjectTeachersByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveSubject")]
        public async Task<IActionResult> SaveSubject([FromBody] SubjectDTO dto)
        {
            var respose = await _mediator.Send(new SaveSubjectCommand(dto));

            return Ok(respose);
        }

        [HttpDelete("deleteSubject/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var response = await _mediator.Send(new DeleteSubjectCommand(id));

            return Ok(response);
        }

        [HttpPost("getSubjectsByFilter")]
        public async Task<IActionResult> GetSubjectsByFilter(SubjectFilterDTO filter)
        {
            var response = await _mediator.Send(new GetSubjectsByFilterQuery(filter));

            return Ok(response);
        }

        [HttpPost("saveSubjectTeachers")]
        public async Task<IActionResult> SaveSubjectTeachers([FromBody] SubjectTeachersDTO subjectTeachersDetails)
        {
            var response = await _mediator.Send(new SaveSubjectTeachersCommand(subjectTeachersDetails));

            return Ok(response);
        }

        [HttpPost("getAllSubjectTeachersByFilter")]
        public async Task<IActionResult> GetAllSubjectTeachersByFilter([FromBody] SubjectTeacherFilterDTO filter)
        {
            var response = await _mediator.Send(new GetAllSubjectTeachersByFilterQuery(filter));

            return Ok(response);
        }

    }
}
