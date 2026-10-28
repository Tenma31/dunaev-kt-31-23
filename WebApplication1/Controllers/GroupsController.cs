using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
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
        [HttpPost("add")]
        public async Task<IActionResult> AddGroup([FromBody] Groups group, CancellationToken ct)
    => Ok(await _groupsService.AddGroupAsync(group, ct));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGroup([FromBody] Groups group, CancellationToken ct)
            => Ok(await _groupsService.UpdateGroupAsync(group, ct));

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken ct)
            => Ok(await _groupsService.DeleteGroupAsync(id, ct));
    }
}