using Microsoft.AspNetCore.Http;

namespace App.Domain.ViewModels
{
    public class UploadManageVM
    {
        public IFormFile File { get; set; }
    }
}