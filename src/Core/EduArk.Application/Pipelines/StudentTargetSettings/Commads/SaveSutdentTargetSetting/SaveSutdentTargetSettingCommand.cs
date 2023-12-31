using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.StudentTargetSettingDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace EduArk.Application.Pipelines.StudentTargetSettings.Commads.SaveSutdentTargetSetting
{
    public record SaveSutdentTargetSettingCommand(StudentTargetSettingConfigureDTO studentTargetSettingData) : IRequest<ResultDTO>
    {
    }


    public class SaveSutdentTargetSettingCommandHandler : IRequestHandler<SaveSutdentTargetSettingCommand, ResultDTO>
    {
        private readonly IStudentQueryRepository _studentQueryRepository;
        private readonly ISubjectTargetSettingQueryRepository _subjectTargetSettingQueryRepository;
        private readonly ISubjectTargetSettingCommandRepository _subjectTargetSettingCommandRepository;
        private readonly IStudentClassQueryRepository _studentClassQueryRepository;
        private readonly IExamMarkQueryRepository _examMarkQueryRepository;
        private readonly IEduArkMachineLearningAPIService _eduArkMachineLearningAPIService;
        private readonly IConfiguration _configuration;

        public SaveSutdentTargetSettingCommandHandler
        (
            IStudentQueryRepository studentQueryRepository,
            ISubjectTargetSettingQueryRepository subjectTargetSettingQueryRepository,
            ISubjectTargetSettingCommandRepository subjectTargetSettingCommandRepository,
            IStudentClassQueryRepository studentClassQueryRepository,
            IExamMarkQueryRepository examMarkQueryRepository,
            IEduArkMachineLearningAPIService eduArkMachineLearningAPIService,
            IConfiguration configuration
        )
        {
            this._studentQueryRepository = studentQueryRepository;
            this._subjectTargetSettingQueryRepository = subjectTargetSettingQueryRepository;
            this._subjectTargetSettingCommandRepository = subjectTargetSettingCommandRepository;
            this._studentClassQueryRepository = studentClassQueryRepository;
            this._examMarkQueryRepository = examMarkQueryRepository;
            this._eduArkMachineLearningAPIService = eduArkMachineLearningAPIService;
            this._configuration = configuration;

        }
        public async Task<ResultDTO> Handle(SaveSutdentTargetSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ConfigureFilter(request);

                var listOfStudents = (await _studentClassQueryRepository
                                    .Query(x => x.AcademicYearId == request.studentTargetSettingData.CurrentAcademicYear &&
                                    x.AcademicLevelId == request.studentTargetSettingData.AcademicLevel &&
                                    x.IsActive == true)).ToList();

                

                foreach(var item in listOfStudents)
                {
                    
                    var inputData = await ProcessMachineLearningServiceInput(item, request);

                   
                    var analyzedMark = await _eduArkMachineLearningAPIService.StudentPerformanceAnalyzeAsync(inputData);

                    var subjectTargetSetting = new SubjectTargetSetting()
                    {
                        AcademicLevelId = item.AcademicLevelId,
                        SubjectId = request.studentTargetSettingData.Subject,
                        PredictedMark = analyzedMark,
                        AcademicYearId =item.AcademicYearId,
                        StudentId = item.Student.Id,
                        SemesterId = request.studentTargetSettingData.CurrentSemester,
                    };

                    
                    await _subjectTargetSettingCommandRepository
                            .AddAsync(subjectTargetSetting, cancellationToken);

                }

                return ResultDTO.Success(string.Empty);
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }

        private async  Task<List<int>> ProcessMachineLearningServiceInput(StudentClass item, SaveSutdentTargetSettingCommand request)
        {
            var markTerm1 = (int)Math.Round((await _examMarkQueryRepository
                                    .Query(x => x.Exam.AcademicYearId == request.studentTargetSettingData.PreviousSemesterOneAcademicYear &&
                                     x.Exam.ExamTypeId == request.studentTargetSettingData.ExamTypeId &&
                                     x.SubjectId == request.studentTargetSettingData.Subject &&
                                     x.Exam.SemesterId == request.studentTargetSettingData.PreviousSemesterOne &&
                                     x.Student.User.Id == item.StudentId)).FirstOrDefault()?.Marks ?? 0);

            var markTerm2 = (int)Math.Round((await _examMarkQueryRepository
                            .Query(x => x.Exam.AcademicYearId == request.studentTargetSettingData.PreviousSemesterTwoAcademicYear &&
                             x.Exam.ExamTypeId == request.studentTargetSettingData.ExamTypeId &&
                             x.SubjectId == request.studentTargetSettingData.Subject &&
                             x.Exam.SemesterId == request.studentTargetSettingData.PreviousSemesterTwo &&
                             x.Student.User.Id == item.StudentId)).FirstOrDefault()?.Marks ?? 0);

            var markTerm3 = (int)Math.Round((await _examMarkQueryRepository
                            .Query(x => x.Exam.AcademicYearId == request.studentTargetSettingData.PreviousSemesterThreeAcademicYear &&
                             x.Exam.ExamTypeId == request.studentTargetSettingData.ExamTypeId &&
                             x.SubjectId == request.studentTargetSettingData.Subject &&
                             x.Exam.SemesterId == request.studentTargetSettingData.PreviousSemesterThree &&
                             x.Student.User.Id == item.StudentId)).FirstOrDefault()?.Marks ?? 0);

            var markTerm4 = (int)Math.Round((await _examMarkQueryRepository
                            .Query(x => x.Exam.AcademicYearId == request.studentTargetSettingData.PreviousSemesterFourAcademicYear &&
                             x.Exam.ExamTypeId == request.studentTargetSettingData.ExamTypeId &&
                             x.SubjectId == request.studentTargetSettingData.Subject &&
                             x.Exam.SemesterId == request.studentTargetSettingData.PreviousSemesterFour &&
                             x.Student.User.Id == item.StudentId)).FirstOrDefault()?.Marks ?? 0);

            var markTerm5 = (int)Math.Round((await _examMarkQueryRepository
                            .Query(x => x.Exam.AcademicYearId == request.studentTargetSettingData.PreviousSemesterFiveAcademicYear &&
                             x.Exam.ExamTypeId == request.studentTargetSettingData.ExamTypeId &&
                             x.SubjectId == request.studentTargetSettingData.Subject &&
                             x.Exam.SemesterId == request.studentTargetSettingData.PreviousSemesterFive &&
                             x.Student.User.Id == item.StudentId)).FirstOrDefault()?.Marks ?? 0);

            var average = (markTerm1 + markTerm2 + markTerm3 + markTerm4 + markTerm5 + markTerm1 + markTerm2 + markTerm3 + markTerm4 + markTerm5) / 9;

            var total = average * 9;

            var initMark = markTerm1;

            var studyHours = item.Student.StudyHours;
            var confidentAcademicPerformance = item.Student.ConfidentAcademicPerformance;
            var importantFactorsAcademicPerformance = 6;

            var inputData = new List<int> { markTerm1, markTerm2, markTerm3, markTerm4, markTerm5, GetTotalMark(total), average, importantFactorsAcademicPerformance, studyHours, 2 };

            return inputData;
        }

        public void ConfigureFilter(SaveSutdentTargetSettingCommand request)
        {
           
            var currentAcademicYearId = request.studentTargetSettingData.CurrentAcademicYear;

            switch (request.studentTargetSettingData.CurrentSemester)
            {
                case 1:
                    request.studentTargetSettingData.PreviousSemesterOne = 3;
                    request.studentTargetSettingData.PreviousSemesterOneAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterTwo = 2;
                    request.studentTargetSettingData.PreviousSemesterTwoAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterThree = 1;
                    request.studentTargetSettingData.PreviousSemesterThreeAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterFour = 3;
                    request.studentTargetSettingData.PreviousSemesterFourAcademicYear = currentAcademicYearId - 2;

                    request.studentTargetSettingData.PreviousSemesterFive = 2;
                    request.studentTargetSettingData.PreviousSemesterFiveAcademicYear = currentAcademicYearId - 2;

                    break;
                case 2:
                    request.studentTargetSettingData.PreviousSemesterOne = 1;
                    request.studentTargetSettingData.PreviousSemesterOneAcademicYear = currentAcademicYearId;

                    request.studentTargetSettingData.PreviousSemesterTwo = 3;
                    request.studentTargetSettingData.PreviousSemesterTwoAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterThree = 2;
                    request.studentTargetSettingData.PreviousSemesterThreeAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterFour = 1;
                    request.studentTargetSettingData.PreviousSemesterFourAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterFive = 3;
                    request.studentTargetSettingData.PreviousSemesterFiveAcademicYear = currentAcademicYearId - 2;
                    break;
                case 3:
                    request.studentTargetSettingData.PreviousSemesterOne = 2;
                    request.studentTargetSettingData.PreviousSemesterOneAcademicYear = currentAcademicYearId ;

                    request.studentTargetSettingData.PreviousSemesterTwo = 1;
                    request.studentTargetSettingData.PreviousSemesterTwoAcademicYear = currentAcademicYearId;

                    request.studentTargetSettingData.PreviousSemesterThree = 1;
                    request.studentTargetSettingData.PreviousSemesterThreeAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterFour = 3;
                    request.studentTargetSettingData.PreviousSemesterFourAcademicYear = currentAcademicYearId - 1;

                    request.studentTargetSettingData.PreviousSemesterFive = 2;
                    request.studentTargetSettingData.PreviousSemesterFiveAcademicYear = currentAcademicYearId - 1;
                    break;

            }
        }

        private int GetTotalMark(decimal mark)
        {
            var total = 0m;

            if (mark > 500 || mark > 600)
            {
                total = 900;
            }

            if (mark > 700)
            {
                total = 1000;
            }

            if (mark > 850)
            {
                total = 1100;
            }


            if (mark > 850)
            {
                total = 1250;
            }


            return (int)total;
        }

    }
  
}
