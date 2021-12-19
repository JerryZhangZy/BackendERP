using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.XUnit.Common
{
    public class TestFixture<TStartup> : IDisposable
        where TStartup : class
    {
        public readonly TestServer _server;
        public IConfiguration Configuration { get; }
        public string ConfigurationPath { get; }

        public IHttpContextAccessor HttpContextAccessor
        {
            get => CreateDefaultHttpContextAccessor();
        }
        public HttpContext DefaultHttpContext
        {
            get => HttpContextAccessor.HttpContext;
        }
        public ClaimsPrincipal ClaimsPrincipal
        {
            get => DefaultHttpContext.User;
        }

        public TestFixture()
        {
            //var projectDir = Directory.GetCurrentDirectory();
            //var configPath = Path.Combine(projectDir, "appsettings.test.json");
            ConfigurationPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.test.json");

            var builder = new WebHostBuilder()
                .UseStartup<TStartup>()
                .ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(ConfigurationPath);
                });

            _server = new TestServer(builder);

            //Client = _server.CreateClient();
            //Client.BaseAddress = new Uri("http://localhost:5000");

            Configuration = _server.Services.GetService<IConfiguration>();

            LoadConfiguration();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client?.Dispose();
            _server.Dispose();
        }

        public void LoadConfiguration(string path = null)
        {
            if (string.IsNullOrEmpty(path))
                path = ConfigurationPath;

            var others = File.ReadAllText(path);
            var jo = JObject.Parse(others);
            foreach (JProperty item in jo.Children())
            {
                if (item.Value.HasValues)
                {
                    foreach (JProperty node in item.Value.Children())
                    {
                        var key = item.Path + node.Name;
                        ConfigurationManager.AppSettings[key] = node.Value.ToString();
                    }
                }
                else
                    ConfigurationManager.AppSettings[item.Path] = item.Value.ToString();
            }
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

        public IHttpContextAccessor CreateDefaultHttpContextAccessor()
        {
            var defaultHttpContext = CreateDeafultHttpContext();
            var contextAccessor = new HttpContextAccessor() { HttpContext = defaultHttpContext };

            var services = new ServiceCollection();
            services.AddSingleton<IHttpContextAccessor>(contextAccessor);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            contextAccessor.HttpContext.RequestServices = serviceProvider;
            return contextAccessor;
        }
    }
}
