using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Services
{
    public interface IUploadHandler<T>
    {
        string UploadFile(IFormFile file);
        void DeleteFile(string filePath);
        Task<List<T>> ReadDetailsFromExcel(string filePath);
    }
}