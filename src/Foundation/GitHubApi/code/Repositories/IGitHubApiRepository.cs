using System.Collections.Generic;
using Foundation.GitHubApi.Models;

namespace Foundation.GitHubApi.Repositories
{
    public interface IGitHubApiRepository
    {
        string ConvertMarkdownToHtml(string gitRoot, string rawMdUrl);
        GitRepository GetGitRepository(string gitRoot);
        GitItem GetRootPage(string gitRoot);
        List<GitItem> GetDocumentationPages(string gitRoot);
        List<GitContributor> GetContributors(string gitRoot);
        List<GitItem> GetDownloads(string gitRoot);
    }
}