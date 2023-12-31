using EduArk.Application.Pipelines.StudentTargetSettings.Queries.DownloadStudentTargetSettingReport;
using EduArk.Application.Pipelines.Users.Queries.DownloadStudentExcelTemplate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("downloadStudentExcelTemplate")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<FileStreamResult> DownloadStudentExcelTemplate(DownloadStudentExcelTemplateQuery query)
        {
            var response = await _mediator.Send(query);

            byte[] excelFile = response.FileContent.ToArray();

            return File(new MemoryStream(excelFile), response.MimeType, response.FileName);

        }

        [HttpPost("downloadStudentTargetSettingReport")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<FileStreamResult> DownloadStudentTargetSettingReport(DownloadStudentTargetSettingReportQuery query)
        {
            var response = await _mediator.Send(query);

            byte[] excelFile = response.FileContent.ToArray();

            return File(new MemoryStream(excelFile), response.MimeType, response.FileName);
        }
    }
}
