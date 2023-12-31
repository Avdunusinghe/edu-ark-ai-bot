using EduArk.Application.Common.Helper;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace EduArk.Application.Pipelines.Users.Commands.UploadClassStudents
{
    public class UploadClassStudentsCommand : IRequest<MasterDataUploadResponseDTO>
    {
        public FileContainerDTO Container { get; set; }
    }

    public class UploadClassStudentsCommandHandler : IRequestHandler<UploadClassStudentsCommand, MasterDataUploadResponseDTO>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IStudentClassCommandRepository _studentClassCommandRepository;
        private readonly IConfiguration _configuration; 
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UploadClassStudentsCommandHandler> _logger;
        private StudentExcelContainerDTO studentExcelContainer;
        public UploadClassStudentsCommandHandler
        (
            IUserCommandRepository userCommandRepository,
            IUserQueryRepository userQueryRepository,
            IStudentClassCommandRepository studentClassCommandRepository, 
            IConfiguration configuration,
            IAcademicYearQueryRepository academicYearQueryRepository,
            IClassQueryRepository classQueryRepository,
            IAcademicLevelQueryRepository academicLevelQueryRepository,
            ICurrentUserService currentUserService,
            ILogger<UploadClassStudentsCommandHandler> logger
        )

        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._studentClassCommandRepository = studentClassCommandRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._classQueryRepository = classQueryRepository;
            this._configuration = configuration;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._currentUserService = currentUserService;
            this._logger = logger;
            this.studentExcelContainer = new StudentExcelContainerDTO();
        }
        public async Task<MasterDataUploadResponseDTO> Handle(UploadClassStudentsCommand request, CancellationToken cancellationToken)
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
                    studentExcelContainer = new StudentExcelContainerDTO();

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
                            foreach (var student in studentExcelContainer.Students)
                            {
                                var studentRecord = (await _userQueryRepository
                                                   .Query(x=>x.UserName.Trim().ToLower() == student.UserName.Trim().ToLower())).FirstOrDefault();

                                if(studentRecord is null)
                                {
                                    studentRecord = new Domain.Entities.Tenant.User()
                                    {
                                        FirstName = student.FirstName,
                                        LastName = student.LastName,
                                        Email = student.Email,
                                        UserName = student.UserName,
                                        PhoneNumber = student.EmegencyContactNo1,
                                        IsActive = true,
                                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(student.UserName),

                                    };

                                    studentRecord.UserRoles.Add(new Domain.Entities.Tenant.UserRole()
                                    {
                                        UserId = studentRecord.Id,
                                        RoleId = (int)RoleType.Student,
                                        IsActive = true,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedByUserId = _currentUserService.UserId!.Value,
                                        UpdateDate = DateTime.UtcNow,
                                        UpdatedByUserId = _currentUserService.UserId!.Value,
                                    });

                                    studentRecord.Student = new Student()
                                    {
                                        AdmissionNo = StudentAdmissionNumberManager.GenerateUniqueAdmissionNumber(),
                                        EmegencyContactNo1 = student.EmegencyContactNo1,
                                        EmegencyContactNo2 = student.EmegencyContactNo2,
                                        Gender = student.Gender,
                                        IsActive = true,
                                    };

                                    studentRecord.Student.StudentClasses.Add(new StudentClass()
                                    {
                                        StudentId = studentRecord.Id,
                                        ClassNameId = studentExcelContainer.ClassNameId,
                                        AcademicYearId = studentExcelContainer.AcademicYearId,
                                        AcademicLevelId = studentExcelContainer.AcademicLevelId,
                                        IsActive = true,
                                    });

                                    await _userCommandRepository.AddAsync(studentRecord, cancellationToken);
                                }
                                else
                                {
                                    studentRecord.FirstName = student.FirstName;
                                    studentRecord.LastName = student.LastName;
                                    studentRecord.Email = student.Email;
                                    studentRecord.UserName = student.UserName;


                                    studentRecord.Student.EmegencyContactNo1 = student.EmegencyContactNo1;
                                    studentRecord.Student.EmegencyContactNo2 = student.EmegencyContactNo2;
                                    studentRecord.Student.Gender = student.Gender;

                                    var studentClass = studentRecord.Student.StudentClasses.FirstOrDefault(x => x.IsActive == true);

                                    if (studentClass.ClassNameId != studentExcelContainer.ClassNameId)
                                    {
                                        studentClass.IsActive = false;
                                       
                                        await _studentClassCommandRepository.UpdateAsync(studentClass, cancellationToken);

                                        studentRecord.Student.StudentClasses.Add(new StudentClass()
                                        {
                                            StudentId = studentRecord.Id,
                                            ClassNameId = studentExcelContainer.ClassNameId,
                                            AcademicYearId = studentExcelContainer.AcademicYearId,
                                            AcademicLevelId = studentExcelContainer.AcademicLevelId,
                                            IsActive = true,
                                        });
                                    }

                                     await _userCommandRepository.UpdateAsync(studentRecord, cancellationToken);

                                }

                            }
                        }

                        response.Results.Add(new MasterDataFileValidateResultDTO()
                        {
                            IsSuccess = true,
                            ValidateMessage = $"Class student list has been uploaded successfully for file name {item.Value}."
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());;
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
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["students"];

                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row - 5;

                    var yearValue = worksheet.Cells[1, 2].Value.ToString().Trim();

                    if (!string.IsNullOrEmpty(yearValue))
                    {
                        int enteredYear;

                        if (int.TryParse(yearValue, out enteredYear))
                        {
                            var currentYear = DateTime.Now.Year;
                            studentExcelContainer.AcademicYearId = academicYears.FirstOrDefault(x => x.Id == Int32.Parse(yearValue)).Id;
                            //if (!(enteredYear == currentYear || enteredYear == currentYear + 1))
                            //{
                            //    response.Add(new MasterDataFileValidateResultDTO()
                            //    {
                            //        ValidateMessage = "Invalid user excel template. Year can be current year or next year only.",
                            //        IsSuccess = false
                            //    });
                            //}
                            //else
                            //{
                               
                            //}
                        }
                        else
                        {
                            response.Add(new MasterDataFileValidateResultDTO()
                            {
                                ValidateMessage = "Invalid user excel template. Invalid Year format has been entered.",
                                IsSuccess = false
                            });
                        }
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResultDTO()
                        {
                            ValidateMessage = "Invalid user excel template. Academic year is not entered.",
                            IsSuccess = false
                        });
                    }

                    var academicLevel = worksheet.Cells[2, 2].Value.ToString().Trim().ToLower();
                    var enteredAcademicLevel = academicLevels.FirstOrDefault(x => x.Name.Trim().ToLower() == academicLevel);

                    if (enteredAcademicLevel != null)
                    {
                        studentExcelContainer.AcademicLevelId = enteredAcademicLevel.Id;
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResultDTO() 
                                    { 
                                        ValidateMessage = "Invalid user excel template. academicLevel value does not exists or invalid academicLevel value has been entered.", 
                                        IsSuccess = false 
                                    });
                    }

                    if (studentExcelContainer.AcademicYearId > 0 && studentExcelContainer.AcademicLevelId > 0)
                    {
                        var classValue = worksheet.Cells[3, 2].Value.ToString().Trim().ToLower();

                        var enteredClass = (
                           await _classQueryRepository
                               .Query(x => x.ClassName.Name.Trim().ToLower() == classValue && x.AcademicLevelId == studentExcelContainer.AcademicLevelId && x.AcademicYearId == studentExcelContainer.AcademicYearId))
                               .FirstOrDefault();

                        if (enteredClass != null)
                        {
                            studentExcelContainer.ClassNameId = enteredClass.ClassNameId;
                        }
                        else
                        {
                            response.Add(new MasterDataFileValidateResultDTO() 
                            { 
                                ValidateMessage = "Invalid user excel template. Class name value does not exists or invalid class name value has been entered.", 
                                IsSuccess = false 
                            });
                        }
                    }

                    var teacherName = worksheet.Cells[4, 2].Value.ToString().Trim().ToLower();

                    var enterTeacher = (await _userQueryRepository.Query(x => x.UserName.ToLower().Trim() == teacherName)).FirstOrDefault();

                    if (enterTeacher != null)
                    {
                        studentExcelContainer.ClassTeacherId = enterTeacher.Id;
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResultDTO()
                        {
                            ValidateMessage = "Invalid user excel template. Teacher username does not exists or invalid teacher username has been entered.",
                            IsSuccess = false
                        });
                    }

                    for (int row = 6; row <= worksheet.Dimension.End.Row; row++)
                    {

                        var student = new StudentDTO();

                        for (int col = 1; col <= colCount; col++)
                        {
                            if (row == 6)
                            {
                                if (col == 1)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "First Name")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO() 
                                        { 
                                            ValidateMessage = "Invalid excel file. FirstName column (Column index 0) is missing.", 
                                            IsSuccess = false 
                                        });
                                    }

                                }
                                else if (col == 2)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Last  Name")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO() 
                                        { 
                                            ValidateMessage = "Invalid excel file. Last  Name column (Column index 1) is missing.", 
                                            IsSuccess = false 
                                        });

                                    }
                                }
                                else if (col == 3)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "User Name")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Last  User Name column (Column index 2) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                                else if (col == 4)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Email")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Last  Email column (Column index 3) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                                else if (col == 5)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "EmegencyContactNo1")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Last  EmegencyContactNo1 column (Column index 4) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                                else if (col == 6)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "EmegencyContactNo2")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Last  EmegencyContactNo2 column (Column index 5) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                                else if (col == 7)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Gender")
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Invalid excel file. Last  Gender column (Column index 6) is missing.",
                                            IsSuccess = false
                                        });

                                    }
                                }
                            }
                            else
                            {
                                if (col == 1)
                                {
                                    var firstName = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if(!string.IsNullOrEmpty(firstName))
                                    {
                                        student.FirstName = firstName;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO() 
                                        { 
                                            ValidateMessage = "Empty First Name found in row " + row.ToString() + ".", 
                                            IsSuccess = false 
                                        });
                                    }

                                }
                                else if (col == 2)
                                {
                                    var lastName = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if(!string.IsNullOrEmpty(lastName))
                                    {
                                        student.LastName = lastName;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO() 
                                        { 
                                            ValidateMessage = "Gender can not be empty in row " + row.ToString() + ".", 
                                            IsSuccess = false 
                                        });
                                    }
                                }
                                else if(col == 3)
                                {
                                    var userName = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if(!string.IsNullOrEmpty(userName))
                                    {
                                        student.UserName = userName;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "User Name can not be empty in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }

                                else if (col == 4)
                                {
                                    var email = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        student.Email = email;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Email can not be empty in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }
                                else if (col == 5)
                                {
                                    var emegencyContactNo1 = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if (!string.IsNullOrEmpty(emegencyContactNo1))
                                    {
                                        student.EmegencyContactNo1 = emegencyContactNo1;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "EmegencyContactNo1 can not be empty in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }
                                else if (col == 6)
                                {
                                    var emegencyContactNo2 = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if (!string.IsNullOrEmpty(emegencyContactNo2))
                                    {
                                        student.EmegencyContactNo2 = emegencyContactNo2;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "EmegencyContactNo2 can not be empty in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }
                                else if (col == 7)
                                {
                                    var gender = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();

                                    if (!string.IsNullOrEmpty(gender))
                                    {
                                       
                                        if(gender == "Male")
                                        {
                                            student.Gender =  Gender.Male;
                                        }
                                        else if(gender == "Female")
                                        {
                                            student.Gender = Gender.Female;
                                        }
                                      
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResultDTO()
                                        {
                                            ValidateMessage = "Gender can not be empty in row " + row.ToString() + ".",
                                            IsSuccess = false
                                        });
                                    }
                                }

                                
                            }

                            

                        }
                        if (row > 6)
                        {
                            studentExcelContainer.Students.Add(student);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
