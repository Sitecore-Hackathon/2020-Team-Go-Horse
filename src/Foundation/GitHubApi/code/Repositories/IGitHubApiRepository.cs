using System.Collections.Generic;

namespace Foundation.GitHubApi.Repositories
{
    public interface IGitHubApiRepository
    {
        KeyValuePair<string,string> GetRootPage(string gitRoot);
        Dictionary<string, string> GetDocumentationPages(string gitRoot);
        Dictionary<string,string> GetContributors(string gitRoot);
        Dictionary<string,string> GetDownloads(string gitRoot);
    }
}