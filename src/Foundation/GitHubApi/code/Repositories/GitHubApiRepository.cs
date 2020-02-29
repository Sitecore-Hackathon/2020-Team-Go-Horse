using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Foundation.GitHubApi.Models;
using Newtonsoft.Json;
using Sitecore.Collections;

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
            items = items.Where(p => p.type == "file").ToList();
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
            var httpResult = GetHttpRequest(path);
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
            items = items.Where(p => p.type == "file").ToList();
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

        public string ConvertMarkdownToHtml(string gitRoot, string rawMdUrl, string folder = "")
        {
            var response = GetHttpRequest(rawMdUrl);
            response = ReplaceImagesFromJson(gitRoot, response, folder);
            response = ReplaceLinksFromJson(gitRoot, response, folder);
            var markDownResult = CommonMark.CommonMarkConverter.Convert(response);
            return markDownResult;
        }

        private string ReplaceImagesFromJson(string gitRoot, string rawMdContent, string folder = "")
        {
            const string imgOpen = "![";
            const string imgClose = "\")";
            const string imgPreUrl = "](";

            if (!string.IsNullOrEmpty(folder))
                folder = $"{folder}/";

            // Nothing to parse - exit
            if (!rawMdContent.Contains(imgOpen))
                return rawMdContent;

            // Build strings to be replaced
            var index = 0;
            var imagesToReplace = new List<KeyValuePair<string,string>>();
            while ((index=rawMdContent.IndexOf(imgOpen, index, StringComparison.Ordinal)) != -1)
            {
                var indexOfNextClosing = rawMdContent.Substring(index).IndexOf(imgClose, StringComparison.Ordinal);
                indexOfNextClosing = indexOfNextClosing + imgClose.Length;

                var imgOriginal = rawMdContent.Substring(index, indexOfNextClosing);
                var imgReplaced = imgOriginal.Contains("http://") || imgOriginal.Contains("https://")
                    ? imgOriginal
                    : imgOriginal.Replace(imgPreUrl, $"{imgPreUrl}{gitRoot}/blob/master/{folder}");

                imagesToReplace.Add(new KeyValuePair<string, string>(imgOriginal, imgReplaced));
                index++;
            }
            if (!imagesToReplace.Any()) 
                return rawMdContent;

            // Replace all
            foreach (var pair in imagesToReplace)
                if (pair.Key != pair.Value)
                    rawMdContent = rawMdContent.Replace(pair.Key, pair.Value);

            return rawMdContent;            
        }

        private string ReplaceLinksFromJson(string gitRoot, string rawMdContent, string folder = "")
        {
            const string linkOpen = " [";
            const string linkClose = ")";
            const string linkPreUrl = "](";

            if (!string.IsNullOrEmpty(folder))
                folder = $"{folder}/";

            // Nothing to parse - exit
            if (!rawMdContent.Contains(linkOpen))
                return rawMdContent;

            // Build strings to be replaced
            var index = 0;
            var linksToReplace = new List<KeyValuePair<string,string>>();
            while ((index=rawMdContent.IndexOf(linkOpen, index, StringComparison.Ordinal)) != -1)
            {
                var indexOfNextClosing = rawMdContent.Substring(index).IndexOf(linkClose, StringComparison.Ordinal);
                indexOfNextClosing = indexOfNextClosing + linkClose.Length;

                var linkOriginal = rawMdContent.Substring(index, indexOfNextClosing);
                var linkReplaced = linkOriginal.Contains("http://") || linkOriginal.Contains("https://")
                    ? linkOriginal
                    : linkOriginal.Replace(linkPreUrl, $"{linkPreUrl}{gitRoot}/blob/master/{folder}");

                linksToReplace.Add(new KeyValuePair<string, string>(linkOriginal, linkReplaced));
                index++;
            }
            if (!linksToReplace.Any()) 
                return rawMdContent;

            // Replace all
            foreach (var pair in linksToReplace)
                if (pair.Key != pair.Value)
                    rawMdContent = rawMdContent.Replace(pair.Key, pair.Value);

            return rawMdContent;            
        }

    }
}