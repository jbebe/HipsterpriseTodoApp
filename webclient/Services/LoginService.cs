using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoClient.Data;

namespace TodoClient.Services
{
    public class LoginService
    {
        private NavigationManager Navigation { get; }

        private HttpClient Http { get; }
        
        private Settings Settings { get; }

        public User User { get; set; }

        public string LastError { get; set; } = null;

        public LoginService(NavigationManager navigation, HttpClient http, Settings settings)
        {
            Navigation = navigation;
            Http = http;
            Settings = settings;
        }

        public async Task DoLoginAsync()
        {
            try
            {
                var user = await Http.GetFromJsonAsync<User>(new Uri($"{Settings.ApiUrl}/login", UriKind.Absolute));
                LastError = null;
                Navigation.NavigateTo("home");
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }
    }
}
