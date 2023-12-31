using EduArk.Application.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Http;

namespace EduArk.Application.Common.Interfaces
{
    public interface IAzureBlobStorageService
    {
        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDTO> UploadAsync(BlobContainerDTO blobContainer);
    }
}
