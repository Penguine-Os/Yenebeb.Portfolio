namespace BlazorApp.Models;

public class Repository
{
    public string Name { get; set; }
    public string RepoLink { get; set; }
    public List<string> Topics { get; set; }
    public Dictionary<string, double> Languages { get; set; }
    public bool IsPrivate { get; set; }
    
}