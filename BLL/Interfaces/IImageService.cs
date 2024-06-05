using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        public string ConvertToBase64(IFormFile file);
        public IFormFile ConvertToFormFile(string file);
    }
}
