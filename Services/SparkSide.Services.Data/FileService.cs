namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using SparkSide.Common;
    using SparkSide.Services.Data.Contracts;

    public class FileService : IFileService
    {
        public FileService()
        {
        }

        public async Task<string> UploadAsync(IFormFile inputFile, string folderName, string id, string path)
        {
            string blobPath = "";
            Directory.CreateDirectory($"{path}/{folderName}/");

            var extension = Path.GetExtension(inputFile.FileName).TrimStart('.');

            var physicalPath = $"{path}/{folderName}/{id}.{extension}";

            using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                await inputFile.CopyToAsync(fileStream);
            }

            string connectionString = ConfigurationStrings.BlobConnectionString;

            BlobContainerClient container = new BlobContainerClient(connectionString, folderName);
            await container.CreateIfNotExistsAsync();
            try
            {
                BlobClient blob = container.GetBlobClient($"{id}");

                using (FileStream file = File.OpenRead(physicalPath))
                {
                    await blob.UploadAsync(file);
                }
            }
            finally
            {
                //if (File.Exists(physicalPath))
                //{
                //    File.Delete(physicalPath);
                //}
            }

            return GlobalConstants.BlobStorageBaseUrl + folderName + "/" + id;
        }
    }
}
