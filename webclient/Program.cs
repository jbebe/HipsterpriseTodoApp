using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoClient.Data;
using TodoClient.Services;

namespace TodoClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            builder.Services.AddTransient(sp => httpClient);
            builder.Services.AddSingleton<Settings>();
            builder.Services.AddSingleton<LoginService>();

            var host = builder.Build();

            var settings = host.Services.GetRequiredService<Settings>();
            await settings.InitializeAsync(httpClient);

            await host.RunAsync();
        }
    }
}
