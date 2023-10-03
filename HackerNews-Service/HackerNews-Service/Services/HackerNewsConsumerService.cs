using HackerNews_Service.Controllers;
using HackerNews_Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HackerNews_Service.Services
{
    public class HackerNewsConsumerService
    {
        private readonly ILogger<HackerNewsConsumerService> _logger;
        private readonly IConfiguration _configuration;

        public HackerNewsConsumerService(ILogger<HackerNewsConsumerService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Stories[]?> GetStories()
        {
            try
            {

                var newstories = await GetLastestStories();
                foreach (var id in newstories.Stories)
                {
                    var story = await GetStoryById(id);
                }
                return new Stories[3];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurs");
            }

            return null;
        }


        private async Task<NewStories> GetLastestStories()
        {
            var url = $"{_configuration.GetValue<string>("HackerNewsUrl")}/newstories.json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var newStories = new NewStories();
                newStories.Stories = JsonSerializer.Deserialize<List<int>>(jsonResponse);
                return newStories;
            }
        }

        private async Task<Stories> GetStoryById(int id)
        {
            var url = $"{_configuration.GetValue<string>("HackerNewsUrl")}/item/{id}.json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var story = JsonSerializer.Deserialize<Stories>(jsonResponse);
                return story;
            }

            _logger($"Id [{id}], returns [{response.StatusCode}]");
        }
    }
}
