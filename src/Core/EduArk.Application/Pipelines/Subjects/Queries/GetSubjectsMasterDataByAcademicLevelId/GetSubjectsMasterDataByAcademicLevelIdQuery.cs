using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Queries.GetSubjectsMasterDataByAcademicLevelId
{
    public record GetSubjectsMasterDataByAcademicLevelIdQuery(int academicLevelId) : IRequest<List<DropDownDTO>>;
   
    public class GetSubjectsMasterDataByAcademicLevelIdQueryHandler
        : IRequestHandler<GetSubjectsMasterDataByAcademicLevelIdQuery, List<DropDownDTO>>
    {
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;

        public GetSubjectsMasterDataByAcademicLevelIdQueryHandler(ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository)
        {
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetSubjectsMasterDataByAcademicLevelIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = (await _subjectAcademicLevelQueryRepository
                                .Query(x => x.AcademicLevelId == request.academicLevelId)).Select(x => new DropDownDTO()
                                {
                                    Id = x.Subject.Id,
                                    Name = x.Subject.Name,
                                }).ToList();

                return subjects;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
