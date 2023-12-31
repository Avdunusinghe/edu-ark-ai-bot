using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.StudentTargetSettingDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.StudentTargetSettings.Queries.GetStudentsTargetSettingsByFilter
{
    public record GetStudentsTargetSettingsByFilterQuery : IRequest<PaginatedItemDTO<StudentTargetSettingDetailDTO>>
    {
        public StudentTargetSettingsFilterDTO Filter { get; set; }
    }

    public class GetStudentsTargetSettingsByFilterQueryHandler : IRequestHandler<GetStudentsTargetSettingsByFilterQuery, PaginatedItemDTO<StudentTargetSettingDetailDTO>>
    {
        
        private readonly IStudentClassQueryRepository _studentClassQueryRepository;
      
        public GetStudentsTargetSettingsByFilterQueryHandler(IStudentClassQueryRepository studentClassQueryRepository)
        {
            this._studentClassQueryRepository = studentClassQueryRepository;
        }
        public async Task<PaginatedItemDTO<StudentTargetSettingDetailDTO>> Handle(GetStudentsTargetSettingsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var targetSettingDetails = new List<StudentTargetSettingDetailDTO>();
                var totalRecordCount = 0;

                var query = await _studentClassQueryRepository
                           .Query(x => x.AcademicYearId == request.Filter.AcademicYearId && x.AcademicLevelId == request.Filter.AcademicLevelId && 
                           x.ClassNameId == request.Filter.ClassNameId);

                

                if (!string.IsNullOrEmpty(request.Filter.SearchText))
                {
                    query = query.Where(x => x.Student.User.FirstName.Contains(request.Filter.SearchText) ||
                                        x.Student.User.LastName.Contains(request.Filter.SearchText) ||
                                        x.Student.User.UserName.Contains(request.Filter.SearchText));
                }

                totalRecordCount = query.Count();

                var studentClassData = query.OrderByDescending(x => x.ClassNameId)
                                           .Skip(request.Filter.CurrentPage * request.Filter.PageSize)
                                           .ToList();

                foreach ( var classData in studentClassData)
                {
                    var item = new StudentTargetSettingDetailDTO();

                    var targetSettings = classData.Student.SubjectTargetSettings
                                        .FirstOrDefault(x => x.SubjectId == request.Filter.SubjectId && x.SemesterId == request.Filter.SemesterId);
                    if(targetSettings != null)
                    {
                        item.Id = targetSettings.Id;
                        item.StudentId = classData.Student.Id;
                        item.StudentName = $"{classData.Student.User.FirstName} {classData.Student.User.LastName}";
                        item.StudentProfileImage = classData.Student.User.ProfileImageUrl ?? string.Empty;
                        item.SubjectId = targetSettings.SubjectId;
                        item.SubjectName = targetSettings.Subject.Name;
                        item.PredictedMark = targetSettings.PredictedMark;
                        item.TeacherTaergetScore = targetSettings.TeacherTargetScore ?? 0;
                        item.Grade = ConfigureGrade(targetSettings.PredictedMark);
                        item.Severity = GetGradeSeverity(ConfigureGrade(targetSettings.PredictedMark));
                        targetSettingDetails.Add(item);
                    }
                  

                }
                var data = targetSettingDetails.OrderBy(x=>x.PredictedMark).ToList();
                return new PaginatedItemDTO<StudentTargetSettingDetailDTO>
                        (data, totalRecordCount, request.Filter.CurrentPage + 1, request.Filter.PageSize);

            }
            catch (Exception)
            {
                return new PaginatedItemDTO<StudentTargetSettingDetailDTO>
                        (new List<StudentTargetSettingDetailDTO>(), 0, 0, 0);
                throw;
            }
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

        public static string GetGradeSeverity(string letterGrade)
        {
            switch (letterGrade)
            {
                case "A+":
                    return "Excellent (Low Severity)";
                case "A":
                    return "Excellent (Low Severity)";
                case "B":
                    return "Good (Moderate Severity)";
                case "C+":
                    return "Satisfactory (Moderate Severity)";
                case "C":
                    return "Satisfactory (Moderate Severity)";
                case "F":
                    return "Failing (Highest Severity)";
                default:
                    return "Invalid Grade";
            }
        }
    }
}
