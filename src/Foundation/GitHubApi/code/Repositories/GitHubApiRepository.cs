using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Foundation.GitHubApi.Models;
using Newtonsoft.Json;

namespace Foundation.GitHubApi.Repositories
{
    /// <summary>
    /// Responsible for retrieving content from GitHub using GitHubAPI
    /// </summary>
    public class GitHubApiRepository : IGitHubApiRepository
    {
        private static readonly string BaseGitApi = "https://api.github.com/repos/";
        private static readonly string BaseGitUrl = "https://github.com/";

        /// <summary>
        /// Get GitRepository model from a Git URL
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public GitRepository GetGitRepository(string gitRoot)
        {
            var gitRepository = GetHttpRequest(gitRoot);
            var jsonObject = JsonConvert.DeserializeObject<GitRepository>(gitRepository);
            return jsonObject;
        }

        /// <summary>
        /// Get the root page (normally /README.md)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public GitItem GetRootPage(string gitRoot)
        {
            const string fileName = "readme.md";
            var path = $"{gitRoot}/contents";
            var httpResult = GetHttpRequest(path);
            var jsonObject = JsonConvert.DeserializeObject<List<GitItem>>(httpResult);
            var items = jsonObject ?? new List<GitItem>();
            var item = items.FirstOrDefault(x => x.name.ToLower().Equals(fileName));
            if (item != null)
                item.HtmlValue = ConvertMarkdownToHtml(gitRoot, item.download_url);
            return item;
        }

        /// <summary>
        /// Get documentation pages (/documentation/*.md)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public List<GitItem> GetDocumentationPages(string gitRoot)
        {
            var path = $"{gitRoot}/contents/documentation";
            var httpResult = GetHttpRequest(path);
            var jsonObject = JsonConvert.DeserializeObject<List<GitItem>>(httpResult);
            var items = jsonObject ?? new List<GitItem>();
            items = items.Where(p=>p.type=="file").ToList();
            foreach (var item in items)
                item.HtmlValue = ConvertMarkdownToHtml(gitRoot, item.download_url);
            return items;
        }

        /// <summary>
        /// Get module contributors
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public List<GitContributor> GetContributors(string gitRoot)
        {
            var path = $"{gitRoot}/contributors";
            var httpResult = GetHttpRequest( path);
            var jsonObject = JsonConvert.DeserializeObject<List<GitContributor>>(httpResult);
            return jsonObject ?? new List<GitContributor>();
        }

        /// <summary>
        /// Get download files (/sc.package/*.*)
        /// </summary>
        /// <param name="gitRoot"></param>
        /// <returns></returns>
        public List<GitItem> GetDownloads(string gitRoot)
        {
            var path = $"{gitRoot}/contents/sc.package?ref=master";
            var httpResult = GetHttpRequest(path);
            var jsonObject = JsonConvert.DeserializeObject<List<GitItem>>(httpResult);
            var items = jsonObject ?? new List<GitItem>();
            foreach (var item in items)
                item.HtmlValue = ConvertMarkdownToHtml(gitRoot, item.download_url);
            return items;
        }

        /// <summary>
        /// Execute HttpRequest
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetHttpRequest(string url)
        {
            try
            {
                url = CreateGitApiUrlFromBaseGitUrl(url);
                var request = WebRequest.CreateHttp(url);
                request.UserAgent = "request";               
                request.Method = "GET";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream == null)
                            return string.Empty;
                        using (var myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            return myStreamReader.ReadToEnd();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error($"Error executing HttpRequest to url '{url}'", e, this);
                return string.Empty;
            }
        }

        private string CreateGitApiUrlFromBaseGitUrl(string url)
        {
            return url.Replace(BaseGitUrl, BaseGitApi);
        }

        private string ConvertMarkdownToHtml(string gitRoot, string rawMdUrl)
        {
            var response = GetHttpRequest(rawMdUrl);
            response = ReplaceImagesFromJsonByRawImage(gitRoot, response);
            var markDownResult = CommonMark.CommonMarkConverter.Convert(response);
            return markDownResult;
        }

        private string ReplaceImagesFromJsonByRawImage(string gitRoot, string rawMdContent)
        {
            const string blobPartialUrl = "blob/master";
            //Pattern for images inside the MD file: ![image name](local URL "Image Name")
            if (!rawMdContent.Contains("!["))
                return rawMdContent;

            var lastIndex = rawMdContent.IndexOf("](", 0, StringComparison.Ordinal);
            var currentIndex = 0;

            while (true)
            {
                lastIndex = (currentIndex > lastIndex) ? currentIndex : lastIndex;
                var nextIndex = rawMdContent.IndexOf("![", lastIndex + 2, StringComparison.Ordinal);

                if (rawMdContent.Substring(lastIndex + 2).StartsWith("http")) //check if the next image already have an http in its url.
                {
                    if(nextIndex == -1)
                        break;

                    currentIndex = rawMdContent.IndexOf("](", nextIndex, StringComparison.Ordinal);
                    continue;
                }

                rawMdContent = rawMdContent.Insert(lastIndex + 2, $"{gitRoot}/{blobPartialUrl}/"); //inserting the public URL to render the right image.

                if (nextIndex == -1 || nextIndex == lastIndex)
                    break;

                lastIndex = rawMdContent.IndexOf("](", nextIndex, StringComparison.Ordinal);
            }

            return rawMdContent;
        }
    }
}