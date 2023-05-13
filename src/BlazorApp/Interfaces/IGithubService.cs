using BlazorApp.Models;

namespace BlazorApp;

public interface IGithubService
{
    
     Task<List<Repository>> GetPublicRepositories();
}