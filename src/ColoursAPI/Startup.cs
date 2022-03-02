using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColoursAPI.Services;

namespace ColoursAPI
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
            services.AddSingleton<ColoursService>(new ColoursService(Configuration));
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ColoursAPI",
                    Version = "v1",
                    Description = "Grupo de operaciones de la aplicación ColoursApp. Aplicación con fines educativos de AprenderIT, que unifica y extiende las funcionalidades originalmente creadas por Mark Harrison (https://github.com/markharrison/ColourAPI/ y https://github.com/markharrison/ColoursWeb)",
                    TermsOfService = new Uri("https://github.com/AprenderIT/ColoursApp"),
                    Contact = new OpenApiContact
                    {
                        Name = "Pablo Di Loreto",
                        Email = "pdiloreto@aprender.it",
                        Url = new Uri("https://aprender.it"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT License",
                        Url = new Uri("https://github.com/AprenderIT/ColoursApp/blob/main/LICENSE"),
                    }
                }
                );

                c.EnableAnnotations();

                string strURL = Configuration.GetValue<string>("ServerURL");
                if (strURL != null && strURL != "")
                {
                    c.AddServer(new OpenApiServer()
                    {
                        Url = strURL
                    });
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
              builder.WithOrigins("http://localhost")
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ColoursAPI v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
