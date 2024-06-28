using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace UnitTests.Fixtures;

public class LocalDb : IDisposable
{
    public NetflixDbContext Context { get; private set; }

    public LocalDb()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

        var configuration = new ConfigurationBuilder()
                  .SetBasePath(AppContext.BaseDirectory) // Ensure correct base path
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();


        var options = new DbContextOptionsBuilder<NetflixDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        Context = new NetflixDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
