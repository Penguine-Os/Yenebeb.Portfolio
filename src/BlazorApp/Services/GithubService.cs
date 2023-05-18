using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml;

using BlazorApp.Models;
using Microsoft.AspNetCore.Hosting;
namespace BlazorApp.Services;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GithubService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<Repository>> GetPublicRepositories()
    {
        var url = "https://yenebeb-df-portfolio.netlify.app/.netlify/functions/githubRepos";
    
        try
        {
            var repositories = await _httpClient.GetFromJsonAsync<List<Repository>>(url);

            for (int i = 0; i < repositories?.Count; i++)
            {
                var repository = repositories[i];
                var totalBytes = repository.languages.Values.Sum();
                foreach (var language in repository.languages)
                {
                    repository.languages[language.Key] = language.Value / totalBytes;
                }
            }
            return repositories ?? new List<Repository>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new List<Repository>();
    }
   
}
