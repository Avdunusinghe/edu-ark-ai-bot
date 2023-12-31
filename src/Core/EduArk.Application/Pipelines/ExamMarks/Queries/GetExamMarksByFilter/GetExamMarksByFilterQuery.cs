using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.ExamMarkDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.ExamMarks.Queries.GetExamMarksByFilter
{
    public record GetExamMarksByFilterQuery : IRequest<PaginatedItemDTO<StudentMarksDTO>>
    {
        public string? StudentName { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
        public int SubjectId { get; set; }
        public int ExamType { get; set; }
        public int Semester { get; set; }
        public int ExamId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class GetExamMarksByFilterHandler : IRequestHandler<GetExamMarksByFilterQuery, PaginatedItemDTO<StudentMarksDTO>>
    {
        private readonly IExamQueryRepository _examQueryRepository;
        private readonly IExamMarkQueryRepository _examMarkQueryRepository;
        private readonly IStudentClassQueryRepository _studentClassQueryRepository;
        public GetExamMarksByFilterHandler
        (
            IExamQueryRepository examQueryRepository,
            IExamMarkQueryRepository examMarkQueryRepository,
            IStudentClassQueryRepository studentClassQueryRepository
        )
        {
            this._examQueryRepository = examQueryRepository;
            this._examMarkQueryRepository = examMarkQueryRepository;
            this._studentClassQueryRepository = studentClassQueryRepository;
        }
        public async Task<PaginatedItemDTO<StudentMarksDTO>> Handle(GetExamMarksByFilterQuery request, CancellationToken cancellationToken)
        {
            var exam = (await _examQueryRepository.Query(x => x.AcademicYearId == request.AcademicYearId &&
                       x.Id == request.ExamId && x.SemesterId == request.Semester)).FirstOrDefault();

            if(exam != null)
            {
                var classStudentMarks = new List<StudentMarksDTO>();
                var totalRecordCount = 0;

                var classStudents = await _studentClassQueryRepository.Query(x => x.ClassNameId == request.ClassNameId &&
                                    x.AcademicYearId == request.AcademicYearId &&
                                    x.AcademicLevelId == request.AcademicLevelId);

                if (!string.IsNullOrEmpty(request.StudentName))
                {
                    var nameFilter = request.StudentName.ToLower().Trim();

                    classStudents = classStudents.Where(x => x.Student.User.FirstName.ToLower().Trim().Contains(nameFilter) ||
                                                            x.Student.User.LastName.ToLower().Trim().Contains(nameFilter));
                }

                totalRecordCount = classStudents.Count();

                var availableClassStudents = classStudents.OrderByDescending(x => x.StudentId)
                                       .Skip(request.CurrentPage * request.PageSize)
                                       .Take(request.PageSize)
                                       .ToList();

                foreach(var student in availableClassStudents)
                {
                    var examMark = (await _examMarkQueryRepository.Query(x => x.StudentId == student.StudentId &&
                                  x.ExamId == exam.Id && x.SubjectId == request.SubjectId && 
                                  x.AcademicLevelId == request.AcademicLevelId))
                                  .FirstOrDefault();

                    classStudentMarks.Add(new StudentMarksDTO()
                    {
                        Id = examMark != null ? examMark.Id : 0,
                        Marks = examMark != null ? examMark.Marks  : 0,
                        Grade = examMark != null ? ConfigureGrade(examMark.Marks) : "N/A",
                        StudentId = student.StudentId,
                        StudentName = $"{student.Student.User.FirstName} {student.Student.User.LastName}",
                        RegistrationNumber = student.Student.AdmissionNo,
                        ProfileImage = student.Student.User.ProfileImageUrl

                    });
                }


                return new PaginatedItemDTO<StudentMarksDTO>
                 (classStudentMarks, totalRecordCount, request.CurrentPage + 1, request.PageSize);


            }
            else
            {
                return new PaginatedItemDTO<StudentMarksDTO>
                 (new List<StudentMarksDTO>(), 0, 0, 0);
            }

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
