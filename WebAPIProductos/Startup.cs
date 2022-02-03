using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace WebAPIProductos
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
            services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.IgnoreCycles);
            services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(opciones=>{
                opciones.AddDefaultPolicy( builder=>{
                    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(new string[]{"cantidadTotalRegistros"});
                });
            });
            services.AddAutoMapper(typeof(Startup));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
}