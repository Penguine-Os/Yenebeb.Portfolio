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
        var url = "https://api.github.com/user/repos?per_page=100";
        var apiKey = _configuration["api_key"];
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);
        var fetchedRepo = new List<Repository>();
        try
        {
            fetchedRepo =await  _httpClient.GetFromJsonAsync<List<Repository>>(url) ??  new List<Repository>();
            Console.WriteLine("-----------------------------fetched-----------------------------");
            Console.WriteLine(fetchedRepo.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            
        }
        string relativePath = "sample-data/Repositories.json";
        
        var repos = await _httpClient.GetFromJsonAsync<List<Repository>>(relativePath);
     

        return repos ?? new ();
    }
   
}