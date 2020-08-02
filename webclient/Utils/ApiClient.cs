using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoClient.Entities;

namespace TodoClient.Utils
{
    public class ApiClient
    {
        public HttpClient Client { get; }

        public string Url { get; set;  }

        public ApiClient(HttpClient client, string url = null)
        {
            Client = client;
            Url = url;
        }

        public async Task<T> GetAsync<T>(string path) => 
            await Client.GetFromJsonAsync<T>(new Uri($"{Url}/{path}", UriKind.Absolute));

        public async Task PostAsync<T>(string path, T entity)
        {
            await Client.PostAsJsonAsync(new Uri($"{Url}/{path}", UriKind.Absolute), entity);
        }
    }
}
