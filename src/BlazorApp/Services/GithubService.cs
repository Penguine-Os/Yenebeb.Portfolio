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
    {var url = "https://6464cffaba2b1c3a48827b12--portfolio-fuctions-typed.netlify.app/.netlify/functions/getRepos";
    
        try
        {
            var repositories = await _httpClient.GetFromJsonAsync<List<Repository>>(url);
            return repositories ?? new List<Repository>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new List<Repository>();
    }
   
}