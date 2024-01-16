using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolarWatch.ApiConnect;
using SolarWatch.Authentication.Context;
using SolarWatch.DatabaseConnector;
using SolarWatch.Services;
using SolarWatch.Services.AuthService;
using SolarWatch.Services.DbService;
using SolarWatch.Services.TokenService;
using SolarWatch.Services.WeatherService;
using System.Text;

namespace SolarWatch;

public class StartUp
{
    public IConfiguration config { get; }

    public StartUp(IConfiguration configuration)
    {
        config = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "SolarWatch Api", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        services.AddLogging();
        services.AddSingleton<IWebClient, WebClient>();
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IJsonProcessor, JsonProcessor>();
        services.AddScoped<IDbService, DbService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddEntityFrameworkStores<UserContext>();
        var configuration = config.GetSection("Authentication");
        var x = configuration["ValidAudience"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["ValidateIssuer"],
                    ValidAudience = configuration["ValidateAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["IssuerSigningKey"]))
                };
            });

        try
        {
            var connectionString = config["ConnectionString"];
            var serverVersion = ServerVersion.Parse(config["ServerVersion"]);
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR", ex.Message);
        }


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


}
