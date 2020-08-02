using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoClient.Data;
using TodoClient.Services;
using TodoClient.Utils;

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
            var apiClient = new ApiClient(httpClient);

            builder.Services.AddSingleton<HttpClient>(serviceProvider => httpClient);
            builder.Services.AddSingleton<ApiClient>(serviceProvider => apiClient);
            builder.Services.AddSingleton<Settings>();
            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<TodoStore>();

            var host = builder.Build();
            var settings = host.Services.GetRequiredService<Settings>();
            await settings.InitializeAsync(httpClient);
            apiClient.Url = settings.ApiUrl;

            await host.RunAsync();
        }
    }
}
