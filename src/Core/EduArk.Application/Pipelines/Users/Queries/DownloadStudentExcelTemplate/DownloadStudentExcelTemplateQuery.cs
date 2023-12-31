using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using OfficeOpenXml;
using System.Reflection;

namespace EduArk.Application.Pipelines.Users.Queries.DownloadStudentExcelTemplate
{
    public record DownloadStudentExcelTemplateQuery : IRequest<FileDTO>
    {
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
    }

    public class DownloadStudentExcelTemplateQueryHandler : IRequestHandler<DownloadStudentExcelTemplateQuery, FileDTO>
    {
        private readonly IClassQueryRepository _classQueryRepository;
        public DownloadStudentExcelTemplateQueryHandler(IClassQueryRepository classQueryRepository)
        {
            this._classQueryRepository = classQueryRepository;
        }
        public async Task<FileDTO> Handle(DownloadStudentExcelTemplateQuery request, CancellationToken cancellationToken)
        {
            
            var response = new FileDTO();

            var classData = (await _classQueryRepository
                .Query(x => x.AcademicYearId == request.AcademicYearId && x.AcademicLevelId == request.AcademicLevelId && x.IsActive == true))
                .FirstOrDefault();
            try
            {
                using (var package = new ExcelPackage(GenerateTemplatePath(ExcelTemplateConstants.StudentExcelUploadTemplates)))
                {
                    var sheet = package.Workbook.Worksheets["students"];

                    sheet.Cells[1, 2].Value = classData.AcademicYearId;
                    sheet.Cells[2, 2].Value = classData.AcademicLevel.Name;
                    sheet.Cells[3, 2].Value = classData.ClassName.Name;
                    sheet.Cells[4, 2].Value = classData.ClassTeachers.FirstOrDefault(x => x.IsActive == true).Teacher.UserName;

                    var excelData = package.GetAsByteArray();
                    var fileName = $"StudentUpload-{ classData.AcademicYearId}-{classData.AcademicLevel.Name}-{classData.ClassName.Name}.xlsx";

                    response.FileContent = new MemoryStream(excelData);
                    response.MimeType = ExcelTemplateConstants.ContentType;
                    response.FileName = fileName;
                }

              
               
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }

        private string GenerateTemplatePath(string TemplatePath)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            if (outPutDirectory == null) return null;

            return Path.Combine(outPutDirectory, TemplatePath);
        }
    }


}
