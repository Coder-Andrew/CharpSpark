namespace ResuMeta.Services.Abstract
{
    public interface INodeService
    {
        Task<string> ExportPdfAsync(string html);
    }
}