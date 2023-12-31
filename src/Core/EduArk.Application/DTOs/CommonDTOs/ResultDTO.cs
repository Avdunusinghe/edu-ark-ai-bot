using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.DTOs.CommonDTOs
{
    public class ResultDTO
    {
        internal ResultDTO(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        internal ResultDTO(bool succeeded, string successMessage, int id = 0)
        {
            Succeeded = succeeded;
            SuccessMessage = successMessage;
            Id = id;
        }

        public int Id { get; set; }
        public bool Succeeded { get; set; }
        public string SuccessMessage { get; set; }
        public string[] Errors { get; set; }

        public static ResultDTO Success(string messeage, int id = 0)
        {
            return new ResultDTO(true, messeage, id);
        }

        public static ResultDTO Failure(IEnumerable<string> errors)
        {
            return new ResultDTO(false, errors);
        }

    }
}
