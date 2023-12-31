using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Application.Pipelines.Assessments.Commands.DeleteAssessments;
using EduArk.Application.Pipelines.Assessments.Commands.GetByIdAssessments;
using EduArk.Application.Pipelines.Assessments.Commands.SaveAssessments;
using EduArk.Application.Pipelines.Assessments.Queries.GetAssessments;
using EduArk.Application.Pipelines.Assessments.Queries.UpdateAssessments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssessmentController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        //save - post method
        [HttpPost("saveAssessments")]
        public async Task<IActionResult> SaveAssessments([FromBody] AssessmentsDTO dto)
        {
            var response = await _mediator.Send(new SaveAssessmentsCommand(dto));
            return Ok(response);
        }

        //getall
        [HttpGet("getAssessments")]
        public async Task<IActionResult> GetAssessments()
        {
            var response = await _mediator.Send(new GetAllAssessmentsQuery());
            return Ok(response);
        }

        // get by id
        [HttpGet("getByIdAssessments/{id}")]
        public async Task<IActionResult> GetByIDAssessments(int id)
        {
            var response = await _mediator.Send(new GetByIdAssessmentQuery(id));
            return Ok(response);
        }

        //delete method
        [HttpDelete("deleteAssessments/{id}")]
        public async Task<IActionResult> DeleteLearningPlan(int id)
        {
            var response = await _mediator.Send(new DeleteAssessmentsCommand(id));
            return Ok(response);
        }

        // update method
        [HttpPut("updateAssessments/{id}")]
        public async Task<IActionResult> UpdateAssessments(AssessmentsDTO assessmentsDTO)
        {
            var response = await _mediator.Send(new UpdateAssessmentsCommand(assessmentsDTO));
            return Ok(response);
        }
    }

    


}
