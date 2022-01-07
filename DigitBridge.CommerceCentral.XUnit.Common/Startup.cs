using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Security.Claims;

namespace DigitBridge.CommerceCentral.XUnit.Common
{
    public class StartupTest
    {
        protected string AppName { get; set; }
        public IConfiguration Configuration { get; }

        public StartupTest(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.test.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddHttpContextAccessor();
            services.AddRouting();
            CreateDefaultHttpContextAccessor(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                return;
            });
        }

        public ClaimsPrincipal CreateClaim()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "Testing user"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = claimsPrincipal;
            return claimsPrincipal;
        }

        public HttpContext CreateDeafultHttpContext()
        {
            var request = new DefaultHttpContext().Request;
            request.Method = "GET";
            request.Path = new PathString("/Test");
            request.HttpContext.User = CreateClaim(); 
            return request.HttpContext;
        }

        public IHttpContextAccessor CreateDefaultHttpContextAccessor(IServiceCollection services)
        {
            var defaultHttpContext = CreateDeafultHttpContext();

            var contextAccessor = new HttpContextAccessor() { HttpContext = defaultHttpContext };
            services.AddSingleton<IHttpContextAccessor>(contextAccessor);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            contextAccessor.HttpContext.RequestServices = serviceProvider;
            return contextAccessor;
        }

    }
}
