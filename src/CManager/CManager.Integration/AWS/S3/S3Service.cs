using Amazon;
using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;

namespace CManager.Integration.AWS.S3
{
    public static class S3Service
    {
        public static async Task<string> UploadImage(IFormFile file, string bucketName, string regionName)
        {
            if (file == null || file.Length == 0)
            return "No file selected.";

            using var client = new AmazonS3Client("AKIAXZ75UTGUTYWAYWYM", "O2Ocn6MxMx2jLb0SkIJt0u+3wo9N+ovbzh5p6crR", RegionEndpoint.USEast1);
            var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(client, bucketName);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = "uploads/" + fileName;

            using var stream = file.OpenReadStream();
            var request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = bucketName,
                Key = filePath,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await client.PutObjectAsync(request);

            return $"https://{bucketName}.s3.amazonaws.com/{filePath}";
        }
    }
}