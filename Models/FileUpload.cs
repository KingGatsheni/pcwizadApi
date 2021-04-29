using Microsoft.AspNetCore.Http;

namespace Models
{
    public class FileUpload
    {
        internal object files;

        public IFormFile ImageFile { get; set; }
    }
}