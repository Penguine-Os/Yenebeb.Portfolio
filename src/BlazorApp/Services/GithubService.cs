using System.Net.Http.Headers;
using System.Net.Http.Json;

using BlazorApp.Models;

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
        string? token = Environment.GetEnvironmentVariable("Github_Api_Token");
       string? token2 = _configuration["Github_Api_Token"];
       Console.WriteLine("**********************************");
       Console.WriteLine(token);
       Console.WriteLine(token2);
       Console.WriteLine("**********************************"); 
       Console.WriteLine(_configuration["Github_Api_Key"]);
       Console.WriteLine(Environment.GetEnvironmentVariable("Github_Api_Key"));
       Console.WriteLine("**********************************"); 
       Console.WriteLine(_configuration["api_key"]);
       Console.WriteLine(Environment.GetEnvironmentVariable("api_key"));
       Console.WriteLine("**********************************");
       Console.WriteLine(_configuration["api_token"]);
       Console.WriteLine(Environment.GetEnvironmentVariable("api_token"));
       Console.WriteLine("**********************************");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        string baseUri = "https://api.github.com/user/repos?per_page=100";
        //  string uri = $"https://api.github.com/users/penguine-os/repos?Authorization=Bearer {token}&X-GitHub-Api-Version=2022-11-28&Accept=application/vnd.github+json&per_page =100";
        List<Repository> Repositories = new();
        var fetchedGithubRepos = await _httpClient.GetFromJsonAsync<List<GithubModel>>(baseUri);
        //var githubDynamicObject = await _httpClient.GetFromJsonAsync<List<GithubModel>>(uri);
        var githubDto = (fetchedGithubRepos ?? new())
            .Where(y => !y.@private)
            .Select(x => new Repository(
                name: x.name,
                repoLink: x.html_url,
                topics: x.topics,
                languageLink: x.languages_url))
            .ToList();
        try
        {
            for (int i = 0; i < githubDto.Count; i++)
            {
                if (fetchedGithubRepos != null)
                {
                    githubDto[i].Languages =
                        await _httpClient.GetFromJsonAsync<Dictionary<string, double>>(fetchedGithubRepos[i]
                            .languages_url) ?? new();
                }

                var totalBytes = githubDto[i].Languages.Sum(x => x.Value);
                foreach (var language in githubDto[i].Languages)
                {
                    githubDto[i].Languages[language.Key] = language.Value / totalBytes;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return githubDto;
    }
}