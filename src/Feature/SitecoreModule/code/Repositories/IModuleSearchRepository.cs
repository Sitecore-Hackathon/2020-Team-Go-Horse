using System.Collections.Generic;
using Feature.SitecoreModule.Models;
using Sitecore.Data;

namespace Feature.SitecoreModule.Repositories
{
    public interface IModuleSearchRepository
    {
        List<__BaseSitecoreModule> SearchModules(ID bucketId,  string keyword);
    }
}
