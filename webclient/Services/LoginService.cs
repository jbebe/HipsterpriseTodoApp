using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using TodoClient.Data;
using Microsoft.JSInterop;
using TodoClient.Utils;

namespace TodoClient.Services
{
    public class LoginService
    {
        private NavigationManager Navigation { get; }

        private ApiClient ApiClient { get; }
        
        private Settings Settings { get; }
        public IJSRuntime JSRuntime { get; }

        public User User { get; set; }

        public string LastError { get; set; } = null;

        public event EventHandler OnLoginSuccess;

        public LoginService(NavigationManager navigation, ApiClient apiClient, Settings settings, IJSRuntime JSRuntime)
        {
            Navigation = navigation;
            ApiClient = apiClient;
            Settings = settings;
            this.JSRuntime = JSRuntime;
            JSRuntime.CustomLog($"{nameof(LoginService)} ctor");
        }

        public async Task DoLoginAsync()
        {
            try
            {
                User = await ApiClient.GetAsync<User>("login");
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
