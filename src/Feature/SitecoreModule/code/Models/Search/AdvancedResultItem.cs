using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Feature.SitecoreModule.Models.Search
{
    public class AdvancedResultItem : SearchResultItem
    {
        [IndexField("_templates")]
        public string Templates { get; set; }
    }
}