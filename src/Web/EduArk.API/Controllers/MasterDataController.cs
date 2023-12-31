using EduArk.Application.DTOs.UserDTOs;
using EduArk.Application.Pipelines.Classes.Queries.GetTeacherClassesMasterData;
using EduArk.Application.Pipelines.Common.Queries;
using EduArk.Application.Pipelines.Exams.Queries.GetExamMasterData;
using EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterData;
using EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterDataByAcademicLevelId;
using EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterDataByFilter;
using EduArk.Application.Pipelines.Subjects.Queries.GetSubjectsMasterDataByAcademicLevelId;
using EduArk.Application.Pipelines.SubjectStreams.Queries;
using EduArk.Application.Pipelines.Users.Queries.GetLevelHeadsByFilter;
using EduArk.Application.Pipelines.Users.Queries.GetUserDetailMasterDataByFilter;
using EduArk.Application.Pipelines.Users.Queries.GetUserMasterData;
using EduArk.Domain.Entities.Tenant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterDataController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MasterDataController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getUserMasterData")]
        public async Task<IActionResult> GetUserMasterData()
        {
            var response = await _mediator.Send(new GetUserMasterDataQuery());

            return Ok(response);
        }

        [HttpPost("getLevelHeadsByFilter")]
        public async Task<IActionResult> GetLevelHeadsByFilter(GetLevelHeadsByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("getSubjectStreamsMasterData")]
        public async Task<IActionResult> GetSubjectStreamsMasterData()
        {
            var response = await _mediator.Send(new GetSubjectStreamsMasterDataQuery());

            return Ok(response);
        }

        [HttpGet("getSubjectMasterData")]
        public async Task<IActionResult> GetSubjectMasterDataQuery()
        {
            var response = await _mediator.Send(new GetSubjectMasterDataQuery());

            return Ok(response);
        }

        [HttpGet("getBaseAcademicMasterData")]
        public async Task<IActionResult> GetBaseAcademicMasterData()
        {
            var response = await _mediator.Send(new GetBaseAcademicMasterDataQuery());

            return Ok(response);
        }

        
        [HttpGet("getSubjectMasterDataByAcademicLevelId/{academicLevelId}")]
        public async Task<IActionResult> GetSubjectMasterDataByAcademivLevelIdQuery(int academicLevelId)
        {
            var response = await _mediator.Send(new GetSubjectMasterDataByAcademicLevelIdQuery(academicLevelId));

            return Ok(response);
        }

        [HttpPost("getUserDetailMasterDataByFilter")]
        public async Task<IActionResult> GetUserDetailMasterDataByFilterQuery([FromBody]UserDetailsMasterDataFilterDTO filter)
        {
            var response = await _mediator.Send(new GetUserDetailMasterDataByFilterQuery(filter));

            return Ok(response);
        }

        [HttpPost("getSubjectMasterDataByFilter")]
        public async Task<IActionResult> GetSubjectMasterDataByFilter(GetSubjectMasterDataByFilterQuery subjectMasterDataByFilterQuery)
        {
            var response = await _mediator.Send(subjectMasterDataByFilterQuery);

            return Ok(response);
        }

        [HttpGet("getTeacherClassesMasterData")]
        public async Task<IActionResult> GetTeacherClassesMasterData()
        {
            var response = await _mediator.Send(new GetTeacherClassesMasterDataQuery());

            return Ok(response);
        }

        [HttpGet("getSubjectsMasterDataByAcademicLevelId/{academicLevelId}")]
        public async Task<IActionResult> GetSubjectsMasterDataByAcademicLevelId(int academicLevelId)
        {
            var response = await _mediator.Send(new GetSubjectsMasterDataByAcademicLevelIdQuery(academicLevelId));

            return Ok(response);
        }

        [HttpPost("getExamMasterData")]
        public async Task<IActionResult> GetExamMasterData(GetExamMasterDataQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
