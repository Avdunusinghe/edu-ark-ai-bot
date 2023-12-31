using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Application.DTOs.StudentTargetSettingDTOs;
using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Application.Pipelines.StudentTargetSettings.Commads.SaveSutdentTargetSetting;
using EduArk.Application.Pipelines.StudentTargetSettings.Commads.SaveTeacherTargetScore;
using EduArk.Application.Pipelines.StudentTargetSettings.Queries.GetStudentsTargetSettingsByFilter;
using EduArk.Application.Pipelines.Subjects.Commands.SaveSubject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectTargetSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for AcademicLevelController class
        /// </summary>
        /// <param name="mediator">Mediator for handling commands and queries</param>
        public SubjectTargetSettingController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveSubjectTargetSettingConfiguration")]
        public async Task<IActionResult> SaveSubjectTargetSettingConfiguration([FromBody] StudentTargetSettingConfigureDTO dto)
        {
            var response = await _mediator.Send(new SaveSutdentTargetSettingCommand(dto));

            return Ok(response);
        }

        [HttpPost("getStudentsTargetSettingsByFilter")]
        public async Task<IActionResult> GetStudentsTargetSettingsByFilter([FromBody] GetStudentsTargetSettingsByFilterQuery getStudentsTargetSettingsByFilterQuery)
        {
            var response = await _mediator.Send(getStudentsTargetSettingsByFilterQuery);

            return Ok(response);
        }

        [HttpPost("saveTeacherTargetScore")]
        public async Task<IActionResult> SaveTeacherTargetScore([FromBody] TeacherTargetScoreContainerDTO dto)
        {
            var response = await _mediator.Send(new SaveTeacherTargetScoreCommand(dto));

            return Ok(response);
        }
    }
}
