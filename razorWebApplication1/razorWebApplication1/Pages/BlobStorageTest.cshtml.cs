using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace razorWebApplication1.Pages
{
    public class BlobStorageTestModel(IConfiguration configuration) : PageModel
    {
        // Insert into Azure Queue Storage
        private const string connectionString = "";
        private const string containerName = "ko-container";

        public async Task<IActionResult> OnPostAsync(IFormFile formFile)
        {
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(formFile.FileName);
            using (var stream = formFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return RedirectToPage("BlobStorageTest");
        }
    }
}
