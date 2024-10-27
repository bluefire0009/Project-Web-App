using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;
using WebCalendaar.Services;
using Newtonsoft.Json;

namespace WebCalendaar
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Event e1 = new()
            {
                EndTime = TimeSpan.FromSeconds(60),
                Title = "AAA",
                Description = "BBB",
                Location = "SSs",
                Event_Attendances = new()
                {
                    new(){Event = null,Feedback = "Feed", User = new User() { FirstName = "John",LastName = "", Email = "", Password = "", RecuringDays = "", AttendanceIds = [], Event_Attendances = []}}
                }
            };
            JsonConvert.SerializeObject(e1);*/
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IAdminStorage, AdminDBStorage>();
            builder.Services.AddTransient<IAttendanceStorage, AttendanceDBStorage>();
            builder.Services.AddTransient<IEventStorage, EventDBStorage>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IUserStorage, UserDBStorage>();


            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteDb")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}