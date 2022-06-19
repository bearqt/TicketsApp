using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketsApp.Data;
using TicketsApp.Data.Services;
using TicketsApp.JsonExtensions;
using TicketsApp.Middlewares;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace TicketsApp
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance);
            services.AddApiVersioning();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<ITicketValidator, TicketValidator>();
            services.AddDbContext<TicketsDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("TicketsDatabase"));
            });

            services.AddScoped<ITicketsService, TicketsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseErrorHandler();
            }
            
            app.UseRouting();
            app.UseValidateJsonSizeMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}