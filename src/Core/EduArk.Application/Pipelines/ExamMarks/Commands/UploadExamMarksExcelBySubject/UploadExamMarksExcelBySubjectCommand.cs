using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.ExamMarkDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace EduArk.Application.Pipelines.ExamMarks.Commands.UploadExamMarksExcelBySubject
{
    public record UploadExamMarksExcelBySubjectCommand : IRequest<MasterDataUploadResponseDTO>
    {
        public FileContainerDTO Container { get; set; }
    }

    public class UploadExamMarksExcelBySubjectCommandHandler : IRequestHandler<UploadExamMarksExcelBySubjectCommand, MasterDataUploadResponseDTO>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly ILogger<UploadExamMarksExcelBySubjectCommandHandler> _logger;
        private readonly IStudentQueryRepository _studentQueryRepository; 
        private readonly IConfiguration _configuration;
        private readonly IExamQueryRepository _examQueryRepository;
        private readonly ISemesterQueryRepository _semesterQueryRepository;
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        private readonly IExamMarkCommandRepository _examMarkCommandRepository;
        private ExamMarkContainerDTO examMarkExcelContainer;
        public UploadExamMarksExcelBySubjectCommandHandler
        (
            IAcademicYearQueryRepository academicYearQueryRepository,
            IAcademicLevelQueryRepository academicLevelQueryRepository,
            ILogger<UploadExamMarksExcelBySubjectCommandHandler> logger,
            IStudentQueryRepository studentQueryRepository,
            IConfiguration configuration,
            IExamQueryRepository examQueryRepository,
            ISemesterQueryRepository semesterQueryRepository,
            ISubjectQueryRepository subjectQueryRepository,
            IExamMarkCommandRepository examMarkCommandRepository
        )
        {
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._logger = logger;
            this._studentQueryRepository = studentQueryRepository;
            this._configuration = configuration;
            this._examQueryRepository = examQueryRepository;
            this._semesterQueryRepository = semesterQueryRepository;
            this._subjectQueryRepository = subjectQueryRepository;
            this._examMarkCommandRepository = examMarkCommandRepository;
            this.examMarkExcelContainer = new ExamMarkContainerDTO();

        }
        public async Task<MasterDataUploadResponseDTO> Handle(UploadExamMarksExcelBySubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new MasterDataUploadResponseDTO();

            response.IsSucccess = true;

            try
            {
                var folderPath = _configuration.GetSection("FileUploadPath").Value;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var upLoadedFiles = new Dictionary<string, string>();

                foreach (var item in request.Container.Files)
                {
                    var filePath = string.Format(@"{0}\{1}", folderPath, item.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    upLoadedFiles.Add(filePath, item.FileName);
                }

                foreach (var item in upLoadedFiles)
                {
                    examMarkExcelContainer = new ExamMarkContainerDTO();

                    try
                    {
                        var validateResult = await ValidateExcelFileContenetsAsync(item.Key, cancellationToken);

                        response.Results.AddRange(validateResult);
                        var validationErrorCount = response.Results.Where(x => x.IsSuccess == false).Count();

                        if (response.IsSucccess == true && validationErrorCount > 0)
                        {
                            response.IsSucccess = false;
                        }
                        else
                        {
                            foreach (var studentMark in examMarkExcelContainer.StudentMarks)
                            {
                                var examMark = new ExamMark()
                                {
                                    ExamId = examMarkExcelContainer.ExamId,
                                    AcademicLevelId = examMarkExcelContainer.ExamId,
                                    SubjectId = examMarkExcelContainer.SubjectId,
                                    StudentId = studentMark.StudentId,
                                    Marks = studentMark.Marks,
                                    Grade = ConfigureGrade(studentMark.Marks),
                                };

                                await _examMarkCommandRepository.AddAsync(examMark, cancellationToken);
                            }
                        }

                        response.Results.Add(new MasterDataFileValidateResultDTO()
                        {
                            IsSuccess = true,
                            ValidateMessage = $"Student Marks list has been uploaded successfully for file name {item.Value}."
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        response.IsSucccess = false;
                        response.Results.Add(new MasterDataFileValidateResultDTO() { IsSuccess = false, ValidateMessage = $"Exception has been occured while processing the class student import using file name {item.Value}." });
                    }

                }

                var errorCount = response.Results.Where(x => x.IsSuccess == false).Count();

                if (response.IsSucccess == true && errorCount == 0)
                {
                    response.Results.Add(new MasterDataFileValidateResultDTO()
                    {
                        IsSuccess = true,
                        ValidateMessage = "All file has been uploaded and process successfully."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSucccess = false;
                response.Results.Add(
                    new MasterDataFileValidateResultDTO()
                    {
                        IsSuccess = false,
                        ValidateMessage = "An exception has been occured while processing the excel file."
                    });
            }
            return response;
        }

        private async Task<List<MasterDataFileValidateResultDTO>> ValidateExcelFileContenetsAsync(string saveFilePath, CancellationToken cancellationToken)
        {
            var response = new List<MasterDataFileValidateResultDTO>();

            try
            {
                var academicYears = await _academicYearQueryRepository.GetAll(cancellationToken);
                var academicLevels = await _academicLevelQueryRepository.GetAll(cancellationToken);

                FileInfo fileInfo = new FileInfo(saveFilePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["marks"];

                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row - 6;

                    
                    var yearValue = worksheet.Cells[1, 2].Value.ToString().Trim();
                    var academicLevelValue = worksheet.Cells[2, 2].Value.ToString().Trim();
                    var examType = worksheet.Cells[4, 2].Value.ToString().Trim();
                    var semesterValue = worksheet.Cells[5, 2].Value.ToString().Trim();
                    var subjectCode = worksheet.Cells[7, 2].Value.ToString().Trim();


                    var semester = (await _semesterQueryRepository.Query(x=>x.Name.ToLower().Trim() == semesterValue.ToLower().Trim())).FirstOrDefault();
                    var exam = (await _examQueryRepository.Query(x => x.AcademicYearId == int.Parse(yearValue) && x.SemesterId == semester.Id)).FirstOrDefault();
                    var subject = (await _subjectQueryRepository.Query(x=>x.SubjectCode == subjectCode)).FirstOrDefault();

                    if(semester == null)
                    {
                        response.Add(new MasterDataFileValidateResultDTO()
                        {
                            ValidateMessage = "Invalid excel template. system not available entertred semster.",
                            IsSuccess = false
                        });
                    }
                    else if (exam == null)
                    {
                        response.Add(new MasterDataFileValidateResultDTO()
                        {
                            ValidateMessage = "Invalid excel template. system not available entertred exam.",
                            IsSuccess = false
                        });
                    }
                    else
                    {
                        examMarkExcelContainer.ExamId = exam.Id;
                    }

                    if(subject == null)
                    {
                        response.Add(new MasterDataFileValidateResultDTO()
                        {
                            ValidateMessage = "Invalid excel template. system not available entertred subject code subject.",
                            IsSuccess = false
                        });
                    }
                    else
                    {
                        examMarkExcelContainer.SubjectId = subject.Id;
                    }

                    for (int row = 9; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var studentMark = new StudentMarksDTO();

                        for(int col = 1; col <= colCount; col++)
                        {
                            if(row == 9)
                            {
                                if(col == 1)
                                {
                                    if(worksheet.Cells[row, col].Value.ToString().Trim() != "Addmission Number")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Addmission Number column (Column index 0) is missing.",
                                            IsSuccess = false
                                        });
                                    }
                                }
                                else if (col == 3)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Mark")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file.Mark column (Column index 3) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                              
                            }
                            else
                            {
                                if (col == 1)
                                {
                                    var addmissionNumber = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if (!string.IsNullOrEmpty(addmissionNumber))
                                    {
                                        var student = (await _studentQueryRepository.Query(x=>x.AdmissionNo == addmissionNumber)).FirstOrDefault();

                                        if(student != null)
                                        {
                                            studentMark.StudentId = student.Id;
                                        }
                                        else
                                        {
                                            response.Add(new MasterDataFileValidateResultDTO()
                                            {
                                                ValidateMessage = $"Student - ({addmissionNumber}) not exsitis in the system ",
                                                IsSuccess = false
                                            });
                                        }
                                    }
                                    else
                                    {

                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Empty Addmission Number found in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }
                                else if (col == 3)
                                {
                                    var mark = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if (!string.IsNullOrEmpty(mark))
                                    {
                                        if (mark.ToLower() == "ab")
                                        {
                                            studentMark.Marks = 0;
                                        }
                                        else
                                        {
                                            studentMark.Marks = decimal.Parse(mark);
                                        }
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Empty Mark found in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }
                               

                            }
                        }

                        if (row > 9)
                        {
                            examMarkExcelContainer.StudentMarks.Add(studentMark);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        private string ConfigureGrade(decimal mark)
        {
            string grade;

            if (mark >= 90)
            {
                grade = "A+";
            }
            else if (mark >= 85)
            {
                grade = "A";
            }
            else if (mark >= 80)
            {
                grade = "A-";
            }
            else if (mark >= 75)
            {
                grade = "B+";
            }
            else if (mark >= 70)
            {
                grade = "B";
            }
            else if (mark >= 65)
            {
                grade = "B-";
            }
            else if (mark >= 60)
            {
                grade = "C+";
            }
            else if (mark >= 55)
            {
                grade = "C";
            }
            else if (mark >= 50)
            {
                grade = "C-";
            }
            else if (mark >= 45)
            {
                grade = "D";
            }
            else
            {
                grade = "F";
            }

            return grade;
        }
    }
}
