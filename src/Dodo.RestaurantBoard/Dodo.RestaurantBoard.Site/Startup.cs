using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dodo.RestaurantBoard.Site.Core
{
    public class Startup
    {
        private IContainer ApplicationContainer { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Setup localization
            void ConfigureOptions(RequestLocalizationOptions options)
            {
                var defaultCulture = "ru-RU";
                var cultureQueryStringKey = "language";
                var supportedCultureNames = new[] {defaultCulture, "en-US"};
                var supportedCultures = supportedCultureNames.Select(x => new CultureInfo(x)).ToList();

                options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(
                    context =>
                    {
                        var queriedCulture = context.Request.Query[cultureQueryStringKey].FirstOrDefault() ?? "";
                        var culture = supportedCultureNames.Contains(queriedCulture, StringComparer.InvariantCultureIgnoreCase)
                            ? queriedCulture
                            : defaultCulture;
                        return Task.FromResult(new ProviderCultureResult(culture));
                    }));
            }

            services.Configure<RequestLocalizationOptions>(ConfigureOptions);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.Name = ".RestaurantBoards.Session";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var container = AutofacConfig.Register(services, Configuration);
            ApplicationContainer = container;
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Boards}/{action=Index}/{id?}");
            });

            appLifetime.ApplicationStopped.Register(() => { ApplicationContainer.Dispose(); });
        }
    }
}