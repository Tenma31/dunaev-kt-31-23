using WebApplication1.Services;

namespace WebApplication1
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupsService, GroupsService>();
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IDisciplinesService, DisciplinesService>();
            services.AddScoped<IGradesService, GradesService>();

            return services;
        }
    }
}