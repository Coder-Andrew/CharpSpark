using System.Globalization;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using ResuMeta.Services.Abstract;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ResuMeta.Services.Concrete
{

    class JsonPdf
    {
        public string pdf { get; set; }
    }
    public class NodeService : INodeService
    {
        private readonly HttpClient _httpClient;
        public NodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> ExportPdfAsync(string html)
        {
            var content = new Dictionary<string, string>
            {
                { "html", html }
            };
            var json = JsonSerializer.Serialize(content);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:8080/pdfgenerator", stringContent);
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
    }
}