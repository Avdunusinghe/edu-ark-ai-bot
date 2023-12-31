using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.SubjectStreams.Queries
{
    public record GetSubjectStreamsMasterDataQuery : IRequest<List<DropDownDTO>>
    {
    }

    public class GetSubjectStreamsMasterDataQueryHandler : IRequestHandler<GetSubjectStreamsMasterDataQuery, List<DropDownDTO>>
    {
        private readonly ISubjectStreamQueryRepository _subjectStreamQueryRepository;
        public GetSubjectStreamsMasterDataQueryHandler(ISubjectStreamQueryRepository subjectStreamQueryRepository)
        {
            this._subjectStreamQueryRepository = subjectStreamQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetSubjectStreamsMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _subjectStreamQueryRepository
                      .GetAll(cancellationToken))
                      .Select(x=> new DropDownDTO()
                      {
                           Id = x.Id,
                           Name = x.Name,

                      }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
