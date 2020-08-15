using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiCore.Data.Models;

namespace WebApiCore.Api
{
   public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext <WebApiCoreContext>(builder =>
               builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebApiCore.Api"))
            );

            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<CurrentWeather>, WeatherRepository>();

            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { TypeFilterAttribute = "My API", Version = "vi" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           app.UseSwagger();

           app.UseSwaggerUI( c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
           });
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

   internal interface IRepository<T>
   {
      IEnumerable<Customer> All { get; }

      ActionResult<Customer> FindById(int id);
      void Update(Customer value);
      ActionResult<Customer> FindById(ActionResult<Customer> teamleadId);
      void Add(Customer value);
      void Delete(ActionResult<Customer> entity);
      void Update(CurrentWeather value);
      void Add(CurrentWeather value);
   }
}
