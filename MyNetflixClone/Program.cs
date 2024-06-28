using Extensions;
using MyNetflixClone.LocalConfiguration;
using Serilog;

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
            app.MapControllers();

            app.Run();
        }
    }
}
