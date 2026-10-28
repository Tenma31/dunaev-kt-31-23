using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesService _specialtiesService;

        public SpecialtiesController(ISpecialtiesService specialtiesService)
        {
            _specialtiesService = specialtiesService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetSpecialties([FromBody] SpecialtiesFilter filter, CancellationToken ct)
        {
            var result = await _specialtiesService.GetSpecialtiesAsync(filter, ct);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSpecialty([FromBody] Specialty specialty, CancellationToken ct)
        {
            var result = await _specialtiesService.AddSpecialtyAsync(specialty, ct);
            return Ok(result);
        }
    }
}