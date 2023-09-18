using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace whizlabblob
{
    class Program
    {
        static string whizlabconnstring = "DefaultEndpointsProtocol=https;AccountName=whizlab9090;AccountKey=2wT8sn4pOhr4VlaPiCr+22sKszqGsOTSNpmltdmi8VFaM3wt/hCMFgnVoSA0BRaMYTRDLB30zumetIdm/mmyRw==;EndpointSuffix=core.windows.net";
        static string whizlabcontainerName = "images";
        static string whizlabfilename = "sample.html";
        static string filepath="C:\\Work\\sample.html";
        static string downloadpath = "C:\\Work\\sample2.html";
        static async Task Main(string[] args)
        {
            //CreateContainer().Wait();
            //UploadFile().Wait();
            //ListBlobs().Wait();
            DownloadBlob().Wait();
            Console.WriteLine("Operation complete");
            Console.ReadKey();
        }

        static async Task CreateContainer()
        {
            // Create a connection to the storage account
            BlobServiceClient w_blobServiceClient = new BlobServiceClient(whizlabconnstring);
            // Create the container
            BlobContainerClient w_containerClient = await w_blobServiceClient.CreateBlobContainerAsync(whizlabcontainerName);
        }

        static async Task UploadFile()
        {
            // Create a connection to the storage account
            BlobServiceClient w_blobServiceClient = new BlobServiceClient(whizlabconnstring);
            // Get a handle to an existing container
            BlobContainerClient w_containerClient = w_blobServiceClient.GetBlobContainerClient(whizlabcontainerName);
            // Create a new reference to a new blob in the container
            BlobClient w_blobClient = w_containerClient.GetBlobClient(whizlabfilename);

            // Upload the local file to a file stream
            using FileStream w_uploadFileStream = File.OpenRead(filepath);
            // Upload the blob to the container
            await w_blobClient.UploadAsync(w_uploadFileStream, true);
            w_uploadFileStream.Close();
        }


        static async Task ListBlobs()
        {
            // Create a connection to the storage account
            BlobServiceClient w_blobServiceClient = new BlobServiceClient(whizlabconnstring);
            // Get a handle to an existing container
            BlobContainerClient w_containerClient = w_blobServiceClient.GetBlobContainerClient(whizlabcontainerName);
            
            // For each blob in the container, output the name of the blob
            await foreach (BlobItem w_blobItem in w_containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + w_blobItem.Name);
            }

        }

        static async Task DownloadBlob()
        {
            // Create a connection to the storage account
            BlobServiceClient w_blobServiceClient = new BlobServiceClient(whizlabconnstring);
            // Get a handle to an existing container
            BlobContainerClient w_containerClient = w_blobServiceClient.GetBlobContainerClient(whizlabcontainerName);
            // Get a handle to the blob
            BlobClient w_blob = w_containerClient.GetBlobClient(whizlabfilename);
            // Download the blob            
            BlobDownloadInfo w_blobdata = await w_blob.DownloadAsync();

            // Get the content into a file stream
            using (FileStream w_downloadFileStream = File.OpenWrite(downloadpath))
            {
                await w_blobdata.Content.CopyToAsync(w_downloadFileStream);
                w_downloadFileStream.Close();
            }


            // Read the new file
            using (FileStream w_downloadFileStream = File.OpenRead(downloadpath))
            {
                using var strreader = new StreamReader(w_downloadFileStream);
                string line;
                while ((line = strreader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
    }
}
