using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq.Utilities;
using System.Linq;
using System.Web;
using Feature.SitecoreModule.Models;
using Sitecore.Data;
using Sitecore.Data.Items;

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
                    var query = context.GetQueryable<SearchResultItem>()
                        //.Where(item => item.Fields["_templates"].ToString().Contains(__BaseSitecoreModule.TemplateID.T)
                        .Where(item => item.Paths.Contains(bucketId));

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