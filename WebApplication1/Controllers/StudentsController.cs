using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetStudents([FromBody] StudentsFilter filter, CancellationToken cancellationToken)
        {
            var result = await _studentsService.GetStudentsAsync(filter, cancellationToken);
            return Ok(result);
        }
        [HttpGet("specialties-by-lastname")]
        public async Task<IActionResult> GetSpecialtiesByLastName([FromQuery] string lastName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return BadRequest("Фамилия не может быть пустой");

            var result = await _studentsService.GetSpecialtiesByStudentLastNameAsync(lastName, cancellationToken);
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] Student student, CancellationToken ct)
    => Ok(await _studentsService.AddStudentAsync(student, ct));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student, CancellationToken ct)
            => Ok(await _studentsService.UpdateStudentAsync(student, ct));

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken ct)
            => Ok(await _studentsService.DeleteStudentAsync(id, ct));
    }
}