using System.Collections.Generic;

namespace Foundation.GitHubApi.Repositories
{
    /// <summary>
    /// Responsible for retrieving content from GitHub using GitHubAPI
    /// </summary>
    public class GitHubApiRepository : IGitHubApiRepository
    {
        /// <summary>
        /// Get the root page (normally README.md)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetRootPage(string gitRoot)
        {
            return new KeyValuePair<string, string>();
        }

        /// <summary>
        /// Get documentation pages (/documentation/*.md)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDocumentationPages(string gitRoot)
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Get module contributors
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetContributors(string gitRoot)
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Get download files (/sc.package/*.*)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDownloads(string gitRoot)
        {
            return new Dictionary<string, string>();
        }
    }
}