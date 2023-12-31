namespace EduArk.Application.DTOs.CommonDTOs
{
    public class MasterDataUploadResponseDTO
    {
        public MasterDataUploadResponseDTO()
        {
            Results = new List<MasterDataFileValidateResultDTO>();
        }

        public bool IsSucccess { get; set; }
        public List<MasterDataFileValidateResultDTO> Results { get; set; }
    }

    public class MasterDataFileValidateResultDTO
    {
        public bool IsSuccess { get; set; }
        public string ValidateMessage { get; set; }
    }
}
