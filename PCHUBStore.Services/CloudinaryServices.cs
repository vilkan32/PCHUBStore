using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryServices(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await pictureFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "profile_pictures",
                    File = new FileDescription(fileName, ms)
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
