using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public class BlobUniversal
    {
        protected BlobContainerClient blobContainerClient;

        private BlobUniversal(BlobContainerClient client)
        {
            blobContainerClient = client;
        }

        public static async Task<BlobUniversal> CreateAsync(string containerName, string connectionString)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();
            return new BlobUniversal(containerClient);
        }

        public IList<string> GetBlobList()
        {
            var itemList= blobContainerClient.GetBlobs();
            var fileItemList = new List<string>();
            foreach (var blobItem in itemList)
            {
                fileItemList.Add(blobItem.Name);
            }
            return fileItemList;
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return blobContainerClient.GetBlobClient(blobName);
        }

        public async Task<bool> ExistBlob(string blobName)
        {
            return await GetBlobClient(blobName)?.ExistsAsync();
        }

        public async Task<byte[]> GetBlobAsync(string blobName)
        {
            var blob= GetBlobClient(blobName);
            if (blob == null) return null;
            using(var ms=new MemoryStream())
            {
                await blob.DownloadToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task DownloadBlobToFileAsync(string blobName,string filePath)
        {
            await GetBlobClient(blobName)?.DownloadToAsync(filePath);
        }

        public async Task DownloadBlobAsync(string blobName, Stream stream)
        {
            await GetBlobClient(blobName)?.DownloadToAsync(stream);
        }

        public async Task<byte[]> DownloadBlobAsync(string blobName)
        {
            using (var ms = new MemoryStream())
            {
                await DownloadBlobAsync(blobName, ms);
                return ms.ToArray();
            }
        }
        public async Task<string> DownloadBlobToStringAsync(string blobName)
        {
            var result = await DownloadBlobAsync(blobName);
            return result != null ? new BinaryData(result).ToString() : string.Empty;
        }

        public async Task UploadBlobByFileAsync(string blobName,string filePath,bool overwrite=true)
        {
            await GetBlobClient(blobName)?.UploadAsync(filePath,overwrite);
        }

        public async Task UploadBlobAsync(string blobName,byte[] buffer,bool overwrite=true)
        {
            using(var ms=new MemoryStream(buffer))
            {
                await UploadBlobAsync(blobName, ms,overwrite);
            }
        }

        public async Task UploadBlobAsync(string blobName,Stream stream,bool overwrite=true)
        {
            await GetBlobClient(blobName)?.UploadAsync(stream,overwrite);
        }

        public async Task UploadBlobAsync(string blobName, string content, bool overwrite = true)
        {
            await GetBlobClient(blobName)?.UploadAsync(new BinaryData(content), overwrite);
        }

        public async Task<bool> DeleteBlobAsync(string blobName)
        {
            return (await GetBlobClient(blobName)?.DeleteIfExistsAsync()).Value;

        }
    }
}
