using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradesService _gradesService;

        public GradesController(IGradesService gradesService)
        {
            _gradesService = gradesService;
        }

        [HttpPost("average-by-group")]
        public async Task<IActionResult> GetAverageByGroup([FromBody] GradesFilter filter, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(filter.GroupName))
                return BadRequest("GroupName is required");

            var result = await _gradesService.GetAverageGradeByGroupAsync(filter.GroupName, cancellationToken);
            return Ok(new { AverageGrade = result });
        }
    }
}