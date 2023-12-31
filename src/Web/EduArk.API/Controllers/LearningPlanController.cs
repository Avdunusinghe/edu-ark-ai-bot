using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Application.Pipelines.LearningPlans.Commands.DeleteLearningPlan;
using EduArk.Application.Pipelines.LearningPlans.Commands.SaveLearningPlan;
using EduArk.Application.Pipelines.LearningPlans.Queries.GetByIdLearningPlan;
using EduArk.Application.Pipelines.LearningPlans.Queries.GetLearningPlan;
using EduArk.Application.Pipelines.LearningPlans.Queries.UpdateLearningPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LearningPlanController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        //getall
        [HttpGet("getLearningPlan")]
        public async Task<IActionResult> GetLearningPlan()
        {
            var response = await _mediator.Send(new GetAllLearningPlanQuery());
            return Ok(response);
        }

        // get by id
        [HttpGet("getByIdLearningPlan/{id}")]
        public async Task<IActionResult> GetByIDLearningPlan(int id)
        {
            var response = await _mediator.Send(new GetByIdLearningPlanQuery(id));
            return Ok(response);
        }

        //save - post method
        [HttpPost("saveLearningPlan")]
        public async Task<IActionResult> SaveLearningPlan([FromBody] LearningPlanDetailsDTO dto)
        {
            var response = await _mediator.Send(new SaveLearningPlanCommand(dto));
            return Ok(response);
        }

        // update method
        [HttpPut("updateLearningPlan/{id}")]
        public async Task<IActionResult> UpdateLearningPlan(LearningPlanDetailsDTO learningPlanDetailsDTO)
        {
            var response = await _mediator.Send(new UpdateLearningPlanQuery(learningPlanDetailsDTO));
            return Ok(response);
        }

        //delete method
        [HttpDelete("deleteLearningPlan/{id}")]
        public async Task<IActionResult> DeleteLearningPlan(int id)
        {
            var response = await _mediator.Send(new DeleteLearningPlanCommand(id));
            return Ok(response);
        }
    }
}
