using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using System.Linq;
using Feature.SitecoreModule.Models;
using Feature.SitecoreModule.Models.Search;
using Sitecore.Data;

namespace Feature.SitecoreModule.Repositories
{
    public class ModuleSearchRepository : IModuleSearchRepository
    {
        public List<__BaseSitecoreModule> SearchModules(ID bucketId, string keyword)
        {
            var ret = new List<__BaseSitecoreModule>();

            try
            {
                using (var context = ContentSearchManager.GetIndex($"sitecore_{Sitecore.Context.Database}_index").CreateSearchContext())
                {
                    var templateId = __BaseSitecoreModule.TemplateID.ToGuid().ToString().Replace("-", "");
                    var query = context.GetQueryable<AdvancedResultItem>()
                        .Where(item => item.Templates.Contains(templateId) && item.Paths.Contains(bucketId));
                    var results = query.GetResults();
                    foreach (var hit in results.Hits)
                    {
                        var item = hit.Document.GetItem();
                        if (item != null)
                            ret.Add(new __BaseSitecoreModule(item));
                    }
                }
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error($"Error searching modules using keyword '{keyword}'", e, this);
            }

            return ret;
        }
    }
}