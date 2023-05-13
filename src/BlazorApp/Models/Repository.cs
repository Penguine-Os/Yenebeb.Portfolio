namespace BlazorApp.Models;

public class Repository
{
    public string Name { get; set; }
    public string RepoLink { get; set; }
    public List<string> Topics { get; set; }
    public Dictionary<string, double> Languages { get; set; }
    private string LanguageLink { get; set; }
    public Repository(string name, string repoLink, List<string> topics,string languageLink)
    {
        Name = name;
        RepoLink = repoLink;
        Topics = topics;
        LanguageLink = languageLink;
    }

    public Repository(Dictionary<string, double> languages, string languageLink)
    {
        Languages = languages;
        LanguageLink = languageLink;
    }
}