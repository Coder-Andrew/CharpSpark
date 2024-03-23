using System.Runtime.CompilerServices;

namespace ResuMeta.Services.Abstract
{
    public interface INodeService
    {
        Task<string> ExportPdfAsync(string html);
        Task ImportPdfAsync(IFormFile pdf, string userId);
    }
}