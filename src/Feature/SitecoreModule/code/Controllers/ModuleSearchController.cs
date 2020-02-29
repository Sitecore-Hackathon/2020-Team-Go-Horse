using System.Web.Mvc;
using Feature.SitecoreModule.Repositories;
using Sitecore.Data;

namespace Feature.SitecoreModule.Controllers
{
    public class ModuleSearchController: Controller
    {
        private const string SearchParam = "q";
        private readonly ID _bucketRootId = new ID("{1F774DA7-DF90-40D6-ABF0-81DE1180CF63}");
        private readonly IModuleSearchRepository _moduleSearchRepository;

        public ModuleSearchController(IModuleSearchRepository moduleSearchRepository)
        {
            _moduleSearchRepository = moduleSearchRepository;
        }

        public ActionResult SearchResults()
        {
            var keyword = Request.QueryString[SearchParam];
            var result = _moduleSearchRepository.SearchModules(_bucketRootId, keyword);
            return View(result);
        }
    }
}