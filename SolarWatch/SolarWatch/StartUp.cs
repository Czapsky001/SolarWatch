using Microsoft.EntityFrameworkCore;
using SolarWatch.ApiConnect;
using SolarWatch.DatabaseConnector;
using SolarWatch.Services;

namespace SolarWatch
{
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
            services.AddSwaggerGen();
            services.AddLogging();
            services.AddSingleton<IWebClient, WebClient>();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IJsonProcessor, JsonProcessor>();
            services.AddScoped<IDbService, DbService>();


            try
            {
                var connectionString = config["ConnectionString"];
                var serverVersion = ServerVersion.Parse(config["ServerVersion"]);
                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            }
            catch(Exception ex)
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

            app.UseAuthentication();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
