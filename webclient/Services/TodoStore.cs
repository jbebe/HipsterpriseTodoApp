using System.Collections.Generic;
using System.Threading.Tasks;
using TodoClient.Data;
using TodoClient.Entities;
using TodoClient.Utils;

namespace TodoClient.Services
{
    public class TodoStore
    {
        public ApiClient ApiClient { get; }

        public TodoStore(ApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync(User user) => 
            await ApiClient.GetAsync<IEnumerable<TodoItem>>($"todo/{user.Email}");

        public async Task CreateTodoAsync(User user, TodoItem todo)
        {
            await ApiClient.PostAsync($"todo/{user.Email}", todo);
        }
    }
}
