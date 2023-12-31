using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.Pipelines.Classes.Queries.GetClassMasterData;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetTeacherClassesMasterData
{
    public record GetTeacherClassesMasterDataQuery : IRequest<TeacherClassesMasterDataDTO>;
   
    public class GetTeacherClassesMasterDataQueryHandler : IRequestHandler<GetTeacherClassesMasterDataQuery, TeacherClassesMasterDataDTO>
    {
        private readonly IMediator _mediator;
        private readonly ISubjectTeacherQueryRepository _subjectTeacherQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISemesterQueryRepository _semesterQueryRepository;
        private readonly IExamTypeQueryRepository _examTypeQueryRepository;
        public GetTeacherClassesMasterDataQueryHandler
        (
            IMediator mediator, 
            ISubjectTeacherQueryRepository subjectTeacherQueryRepository, 
            ICurrentUserService currentUserService,
            ISemesterQueryRepository semesterQueryRepository,
            IExamTypeQueryRepository examTypeQueryRepository
        )
        {
            this._mediator = mediator;
            this._subjectTeacherQueryRepository = subjectTeacherQueryRepository;
            this._currentUserService = currentUserService;
            this._semesterQueryRepository = semesterQueryRepository;
            this._examTypeQueryRepository = examTypeQueryRepository;
        }
        public async Task<TeacherClassesMasterDataDTO> Handle(GetTeacherClassesMasterDataQuery request, CancellationToken cancellationToken)
        {
            var masterData = new TeacherClassesMasterDataDTO();

            var classMasterData = await _mediator.Send(new GetClassMasterDataQuery(), cancellationToken);

            masterData.CurrentAcademicYear = classMasterData.CurrentAcademicYear;
            masterData.AcademicLevels = classMasterData.AcademicLevels;
            masterData.AcademicYears = classMasterData.AcademicYears;
            masterData.ClassCategories = classMasterData.ClassCategories;
            masterData.LanguageStreams = classMasterData.LanguageStreams;
            masterData.ClassNames = classMasterData.ClassNames;

            masterData.Subjects = (await _subjectTeacherQueryRepository
                                 .Query(x => x.IsActive == true && x.TeacherId == _currentUserService.UserId.Value))
                                 .Select(subject => new DropDownDTO()
                                 {
                                     Id = subject.Subject.Id,
                                     Name = subject.Subject.Name
                                 }).ToList();

            masterData.Semesters = (await _semesterQueryRepository.GetAll(cancellationToken))
                                   .Select(x => new DropDownDTO()
                                   {
                                       Id = x.Id,
                                       Name = x.Name,
                                   }).ToList();

            masterData.ExamTypes = (await _examTypeQueryRepository
                                         .GetAll(cancellationToken))
                                         .Select(x => new DropDownDTO()
                                         {
                                             Id = x.Id,
                                             Name = x.Name
                                         }).ToList();

            return masterData;
        }
    }
}
