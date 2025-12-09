using Microsoft.EntityFrameworkCore;

namespace RRHH.WEB.Extensions;

    public static class MigrationExtensions
    {
        public static void ApplyMigrations<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetRequiredService<TDbContext>();

                try
                {
                    logger.LogInformation("--> Trying to apply migrations...");


                        var retryCount = 0;
                        var maxRetries = 5;

                        while (retryCount < maxRetries)
                        {
                            try
                            {
                                if (context.Database.CanConnect())
                                {
                                    context.Database.Migrate();
                                    logger.LogInformation("--> Migrations applied.");
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                retryCount++;
                                logger.LogWarning($"--> Database unreachable. Retrying {retryCount}/{maxRetries} en 2 seconds...");
                                System.Threading.Thread.Sleep(2000);
                            }
                        }
                        
                        context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "--> ERROR: MIGRATIONS COULDN'T BE APPLIED");
                }
            }
        }
    }
