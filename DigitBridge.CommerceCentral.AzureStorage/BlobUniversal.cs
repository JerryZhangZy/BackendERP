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

        public async Task<byte[]> GetBlobAsync(string blobName)
        {
            var blob= blobContainerClient.GetBlobClient(blobName);
            using(var ms=new MemoryStream())
            {
                await blob.DownloadToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task DownloadBlobAsync(string blobName,string filePath)
        {
            var blob= blobContainerClient.GetBlobClient(blobName);
            await blob.DownloadToAsync(filePath);
        }

        public async Task UploadBlobAsync(string blobName,string filePath,bool overwrite=true)
        {
            var blob = blobContainerClient.GetBlobClient(blobName);
            await blob.UploadAsync(filePath,overwrite);
        }

        public async Task UploadBlobAsync(string blobName,byte[] buffer,bool overrite=true)
        {
            using(var ms=new MemoryStream(buffer))
            {
                await UploadBlobAsync(blobName, ms,overrite);
            }
        }

        public async Task UploadBlobAsync(string blobName,Stream stream,bool overwrite=true)
        {
            var blob = blobContainerClient.GetBlobClient(blobName);
            await blob.UploadAsync(stream,overwrite);
        }

        public async Task<bool> DeleteBlobAsync(string blobName)
        {
            var blob = blobContainerClient.GetBlobClient(blobName);
            return (await blob.DeleteIfExistsAsync()).Value;

        }
    }
}
