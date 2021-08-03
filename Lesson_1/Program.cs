using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace Lesson_1
{
    class Program
    {
        static readonly int fromIndex = 4;
        static readonly int toIndex = 13;
        static readonly string httpAdress = "https://jsonplaceholder.typicode.com/posts";
        static readonly string resultPath = "result.txt";
        static void Main(string[] args)
        {
            GetPostsAsync();
            Console.ReadLine();
        }
        static async void GetPostsAsync()
        {
            var tasks = new List<Task<Post>>();

            for (int i = 0; i < toIndex - fromIndex + 1; i++)
            {
                var index = fromIndex + i;
                tasks.Add(Task.Run(() => GetPost(index)));
            }

            await Task.WhenAll(tasks);

            File.WriteAllText(resultPath, "");
            foreach(Post item in tasks.Select(x => x.Result))
            {
                File.AppendAllText(resultPath, $"{item.userId}\n");
                File.AppendAllText(resultPath, $"{item.id}\n");
                File.AppendAllText(resultPath, $"{item.title}\n");
                File.AppendAllText(resultPath, $"{item.body}\n\n");
            }

            Console.WriteLine("Done");
        }
        static Post GetPost(int index)
        {
            var  client = new HttpClient();

            try
            {
                var response = client.GetAsync($"{httpAdress}/{index}").Result;

                response.EnsureSuccessStatusCode();

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<Post>(responseStream).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at index {index}: {ex.Message}");
            }

            return new Post { id = 99, userId = 99, body = "", title = ""};
        }
    }
}
