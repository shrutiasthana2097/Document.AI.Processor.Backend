using Document.Processor.AI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document.Processor.AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalyticsController : ControllerBase
    {
        private readonly ITextAnalyticsService _textAnalyticsService;
        
        public TextAnalyticsController(ITextAnalyticsService txtService)
        {
            _textAnalyticsService = txtService;
        }

        [Route("GetNamedEntities")]
        [HttpPost]
        public IActionResult GetNamedEntities([FromBody] GetNamedEntities input)
        {
            return Ok(_textAnalyticsService.GetNamedEntities(input.InputText));
        }

        [Route("GetKeyPhrases")]
        [HttpPost]
        public IActionResult GetKeyPhrases([FromBody] GetKeyPhrases input)
        {
            return Ok(_textAnalyticsService.GetKeyPhrases(input.InputText));
        }

        [Route("AnalyzeSentiments")]
        [HttpPost]
        public IActionResult AnalyzeSentiments([FromBody] AnalyzeSentiments input)
        {
            return Ok(_textAnalyticsService.AnalyzeSentiments(input.InputText));
        }

        [Route("GetPII")]
        [HttpPost]
        public IActionResult GetPII([FromBody] AnalyzeSentiments input)
        {
            return Ok(_textAnalyticsService.RecognizePIIEntities(input.InputText));
        }

        [Route("GetSummary")]
        [HttpPost]
        public IActionResult GetSummary([FromBody] AnalyzeSentiments input)
        {
            return Ok(_textAnalyticsService.ExtractSummary(input.InputText).Result);
        }

        [Route("GetHealthEntities")]
        [HttpPost]

        public IActionResult GetHealthcareEntities([FromBody] AnalyzeSentiments input)
        {
            return Ok(_textAnalyticsService.GetHealthcareEntities(input.InputText).Result);
        }
    }
}
