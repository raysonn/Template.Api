using Microsoft.AspNetCore.Mvc;
using Template.Domain.Commands;
using Template.Domain.Interfaces.Service;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : BaseController
    {
        private readonly ILogger<Controller> _logger;
        private readonly ITemplateService _templateService;

        public TemplateController(ILogger<Controller> logger, ITemplateService templateService)
        {
            _logger = logger;
            _templateService = templateService;
        }

        [HttpGet("v1/GetAll")]
        public async Task<IActionResult> GetAll() => Response(await _templateService.GetAll());

        [HttpGet("v1/GetById")]
        public async Task<IActionResult> Get(int id) => Response(await _templateService.GetById(id));

        [HttpPost("v1/Insert")]
        public async Task<IActionResult> Insert([FromBody] TemplateCommand command) => Response(await _templateService.Insert(command));

        [HttpPut("v1/Update")]
        public async Task<IActionResult> Update([FromBody] TemplateCommand command) => Response(await _templateService.Update(command));

        [HttpDelete("v1/Delete")]
        public async Task<IActionResult> Delete(int id) => Response(await _templateService.Delete(id));
    }
}