using Document.Processor.AI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document.Processor.AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerVisionController : ControllerBase
    {
        private readonly IComputerVision _vision;
        public ComputerVisionController(IComputerVision vision)
        {
            _vision = vision;
        }

        [Route("GetOCRText")]
        [HttpPost]
        public IActionResult GetOCRText([FromForm] GetOCRInput input)
        {
            MemoryStream ms = new MemoryStream();
            input.InputText.CopyToAsync(ms);
            ms.Position = 0;
            return Ok(_vision.OCRText(ms));
        }
    }
}
