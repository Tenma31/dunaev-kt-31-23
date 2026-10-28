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
            => Ok(await _specialtiesService.GetSpecialtiesAsync(filter, ct));

        [HttpPost("add")]
        public async Task<IActionResult> AddSpecialty([FromBody] Specialty specialty, CancellationToken ct)
            => Ok(await _specialtiesService.AddSpecialtyAsync(specialty, ct));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSpecialty([FromBody] Specialty specialty, CancellationToken ct)
            => Ok(await _specialtiesService.UpdateSpecialtyAsync(specialty, ct));

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSpecialty(int id, CancellationToken ct)
            => Ok(await _specialtiesService.DeleteSpecialtyAsync(id, ct));
    }
}