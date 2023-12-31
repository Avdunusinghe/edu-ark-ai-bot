using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Exams.Queries.GetExamMasterData
{
    public record GetExamMasterDataQuery : IRequest<List<DropDownDTO>>
    {
        public int AcademicYearId { get; set; }
        public int ExamTypeId { get; set; }
        public int SemesterId { get; set; }
    }

    public class GetExamMasterDataQueryHandler : IRequestHandler<GetExamMasterDataQuery, List<DropDownDTO>>
    {
        private readonly IExamQueryRepository _examQueryRepository;

        public GetExamMasterDataQueryHandler(IExamQueryRepository examQueryRepository)
        {
            this._examQueryRepository = examQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetExamMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listOfExams = (await _examQueryRepository
                                  .Query(x => x.AcademicYearId == request.AcademicYearId &&
                                  x.ExamTypeId == request.ExamTypeId &&
                                  x.SemesterId == request.SemesterId))
                                  .Select(x => new DropDownDTO()
                                  {
                                      Id = x.Id,
                                      Name = x.Name

                                  }).ToList();


                return listOfExams;


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
