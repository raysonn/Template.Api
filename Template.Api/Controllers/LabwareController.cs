using Microsoft.AspNetCore.Mvc;
using Template.Domain.Interfaces.Service;
using AmostrasCommand = Template.Domain.Commands.Resultados.AmostrasCommand;

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

        [HttpGet("v1/Authenticate")]
        public async Task<IActionResult> Auth() => Response(await _labWareService.Auth());

        [HttpPost("v1/SendResults")]
        public async Task<IActionResult> Resultados([FromBody] AmostrasCommand command) => Response(await _labWareService.Resultados(command));
        
        [HttpPost("v1/Close")]
        public async Task<IActionResult> Close([FromBody] string authToken) => Response(await _labWareService.Close(authToken)); 
        
        
        //[HttpPost("v1/SendSampleList")]
       // public async Task<IActionResult> SendSampleList([FromBody] List<Domain.Commands.Amostras.AmostrasCommand> command) => Response(await _labWareService.SendSampleList(command));
    }
}