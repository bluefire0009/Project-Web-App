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
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                policy =>
                                {
                                    policy.WithOrigins("*");
                                });
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
            builder.Services.AddScoped<IEventAttendanceStorage, EventAttendanceDBStorage>();


            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteDb")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                await next(); // Call the next middleware
                Console.WriteLine($"Response: {context.Response.StatusCode}");
            });

            app.Run();

        }
    }
}