using System.Globalization;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using ResuMeta.Services.Abstract;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Humanizer.Bytes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using System.Runtime.InteropServices;
using Microsoft.Identity.Client;
using System.Text.Encodings.Web;
using System.Web;

namespace ResuMeta.Services.Concrete
{
    class line
    {
        public string text { get; set; }
        public List<int> style { get; set; }
    }
    class ImportedJsonPdf
    {
        public string filename { get; set; }
        public List<List<line>> content { get; set; }
    }
    class JsonPdf
    {
        public string pdf { get; set; }
    }
    public class NodeService : INodeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _nodeUrl;
        private readonly IRepository<Resume> _resumeRepo;
        private readonly IRepository<UserInfo> _userInfoRepo;
        public NodeService(HttpClient httpClient, IOptions<NodeServiceOptions> options, IRepository<Resume> resumeRepo, IRepository<UserInfo> userInfoRepo)
        {
            _httpClient = httpClient;
            _nodeUrl = options.Value.NodeUrl;
            _resumeRepo = resumeRepo;
            _userInfoRepo = userInfoRepo;
        }
        public async Task<string> ExportPdfAsync(string html)
        {
            var content = new Dictionary<string, string>
            {
                { "html", html }
            };
            var json = JsonSerializer.Serialize(content);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_nodeUrl + "pdfgenerator", stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                try{
                    var pdf = JsonSerializer.Deserialize<JsonPdf>(responseString);
                    return pdf.pdf;
                }
                catch (Exception e)
                {
                    throw new Exception("Error exporting PDF");
                
                }
            }
            else
            {
                throw new Exception("Error exporting PDF");
            }
        }

        public async Task ImportPdfAsync(IFormFile pdf, string userInfoId) //Need to accept userinfoId to append it to new resume
        {
            // route to 
            try
            {
                string endpoint = _nodeUrl + "importPdf";

                using var memoryStream = new MemoryStream();
                await pdf.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var content = new MultipartFormDataContent();
                content.Add(new StreamContent(memoryStream), "pdfFile", pdf.FileName);

                HttpResponseMessage res = await _httpClient.PostAsync(endpoint, content);

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error importing PDF");
                }

                ImportedJsonPdf importedJson = JsonSerializer.Deserialize<ImportedJsonPdf>(await res.Content.ReadAsStringAsync());
                if (importedJson.content.Count == 0) return;
                
                string htmlContent = string.Empty;
                foreach (List<line> line in importedJson.content)
                {
                    htmlContent += "<p>";
                    foreach (line chunk in line)
                    {
                        htmlContent += chunk.text;
                    }
                    htmlContent += "</p><p><br/></p>";
                }

                string urlEncodedContent = WebUtility.UrlEncode(htmlContent);
                string urlEncodedSafeSpaces = urlEncodedContent.Replace("+", "%20");

                Resume resume = new Resume
                {
                    UserInfo = _userInfoRepo.GetAll().Where(ui => ui.AspnetIdentityId == userInfoId).FirstOrDefault(),
                    Title = importedJson.filename,
                    Resume1 = urlEncodedSafeSpaces
                };

                _resumeRepo.AddOrUpdate(resume);
            }
            catch
            {
                return;
            }

        }
    }
}