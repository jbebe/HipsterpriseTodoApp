using System;
using System.Text.Json.Serialization;

namespace TodoClient.Entities
{
    public class TodoItem
    {
        [JsonPropertyName("author")]
        public string AuthorEmail { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [Obsolete]
        public TodoItem()
        {

        }

        public TodoItem(Data.User user, string content)
        {
            AuthorEmail = user.Email;
            Content = content;
        }
    }
}
