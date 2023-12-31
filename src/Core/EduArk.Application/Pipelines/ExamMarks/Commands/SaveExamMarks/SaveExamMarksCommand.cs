using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.ExamMarkDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.ExamMarks.Commands.SaveExamMarks
{
    public record SaveExamMarksCommand() : IRequest<ResultDTO>
    {
        public ExamMarkContainerDTO ExamMarkContainer  { get; set; }
    }

    public class SaveExamMarksCommandHandler : IRequestHandler<SaveExamMarksCommand, ResultDTO>
    {
        private readonly IExamMarkQueryRepository _examMarkQueryRepository;
        private readonly IExamMarkCommandRepository _examMarkCommandRepository;

        public SaveExamMarksCommandHandler
        (
            IExamMarkQueryRepository examMarkQueryRepository,
            IExamMarkCommandRepository examMarkCommandRepository
        )
        {
            this._examMarkQueryRepository = examMarkQueryRepository;
            this._examMarkCommandRepository = examMarkCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveExamMarksCommand request, CancellationToken cancellationToken)
        {
            foreach(var itemOfExamMark in request.ExamMarkContainer.StudentMarks)
            {
                var examMark = await _examMarkQueryRepository.GetById(itemOfExamMark.Id,cancellationToken);

                if (examMark is null)
                {
                    examMark = new ExamMark()
                    {
                        StudentId = itemOfExamMark.StudentId,
                        SubjectId = request.ExamMarkContainer.SubjectId,
                        AcademicLevelId = request.ExamMarkContainer.AcademicLevelId,
                        Marks = itemOfExamMark.Marks,
                        ExamId = request.ExamMarkContainer.ExamId,
                        Grade = ConfigureGrade(itemOfExamMark.Marks)
                    };

                    await _examMarkCommandRepository.AddAsync(examMark, cancellationToken);
                }
                else
                {
                    examMark.StudentId = itemOfExamMark.StudentId;
                    examMark.SubjectId = request.ExamMarkContainer.SubjectId;
                    examMark.AcademicLevelId = request.ExamMarkContainer.AcademicLevelId;
                    examMark.Marks = itemOfExamMark.Marks;
                    examMark.Grade = ConfigureGrade(itemOfExamMark.Marks);

                    await _examMarkCommandRepository.UpdateAsync(examMark, cancellationToken);
                }
            }

            return ResultDTO.Success("Exam marks has been save successfully.");
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
