using Microsoft.AspNetCore.Mvc;
using Template.Domain.Commands.Amostras;
using Template.Domain.Commands.Resultados;
using Template.Domain.Interfaces.Service;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabwareController : BaseController
    {
        private readonly ILabWareService _labWareService;

        public LabwareController(ILabWareService labWareService)
        {
            _labWareService = labWareService;
        }

        [HttpGet("v1/Auth")]
        public async Task<IActionResult> Auth() => Response(await _labWareService.Auth());

        [HttpPost("v1/Resultados")]
        public async Task<IActionResult> Resultados([FromBody] AmostrasCommand command) => Response(await _labWareService.Resultados(command));

        [HttpPost("v1/Close")]
        public async Task<IActionResult> Close([FromBody] string authToken) => Response(await _labWareService.Close(authToken));


        [HttpPost("v1/SampleList")]
        public async Task<IActionResult> SendSampleList([FromBody] List<SampleCommand> command) => Response(await _labWareService.SendSampleList(command));
    }
}