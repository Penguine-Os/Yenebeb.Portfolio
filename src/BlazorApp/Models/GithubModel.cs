namespace BlazorApp.Models;

public class GithubModel
{
    public string name { get; set; }
    public bool @private { get; set; }
    public string html_url { get; set; }
    public string languages_url { get; set; }
    public List<string> topics { get; set; }
}
