using InventoryTracker.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ������������ ��������
            ConfigureServices(builder);

            var app = builder.Build();

            // ������������ middleware
            ConfigureMiddleware(app);

            // ���������� �������� ��� ������� (������ ��� ����������)
            ApplyMigrationsOnStartup(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // ����������� ������������
            builder.Services.AddControllers();

            // ��������� ������ ����������� � ���������
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json");
            }

            // ����������� DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("InventoryTracker.Infrastructure.Entity.Framework");
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                });

                // ����������� ������ � Development
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                    options.LogTo(Console.WriteLine, LogLevel.Information);
                }
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ApplyMigrationsOnStartup(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                if (app.Environment.IsDevelopment())
                {
                    // �������������� ���������� �������� � Development
                    dbContext.Database.Migrate();
                }
                else
                {
                    // ������ �������� � Production
                    if (!dbContext.Database.CanConnect())
                    {
                        throw new Exception("Could not connect to the database");
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database");

                if (app.Environment.IsProduction())
                {
                    throw; // ��������� ���������� � Production
                }
            }
        }
    }
}