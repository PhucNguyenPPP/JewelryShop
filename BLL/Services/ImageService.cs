using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        public string ConvertToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                return base64String;
            }
        }

        public IFormFile ConvertToFormFile(string file)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new FormFile(stream, 0, stream.Length, "file", "");
            }
        }
    }
}
