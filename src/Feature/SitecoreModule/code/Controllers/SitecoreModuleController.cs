using System.Collections.Generic;
using System.Web.Mvc;
using Feature.SitecoreModule.Models;
using Feature.SitecoreModule.ViewModel;
using Foundation.GitHubApi.Repositories;
using Sitecore.Mvc.Presentation;

namespace Feature.SitecoreModule.Controllers
{
    public class SitecoreModuleController : Controller
    {
        private IGitHubApiRepository _gitHubApiRepository;

        public SitecoreModuleController(IGitHubApiRepository gitHubApiRepository)
        {
            _gitHubApiRepository = gitHubApiRepository;
        }

        public ActionResult ModuleTitle()
        {
            var model = new __BaseSitecoreModule(PageContext.Current.Item);
            return View(model);
        }

        public ActionResult ModuleDocumentation()
        {
            // TODO: Replace with logic
            var model = new Dictionary<string, string>
            {
                {"About", "Some about content"}, 
                {"Documentation", "Some about documentation"}
            };
            return View(model);
        }

        public ActionResult ModuleDownloadWindow()
        {
            var model = new Dictionary<string, string>
            {
                {"About", "Some about content"}, 
                {"Documentation", "Some about documentation"}
            };
            return View(model);
        }

    }
}