using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
using Refit;
using RefitMicroservice.Services;

namespace RefitMicroservice
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
            var authToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2N2FkZmRmZWJmNTlmNDBiMDEwNmY2NWY4MDA0ZGQxMiIsInN1YiI6IjY1ZWM3ZTVmNmYzMWFmMDE2MWU3NTY0MyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.YU5maWyPVqp4dU2rsIsRfEx70OJWb3EPKDv8Tdn-rH4";
            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult(authToken),
            };

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RefitMicroservice", Version = "v1" });
            });
            services
                .AddRefitClient<ITmdbApi>(refitSettings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.themoviedb.org/3"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RefitMicroservice v1"));
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
