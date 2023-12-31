using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.AcademicLevelDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.AcademicLevels.Queries.GetAcademicLevelsByFilter
{
    public record GetAcademicLevelsByFilterQuery(AcademicLevelFilterDTO academicLevelFilter) 
            : IRequest<PaginatedItemDTO<AcademicLevelDetailsDTO>>;

    public class GetAcademicLevelsByFilterQueryHandler 
            : IRequestHandler<GetAcademicLevelsByFilterQuery, PaginatedItemDTO<AcademicLevelDetailsDTO>>
    {
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;

        /// <summary>
        /// Constructor for GetAcademicLevelsByFilterQueryHandler class
        /// </summary>
        /// <param name="academicLevelQueryRepository">Academic level query repository</param>
        public GetAcademicLevelsByFilterQueryHandler(IAcademicLevelQueryRepository academicLevelQueryRepository)
        {
            this._academicLevelQueryRepository = academicLevelQueryRepository;
        }

        /// <summary>
        /// Handles the GetAcademicLevelsByFilterQuery request
        /// </summary>
        /// <param name="request">GetAcademicLevelsByFilterQuery request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>PaginatedItemDTO containing the filtered academic levels</returns>
        public async Task<PaginatedItemDTO<AcademicLevelDetailsDTO>> Handle(GetAcademicLevelsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var availableAcademicLevels = new List<AcademicLevelDetailsDTO>();

                var listOfAcademicLevels = await FilterAcademicLevels(request.academicLevelFilter, cancellationToken);


                totalRecordCount = listOfAcademicLevels.Count();

                var availableDateSet = listOfAcademicLevels
                                           .OrderByDescending(x => x.CreatedDate)
                                           .Skip(request.academicLevelFilter.CurrentPage * request.academicLevelFilter.PageSize)
                                           .Take(request.academicLevelFilter.PageSize)
                                           .Select(academicLevel => academicLevel.ToAcademicLevelDetailsDto())
                                           .ToList();


                return new PaginatedItemDTO<AcademicLevelDetailsDTO>
                           (   availableDateSet,
                               totalRecordCount,
                               request.academicLevelFilter.CurrentPage + 1,
                               request.academicLevelFilter.PageSize
                           );



            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<IEnumerable<AcademicLevel>> FilterAcademicLevels(AcademicLevelFilterDTO filter, CancellationToken cancellationToken)
        {
            var listOfAcademicLevel = await _academicLevelQueryRepository.Query(x => x.IsActive == true);

     
            if (!string.IsNullOrEmpty(filter.Name))
            {
                listOfAcademicLevel = listOfAcademicLevel
                                     .Where(x => x.Name.Contains(filter.Name));
            }

            if (filter.LevelHeadId > 0)
            {
                listOfAcademicLevel = listOfAcademicLevel
                                     .Where(x => x.LevelHeadId == filter.LevelHeadId);
            }



            return listOfAcademicLevel;
        }
    }
}
