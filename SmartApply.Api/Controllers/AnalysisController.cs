using Microsoft.AspNetCore.Mvc;
using SmartApply.Api.Models.Request;
using SmartApply.Api.Services;

namespace SmartApply.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisController : ControllerBase
    {
        private readonly JobAnalysisService _analysisService;

        public AnalysisController(JobAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] AnalyzeRequest request)
        {
            var result = await _analysisService.AnalyzeAsync(request);
            return Ok(result);
        }
    }
}