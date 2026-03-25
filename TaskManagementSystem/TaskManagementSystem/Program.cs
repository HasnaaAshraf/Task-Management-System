using Microsoft.EntityFrameworkCore;
using TaskManagement.Business.Services;
using TaskManagement.Data.Contracts.RepositoryInterfaces;
using TaskManagement.Data.Contracts.ServicesInterfaces;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


			builder.Services.AddDbContext<TaskDBContext>(options =>
	        options.UseSqlServer(
	     	builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", options =>
            {
            options.LoginPath = "/User/Login";
            options.AccessDeniedPath = "/User/Login";
            options.Cookie.Name = "TaskManagementCookie";
            });

			builder.Services.AddScoped<ITaskService, TaskService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<ITaskRepository, TaskRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();


			var app = builder.Build();

			app.UseStaticFiles();

			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
         
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

			app.MapControllerRoute(
	        name: "default",
	        pattern: "{controller=User}/{action=Login}/{id?}");

			app.Run();
        }
    }
}
