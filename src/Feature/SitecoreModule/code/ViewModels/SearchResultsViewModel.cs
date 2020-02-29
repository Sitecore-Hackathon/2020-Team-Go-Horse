using System.Collections.Generic;
using Feature.SitecoreModule.Models;

namespace Feature.SitecoreModule.ViewModels
{
    public class SearchResultsViewModel
    {
        public string Keyword { get; set; }
        public List<__BaseSitecoreModule> Results { get; set; }
    }
}