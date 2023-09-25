﻿using Azure.Storage;
using Azure.Storage.Blobs;
using AzureBlob1.Dtos;
using AzureBlob1.Helpers;

namespace AzureBlob1.Services;

public class FileService
{
    private readonly string _storageAccount = "databaza7730";
    private readonly string _key = "eMDy0vMBqb/3O6AQp8Oj3QVXdkARXtxRPZeq5Uqc1gtScDlZgQvvUrShcZNKByUsfDOwRSTKJdsx+AStk1Kxig==";

    private readonly BlobContainerClient _filesContainer;

   

    public FileService()
    {
       
        var credential = new StorageSharedKeyCredential(_storageAccount, _key);
        var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
        var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        _filesContainer = blobServiceClient.GetBlobContainerClient("bukontainer");
    }

    public async Task<List<BlobDto>> ListAsync()
    {
        List<BlobDto> files = new List<BlobDto>();
        await foreach (var file in _filesContainer.GetBlobsAsync())
        {
            string uri = _filesContainer.Uri.ToString();
            var name = file.Name;
            var fullUri = $"{uri}/{name}";

            files.Add(new BlobDto
            {
                Uri = fullUri,
                Name = name,
                ContentType = file.Properties.ContentType
            });
        }
        return files;
    }

    public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
    {
        BlobResponseDto response = new();
        BlobClient client = _filesContainer.GetBlobClient("video1");

        await using (Stream data = blob.OpenReadStream())
        {
            await client.UploadAsync(data);
        }

        response.Status = $"File {blob.FileName} Uploaded Successfully";
        response.Error = false;
        response.Blob.Uri = client.Uri.AbsoluteUri;
        response.Blob.Name = client.Name;

        return response;
    }



    public async Task<BlobDto?> DownlaodAsync(string blobFilename)
    {
        BlobClient file = _filesContainer.GetBlobClient(blobFilename);

        if (await file.ExistsAsync())
        {
            var data = await file.OpenReadAsync();
            Stream blobContent = data;

            var content = await file.DownloadContentAsync();

            string name = blobFilename;
            string contentType = content.Value.Details.ContentType;

            return new BlobDto { Content = blobContent, Name = name, ContentType = contentType };
        }

        return null;
    }

    public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
    {
        BlobClient file = _filesContainer.GetBlobClient(blobFilename);

        await file.DeleteAsync();

        return new BlobResponseDto { Error = false, Status = $"File: {blobFilename} has been successfully  deleted." };

    }



}
