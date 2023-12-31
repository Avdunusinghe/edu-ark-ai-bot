
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Application.Pipelines.Lessons.Commands.DeleteLesson;
using EduArk.Application.Pipelines.Lessons.Commands.SaveLesson;
using EduArk.Application.Pipelines.Lessons.Commands.UploadLessonFile;
using EduArk.Application.Pipelines.Lessons.Queries.GetByIdLesson;
using EduArk.Application.Pipelines.Lessons.Queries.GetLesson;
using EduArk.Application.Pipelines.Lessons.Queries.UpdateLesson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //get all lessons
        [HttpGet("getLesson")]
        public async Task<IActionResult> GetLesson()
        {
            var response = await _mediator.Send(new GetAllLessonQuery());
            return Ok(response);
        }

        // get by id
        [HttpGet("getBYIdLesson/{id}")]
        public async Task<IActionResult> GetByIdLesson(int id)
        {
            var response = await _mediator.Send(new GetByIdLessonQuery(id));
            return Ok(response);
        }

        //save - post method
        [HttpPost("saveLesson")]
        public async Task<IActionResult> SaveLesson([FromBody] LessonDTO dto)
        {
            var response = await _mediator.Send(new SaveLessonCommand(dto));
            return Ok(response);
        }

        // update method
        [HttpPut("updateLesson/{id}")]
        public async Task<IActionResult> UpdateLesson(LessonDTO lessonDTO)
        {
            var response = await _mediator.Send(new UpdateLessonQuery(lessonDTO));
            return Ok(response);
        }

        //delete method
        [HttpDelete("deleteLesson/{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var response = await _mediator.Send(new DeleteLessonCommand(id));
            return Ok(response);
        }

        //file upload (resources upload - video/audio and text)
        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadLessonFile")]
        public async Task<IActionResult> uploadLessonFile()
        {
            var request = await Request.ReadFormAsync();
            if (request.Files.Any())
            {
                var container = new BlobContainerDTO();
                foreach (var file in request.Files)
                {
                    container.Files.Add(file);
                }
                container.Id = int.Parse(request["lessonID"]);
                var response = await _mediator
                                    .Send(new UploadLessonFileCommand(container));
                return Ok(response);
            }
            else
            {
                return BadRequest(ResultDTO.Failure(new List<string>
                {
                    "BAD REQUEST"
                }));
            }
        }


    }
}
