using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface ICloudinaryServices
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
