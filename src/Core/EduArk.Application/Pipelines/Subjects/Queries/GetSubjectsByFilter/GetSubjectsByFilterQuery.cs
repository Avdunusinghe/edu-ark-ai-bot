using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Queries.GetSubjectsByFilter
{
    public record GetSubjectsByFilterQuery(SubjectFilterDTO subjectFilter) : IRequest<PaginatedItemDTO<SubjectDetailsDTO>>
    {
    }

    public class GetSubjectsByFilterQueryHandler : IRequestHandler<GetSubjectsByFilterQuery, PaginatedItemDTO<SubjectDetailsDTO>>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
      

        public GetSubjectsByFilterQueryHandler(ISubjectQueryRepository _subjectQueryRepository)
        {
            this._subjectQueryRepository = _subjectQueryRepository;
           
        }
        public async Task<PaginatedItemDTO<SubjectDetailsDTO>> Handle(GetSubjectsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var subjectDetails = new List<SubjectDetailsDTO>();

                var listOfSubject = await _subjectQueryRepository.Query(x => x.IsActive);

                if(!string.IsNullOrEmpty(request.subjectFilter.Name))
                {
                    listOfSubject = listOfSubject.Where(x=>x.Name.Contains(request.subjectFilter.Name));
                }

                if(request.subjectFilter.SubjectStreamId > 0)
                {
                    listOfSubject = listOfSubject.Where(x=>x.SubjectStreamId == request.subjectFilter.SubjectStreamId);
                }

                totalRecordCount = listOfSubject.Count();

                var listOfAvailableSubjects = listOfSubject.OrderByDescending(x => x.CreatedDate)
                                          .Skip(request.subjectFilter.CurrentPage * request.subjectFilter.PageSize)
                                          .Take(request.subjectFilter.PageSize)
                                          .ToList();

                foreach(var subject in listOfAvailableSubjects)
                {
                    var subjectData = subject.ToSubjectDetailsDTO();
                    subjectData.ParentBasketSubjectName = await GetParentBasketSubjectName(subject.Id, cancellationToken);
                    subjectDetails.Add(subjectData);
                }

                return new PaginatedItemDTO<SubjectDetailsDTO>
                  (subjectDetails, totalRecordCount, request.subjectFilter.CurrentPage + 1, request.subjectFilter.PageSize);
            
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<string> GetParentBasketSubjectName(int parentBasketSubjectId, CancellationToken cancellationToken)
        {
            var subject = await _subjectQueryRepository.GetById(parentBasketSubjectId, cancellationToken);

            if (subject is null)
            {
                return string.Empty;
            }
            else
            {
                return subject.Name;
            }
        }

    }
}
