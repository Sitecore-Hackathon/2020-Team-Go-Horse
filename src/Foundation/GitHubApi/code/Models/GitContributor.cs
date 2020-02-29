namespace Foundation.GitHubApi.Models
{
    public class GitContributor
    {
        public string login { get; set; }
        public string avatar_url { get; set; }
        public string html_url { get; set; }
        public int contributions { get; set; }
    }
}