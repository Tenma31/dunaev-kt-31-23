using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGroupsService
    {
        Task<IEnumerable<Groups>> GetGroupsAsync(GroupsFilter filter, CancellationToken cancellationToken);
        Task<Groups> AddGroupAsync(Groups group, CancellationToken cancellationToken);
        Task<Groups> UpdateGroupAsync(Groups group, CancellationToken cancellationToken);
        Task<bool> DeleteGroupAsync(int id, CancellationToken cancellationToken);
    }
}