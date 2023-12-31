using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterDataByAcademicLevelId
{
    public record GetSubjectMasterDataByAcademicLevelIdQuery(int academicLevelId) : IRequest<List<DropDownDTO>>
    {
    }

    public class GetSubjectMasterDataByAcademicLevelIdQueryHandler : IRequestHandler<GetSubjectMasterDataByAcademicLevelIdQuery, List<DropDownDTO>>
    {
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;

        public GetSubjectMasterDataByAcademicLevelIdQueryHandler(ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository)
        {
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetSubjectMasterDataByAcademicLevelIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = (await _subjectAcademicLevelQueryRepository
                        .Query(x => x.AcademicLevelId == request.academicLevelId))
                        .Select(x => new DropDownDTO()
                        {
                            Id = x.SubjectId,
                            Name = x.Subject.Name,

                        }).ToList();

                return subjects;
            }
            catch (Exception ex)
            {

                return new List<DropDownDTO>();
            }
           
        }
    }
}
