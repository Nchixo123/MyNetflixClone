using Extensions;
using Microsoft.OpenApi.Models;
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
                    builder.WithOrigins("http://localhost:3000")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseCors("CorsPolicy");


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

            app.MapControllers();
            app.Run();
        }
    }
}
