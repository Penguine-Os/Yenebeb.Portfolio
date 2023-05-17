namespace BlazorApp.Models;

public class Repository
{
    public int id { get; set; }
    public string name { get; set; }
    public bool @private { get; set; }
    public string html_url { get; set; }
    public Dictionary<string, double> languages { get; set; }
    public List<string> topics { get; set; }
    
}