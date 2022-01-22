using Localiza.Frotas.Domain;
using Localiza.Frotas.Infra.Facade;
using Localiza.Frotas.Infra.Repository;
using Localiza.Frotas.Infra.Repository.EF;
using Localiza.Frotas.Infra.Singleton;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Localiza.Frotas
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
            services.AddSwaggerGen(c =>
           {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Localiza.Frotas",
                    Description = "API - Frotas",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Localiza.Frotas.xml");
                c.IncludeXmlComments(apiPath);

            });
            services.AddSingleton<SingletonContainer>();

            services.AddTransient<IVeiculoRepository, FrotaRepository>();
            services.AddDbContext<FrotaContext>(opt => opt.UseInMemoryDatabase("Frota"));
            services.AddTransient<IVeiculoDetran, VeiculoDetranFacade>();
            services.Configure<DetranOptions>(Configuration.GetSection("DetranOptions"));

            services.AddHttpClient();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Locatiza.Frotas");
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
