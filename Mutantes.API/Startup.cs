using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Newtonsoft.Json;
using System.Configuration;

namespace Mutantes.API
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services, IConfiguration  configuration)
        {
            services.AddDbContext<MutantsDbContext>(optionsBuilder =>
               optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"))); 

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                       
            services.AddTransient<IStatsService, StatsService>();
            services.AddTransient<IDnaAnalyzerService, DnaAnalyzerService>();
            services.AddTransient<IMatrixUtilities, MatrixUtilities>();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
}
