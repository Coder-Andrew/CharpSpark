using ResuMeta.Models;
using ResuMeta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using System;
using System.Net;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ResuMeta.Controllers
{

    [Route("api/")]
    [ApiController]
    public class NodeApiController : ControllerBase
    {
        private readonly INodeService _nodeService;
        private readonly ILogger<NodeApiController> _logger;

        public NodeApiController(INodeService nodeService, ILogger<NodeApiController> logger)
        {
            _nodeService = nodeService;
            _logger = logger;
        }

        // POST: api/exportPdf/{html}
        [HttpPost("exportPdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportPdf([FromBody] JsonElement htmlContent)
        {
            try
            {
                var pdf = await _nodeService.ExportPdfAsync(htmlContent.ToString());
                return Ok(pdf);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error exporting PDF");
                return BadRequest();
            }
        }

        // POST: api/importPdf/
        [HttpPost("importPdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportPdf([FromForm] IFormFile pdfFile)
        {
            try
            {
                // get user id from User object
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                
                await _nodeService.ImportPdfAsync(pdfFile, userId);
            }
            catch
            {
                return Ok();
            }
            return Ok();
        }
    }
}