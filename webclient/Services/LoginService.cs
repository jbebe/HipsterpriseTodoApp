using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoClient.Data;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TodoClient.Utils;
using System.Text.Json.Serialization;
using System.Dynamic;

namespace TodoClient.Services
{
    public class LoginService
    {
        private NavigationManager Navigation { get; }

        private HttpClient Http { get; }
        
        private Settings Settings { get; }
        public IJSRuntime JSRuntime { get; }

        public User User { get; set; }

        public string LastError { get; set; } = null;

        public event EventHandler OnLoginSuccess;

        public LoginService(NavigationManager navigation, HttpClient http, Settings settings, IJSRuntime JSRuntime)
        {
            Navigation = navigation;
            Http = http;
            Settings = settings;
            this.JSRuntime = JSRuntime;
            JSRuntime.CustomLog($"{nameof(LoginService)} ctor");
        }

        public async Task DoLoginAsync()
        {
            try
            {
                User = await Http.GetFromJsonAsync<User>(
                    new Uri($"{Settings.ApiUrl}/login", UriKind.Absolute));
                LastError = null;
                JSRuntime.CustomLog($"User created, content: {System.Text.Json.JsonSerializer.Serialize(User)}");
                OnLoginSuccess.Invoke(null, null);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }
    }
}
