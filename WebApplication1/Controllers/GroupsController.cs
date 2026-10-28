using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetGroups([FromBody] GroupsFilter filter, CancellationToken cancellationToken)
        {
            var result = await _groupsService.GetGroupsAsync(filter, cancellationToken);
            return Ok(result);
        }
    }
}