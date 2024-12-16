using System.Text.Json;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Practice_2
{
    public class Posts
    {
        private HttpClient httpClient;
        private const string url = "https://jsonplaceholder.typicode.com/posts";

        public Posts(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<JsonElement>> GetPosts()
        {
            var response = await httpClient.GetAsync("/api/posts");
            var body = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<IEnumerable<JsonElement>>(body);
            return posts;
        }

        public async Task<int> GetPosts2()
        {
            var response = await httpClient.GetAsync("/api/posts");
            var body = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<IEnumerable<PostClass>>(body);
            int postID = 0;
            foreach(var item in posts)
            {
                postID = item.id;
            }

            return postID;
        }
    }
}