using System;

namespace Foundation.GitHubApi.Models
{
    public class GitRepository
    {
        public int id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string html_url { get; set; }
        public string description { get; set; }
        public string collaborators_url { get; set; }
        public string contents_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime pushed_at { get; set; }
        public string language { get; set; }
        public bool has_downloads { get; set; }
        public bool has_wiki { get; set; }
        public int watchers { get; set; }
        public int subscribers_count { get; set; }
    }
}