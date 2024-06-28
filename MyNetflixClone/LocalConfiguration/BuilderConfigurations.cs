using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyNetflixClone.Interfaces;
using MyNetflixClone.Services;
using RepoInterfaces;
using Repositories;
using ServiceInterfaces;
using Services;
using System.Text;

namespace MyNetflixClone.LocalConfiguration
{
    public static class BuilderConfigurations
    {
        public static void ConfigureDependency(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IUserRatingService, UserRatingService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddDbContext<NetflixDbContext>(options => options.UseSqlServer(connectionString));

            //AWS integration
            var awsAccessKey = builder.Configuration["AWS:AccessKey"];
            var awsSecretKey = builder.Configuration["AWS:SecretKey"];
            var awsBucketName = builder.Configuration["AWS:BucketName"];
            var awsRegion = builder.Configuration["AWS:Region"];

            var awsOptions = new Amazon.Extensions.NETCore.Setup.AWSOptions
            {
                Credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey),
                Region = Amazon.RegionEndpoint.GetBySystemName(awsRegion)
            };

            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.AddSingleton<IS3Service, S3Service>();

            // JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? jwtSettings["SecretKey"]; ;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

        }
    }
}
