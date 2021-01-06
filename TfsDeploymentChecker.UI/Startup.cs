using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.BusinessLogic.Implementations;

namespace TfsDeploymentChecker.UI
{
    public class Startup
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            webHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                if (webHostEnvironment.IsDevelopment())
                { 
                    o.DetailedErrors = true;
                }
            });
            services.AddSingleton<ITfsClient, TfsClient>();
            services.AddSingleton<IConfigurationRetriever, ConfigurationRetriever>();
            services.AddTransient<IEnvironmentDeployedGetter, EnvironmentDeployedGetter>();
            services.AddTransient<IReleaseEnvironmentsGetter, ReleaseEnvironmentsGetter>();
            services.AddTransient<IReleaseInfoCoordinator, ReleaseInfoCoordinator>();
            services.AddTransient<IHealthCheckPerformer, HealthCheckPerformer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
