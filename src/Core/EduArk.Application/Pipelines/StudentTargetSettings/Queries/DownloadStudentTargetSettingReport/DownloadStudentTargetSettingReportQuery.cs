using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System.Reflection;

namespace EduArk.Application.Pipelines.StudentTargetSettings.Queries.DownloadStudentTargetSettingReport
{
    public class DownloadStudentTargetSettingReportQuery : IRequest<FileDTO>
    {
        public int StudentId { get; set; }
        public int ClassNameId { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int SubjectId { get; set; }
        public int SemesterId { get; set; }
    }

    public class DownloadStudentTargetSettingReportQueryHandler
                            : IRequestHandler<DownloadStudentTargetSettingReportQuery, FileDTO>
    {
        private readonly IStudentClassQueryRepository _studentClassQueryRepository;
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;
        private readonly IStudentQueryRepository _studentQueryRepository;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly ISubjectTargetSettingQueryRepository _subjectTargetSettingQueryRepository;
        private readonly IConfiguration _configuration;
        public DownloadStudentTargetSettingReportQueryHandler
        (   
            IStudentClassQueryRepository studentClassQueryRepository,
            ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository,
            IStudentQueryRepository studentQueryRepository,
            IAcademicLevelQueryRepository academicLevelQueryRepository,
            ICurrentUserService currentUserService,
            IUserQueryRepository userQueryRepository,
            IClassQueryRepository classQueryRepository,
            ISubjectTargetSettingQueryRepository subjectTargetSettingQueryRepository,
            IConfiguration configuration
        )
        {
            this._studentClassQueryRepository = studentClassQueryRepository;
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
            this._studentQueryRepository = studentQueryRepository;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._currentUserService = currentUserService;
            this._userQueryRepository = userQueryRepository;
            this._classQueryRepository = classQueryRepository;
            this._subjectTargetSettingQueryRepository = subjectTargetSettingQueryRepository;
            this._configuration = configuration;
        }
      
        public async Task<FileDTO> Handle(DownloadStudentTargetSettingReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = new FileDTO();
                var academicSubjects = (await _subjectAcademicLevelQueryRepository
                                        .Query(x => x.AcademicLevelId == request.AcademicLevelId)).ToList();

                var student = (await _studentQueryRepository.Query(x => x.User.Id == request.StudentId)).FirstOrDefault();
                var academicLevel = await _academicLevelQueryRepository.GetById(request.AcademicLevelId, cancellationToken);
                var loggedInUser = await _userQueryRepository.GetById(_currentUserService.UserId.Value, cancellationToken);
                var classDetails = (await _classQueryRepository.Query(x => x.ClassNameId == request.ClassNameId &&
                                    x.AcademicYearId == request.AcademicYearId && x.AcademicLevelId == request.AcademicLevelId))
                                    .FirstOrDefault();

                var fileIdentifier = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var excelName = string.Format("{0}{1}.xlsx", string.Empty, fileIdentifier);
                var pdfName = string.Format("{0}{1}.pdf", string.Empty, fileIdentifier);

                var excelSection = _configuration.GetSection("Report");
                var pdfSavingPath = excelSection.GetSection("PDFReportSavingPath").Value;
                var excelSavingPath = excelSection.GetSection("ExcelReportSavingPath").Value;

                bool isExcelExists = System.IO.Directory.Exists(excelSavingPath);
                bool exists = System.IO.Directory.Exists(pdfSavingPath);

                if (!isExcelExists)
                    System.IO.Directory.CreateDirectory(excelSavingPath);

                if (!exists)
                    System.IO.Directory.CreateDirectory(pdfSavingPath);

                var excelReportPath = string.Format("{0}{1}", excelSavingPath, excelName);
                var pdfReportPath = string.Format("{0}{1}", pdfSavingPath, pdfName);
                var templatePath = GenerateReportTemplatePath();

                File.Copy(templatePath, excelReportPath);
                FileInfo newFile = new FileInfo(excelReportPath);

                using (var package = new ExcelPackage(newFile))
                {
                    var workSheet = package.Workbook.Worksheets["report"];
                    var classTeacherName = $"{classDetails.ClassTeachers.FirstOrDefault().Teacher.FirstName} {classDetails.ClassTeachers.FirstOrDefault().Teacher.LastName}";

                    workSheet.Cells[3, 3].Value = student.AdmissionNo ?? string.Empty;
                    workSheet.Cells[4, 3].Value = $"{student.User.FirstName ?? string.Empty} {student.User.LastName ?? string.Empty}";
                    workSheet.Cells[5, 3].Value = academicLevel.Name ?? string.Empty;
                    workSheet.Cells[5, 3].Value = academicLevel.Name ?? string.Empty;
                    workSheet.Cells[6, 3].Value = request.AcademicYearId.ToString() ?? string.Empty;
                    workSheet.Cells[7, 3].Value = classDetails.Name ?? string.Empty;
                    workSheet.Cells[8, 3].Value = classTeacherName ?? string.Empty;
                    workSheet.Cells[9, 3].Value = $"{loggedInUser.FirstName ?? string.Empty} {loggedInUser.LastName ?? string.Empty}";
                    workSheet.Cells[10, 3].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    var row = 13;

                    foreach (var item in academicSubjects)
                    {
                        var rowData = (await _subjectTargetSettingQueryRepository.Query(x => x.StudentId == student.Id &&
                                           x.AcademicYearId == request.AcademicYearId && x.AcademicLevelId == request.AcademicLevelId &&
                                           x.SubjectId == item.SubjectId)).FirstOrDefault();

                        if (rowData != null)
                        {
                            workSheet.Cells[row, 2].Value = $"{rowData.Subject.SubjectCode} - {rowData.Subject.Name}";
                            workSheet.Cells[row, 3].Value = rowData.PredictedMark.ToString() ?? "N/A";
                            workSheet.Cells[row, 4].Value = ConfigureGrade(rowData.PredictedMark);
                            workSheet.Cells[row, 5].Value = rowData.TeacherTargetScore.ToString() ?? "N/A";

                            row++;

                        }

                    }

                    package.Save();
                }

                Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();

                //Load excel file  
                workbook.LoadFromFile(newFile.FullName);

                //Save excel file to pdf file.  
                workbook.SaveToFile(pdfReportPath, Spire.Xls.FileFormat.PDF);

                byte[] fileContents = null;
                MemoryStream ms = new MemoryStream();

                using (FileStream fs = File.OpenRead(pdfReportPath))
                {
                    fs.CopyTo(ms);
                    fileContents = ms.ToArray();
                    ms.Dispose();
                    file.FileContent = new MemoryStream(fileContents);
                }

                file.MimeType = "application/pdf";
                file.FileName = pdfName;


                return file;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private string GenerateReportTemplatePath()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var templatePath = Path.Combine(outPutDirectory, "ExcelTemplates\\Report\\StudentTargetSettingReport.xlsx");

            return templatePath;
        }

        private string ConfigureGrade(decimal mark)
        {
            if (mark >= 90 && mark <= 100)
            {
                return "A+";
            }
            else if (mark >= 75 && mark < 90)
            {
                return "A";
            }
            else if (mark >= 65 && mark < 75)
            {
                return "B";
            }
            else if (mark >= 55 && mark < 65)
            {
                return "C+";
            }
            else if (mark >= 45 && mark < 55)
            {
                return "C";
            }
            else if (mark < 45)
            {
                return "F";
            }
            else
            {
                return "Invalid grade";
            }
        }
    }
}
