using InventoryTracker.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using InventoryTracker.Infrastructure.EntityFramework;

namespace online_shop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dbConnectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            builder.Services.AddNpgsql<ApplicationDbContext>(dbConnectionString, options =>
            {
                options.MigrationsAssembly("InventoryTracker.Infrastructure.Entity.Framework");

            });

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(dbConnectionString);
                });




            builder.Services.AddControllers();


            var app = builder.Build();




            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}