using Extensions;
using MyNetflixClone.LocalConfiguration;
using Repositories;
using Serilog;
using TestDataLibrary;

namespace MyNetflixClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.ConfigureDependency();
            SerilogConfig.ConfigureLogging(builder.Configuration);
            builder.Host.UseSerilog();
            builder.Services.AddTransient<GlobalExceptionHandler>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials();
                });
            });

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyNetflixClone v1");
                });
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.MapControllers();
            app.UseCors();


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<NetflixDbContext>();
                TestData.Seed(context).Wait();
            }
            catch (Exception ex)
            {

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An Error Has Occured During Migration");
            }


            app.Run();
        }
    }
}
