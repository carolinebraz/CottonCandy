using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Entities.ValueObject;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Core
{
    public class StorageHelper : IStorageHelper
    {
        private string _AccountName;
        private string _AccountKey;
        private string _ImageContainer;

        public StorageHelper(IConfiguration configuration)
        {
            _AccountName = configuration.GetSection("AccountName").Value;
            _AccountKey = configuration.GetSection("AccountKey").Value;
            _ImageContainer = configuration.GetSection("ImageContainer").Value;
        }

        public async Task<ImageBlob> Upload(Stream stream, string nameFile)
        {
            var newNameFile = $"{Guid.NewGuid()}.{nameFile.Split('.')[1]}";
            var url = await UploadFileToStorage(stream, newNameFile, _AccountName, _AccountKey, _ImageContainer);

            return new ImageBlob()
            {
                Url = url,
            };
        }

        private static async Task<string> UploadFileToStorage(Stream fileStream, string fileName, string accountName, string accountKey, string imageContainer)
        {
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(imageContainer);
            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(fileStream);

            return blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
        }

        public bool IsImage(string nameFile)
        {
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => nameFile.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }

}