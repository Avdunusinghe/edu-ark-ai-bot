using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.ExamMarkDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Application.Pipelines.ExamMarks.Commands.SaveExamMarks;
using EduArk.Application.Pipelines.ExamMarks.Commands.UploadExamMarksExcelBySubject;
using EduArk.Application.Pipelines.ExamMarks.Queries.GetExamMarksByFilter;
using EduArk.Application.Pipelines.Users.Queries.GetUserDetailsByFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadExamMarks")]
        public async Task<IActionResult> UploadExamMarks()
        {
            var container = new FileContainerDTO();

            var request = await Request.ReadFormAsync();

            foreach (var file in request.Files)
            {
                container.Files.Add(file);
            }

            var response = await _mediator
                .Send(new UploadExamMarksExcelBySubjectCommand()
                {
                    Container = container
                });



            return Ok(response);
        }

        [HttpPost("saveExamMarks")]
        public async Task<IActionResult> SaveExamMarks([FromBody] ExamMarkContainerDTO examMarks)
        {
            var response = await _mediator.Send(new SaveExamMarksCommand()
            {
                ExamMarkContainer = examMarks
            });

            return Ok(response);
        }

        [HttpPost("getExamMarksByFilter")]
        public async Task<IActionResult> GetExamMarksByFilter(GetExamMarksByFilterQuery getExamMarksByFilterQuery)
        {
            var response = await _mediator.Send(getExamMarksByFilterQuery);

            return Ok(response);
        }
    }
}
