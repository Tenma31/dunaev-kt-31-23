using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplinesService _disciplinesService;

        public DisciplinesController(IDisciplinesService disciplinesService)
        {
            _disciplinesService = disciplinesService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetDisciplines([FromBody] DisciplinesFilter filter, CancellationToken cancellationToken)
        {
            var result = await _disciplinesService.GetDisciplinesAsync(filter, cancellationToken);
            return Ok(result);
        }
    }
}