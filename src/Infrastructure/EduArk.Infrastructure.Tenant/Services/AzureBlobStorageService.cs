using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EduArk.Infrastructure.Tenant.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<AzureBlobStorageService> _logger;

        public AzureBlobStorageService
            (IConfiguration configuration, ILogger<AzureBlobStorageService> logger)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }
        public async Task<BlobResponseDTO> UploadAsync(BlobContainerDTO blobContainer)
        {
            
            var fileUploadResponse = new BlobResponseDTO();

            var blobContainerData = blobContainer.Files.FirstOrDefault();

            var container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
           
            try
            {
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                var blob = container
                         .GetBlobClient(blobContainerData?.FileName);

                await blob
                     .DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                await blob
                      .UploadAsync(blobContainerData?.OpenReadStream(), 
                                       new BlobHttpHeaders 
                                       { 
                                           ContentType = blobContainerData?.ContentType 
                                       });
            
                fileUploadResponse.Status = $"File {blobContainerData.FileName} Uploaded Successfully";
                fileUploadResponse.Error = false;
                fileUploadResponse.Url = blob.Uri.ToString();
               

            }
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blobContainerData.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                fileUploadResponse.Status = $"File with name {blobContainerData.FileName} already exists. Please use another name to store your file.";
                fileUploadResponse.Error = true;
                return fileUploadResponse;
            }
         
            catch (RequestFailedException ex)
            {
               
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                fileUploadResponse.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                fileUploadResponse.Error = true;
                return fileUploadResponse;
            }

            
            return fileUploadResponse;

        }
    }
}
