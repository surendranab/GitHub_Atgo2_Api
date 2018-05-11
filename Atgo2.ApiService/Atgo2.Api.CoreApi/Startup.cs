using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders;
using Atgo2.Api.BusinessLayer;
using Atgo2.Api.BusinessLayer.Interface;
using Atgo2.Api.Entity;

namespace Atgo2.Api.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }        
        public AppSettings appSettings { get; }

        public Startup(IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            appSettings = Configuration.Get<AppSettings>(); //"AppSettings"
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomHeaders();
            services.AddSingleton(appSettings);
            services.AddSingleton(typeof(IServices<>), typeof(Services<>));
            services.AddSingleton<IUserService, UserService>();
            services.AddMvc();

            //CORS Policy configurations
            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add(appSettings.settings.UiUrl);
            policy.SupportsCredentials = true;
            services.AddCors(x => x.AddPolicy("EnableCors", policy)).BuildServiceProvider();

            // Register the Swagger generator, defining one or more Swagger documents
            //services.AddSwaggerGen();
            //services.ConfigureSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1",
            //        new Info
            //        {
            //            Title = "Navvis Web Api",
            //            Version = "v1",
            //            Description = "Navvis Web Api - Patient Tracking System",
            //            TermsOfService = "None"
            //        }
            //    );
            //    options.OperationFilter<SwaggerAuthorizationFilter>();
            //    var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Navvis.PatientTrackingSystem.WebApi.xml");
            //    options.IncludeXmlComments(filePath);
            //    options.DescribeAllEnumsAsStrings();
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //xss and security settings configurations
            var policyCollection = new HeaderPolicyCollection()
               .AddFrameOptionsSameOrigin()    // prevent click-jacking
               .AddXssProtectionBlock()    // prevent cross-site scripting (XSS)
               .AddContentTypeOptionsNoSniff() // prevent drive-by-downloads
               .AddCustomHeader("Content-Security-Policy", "default-src 'none'; script-src 'self'; connect-src 'self'; img-src 'self'; style-src 'self';");
            app.UseSecurityHeaders(policyCollection);
            app.UseCustomHeadersMiddleware(policyCollection);
            //app.UseMiddleware<AppMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("EnableCors");
            app.UseMvc();
        }
    }
}
