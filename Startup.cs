using dotnet.Models;
using dotnet.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using dotnet.Converter;
using System;
using dotnet.Helpers;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Hosting;
using dotnet.Data.Validation;

namespace dotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
                       {
                           options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
                       }).AddNewtonsoftJson(opts =>
              {
                  opts.SerializerSettings.Converters.Add(new OptionalConverter<Country>());
                  opts.SerializerSettings.Converters.Add(new OptionalConverter<Guest>());
                  opts.SerializerSettings.Converters.Add(new OptionalConverter<Place>());
                  opts.SerializerSettings.Converters.Add(new OptionalConverter<User>());
                  opts.SerializerSettings.Converters.Add(new OptionalConverter<Visit>());
              });


            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "client-app/build";
            // });
            services.AddMemoryCache();


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<DefaultdbContext>(options => options.UseMySql(Configuration.GetConnectionString("Database"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql")).UseSnakeCaseNamingConvention().LogTo(Console.WriteLine).EnableSensitiveDataLogging());

            services.AddScoped(typeof(CountryRepository), typeof(CountryRepository));
            services.AddScoped(typeof(GuestRepository), typeof(GuestRepository));
            services.AddScoped(typeof(VisitRepository), typeof(VisitRepository));
            services.AddScoped(typeof(UserRepository), typeof(UserRepository));
            services.AddScoped(typeof(PlaceRepository), typeof(PlaceRepository));

            services.AddAutoMapper(typeof(DefaultProfile));


            services.AddScoped<IListMapper, ListMapper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "client-app";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }
    }
}
