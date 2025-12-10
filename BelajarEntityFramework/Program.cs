using BelajarEntityFramework.GenericRepository;
using BelajarEntityFramework.Models;
using BelajarEntityFramework.Repository;
using BelajarEntityFramework.Services;
using BelajarEntityFramework.UOW;
using CRUDinCoreMVC.UOW;
using Microsoft.EntityFrameworkCore;

namespace BelajarEntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EFDbConnection"));
            });

            // Registering Repository
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employees}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
