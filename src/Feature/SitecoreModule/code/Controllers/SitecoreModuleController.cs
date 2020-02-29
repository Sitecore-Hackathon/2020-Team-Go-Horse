using System.Collections.Generic;
using System.Web.Mvc;
using Feature.SitecoreModule.Models;
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
                {"https://github.com/Sitecore-Hackathon/2020-Team-Go-Horse/blob/master/sc.package/test.txt?raw=true", "test.txt"}, 
                {"https://github.com/Sitecore-Hackathon/2020-Team-Go-Horse/blob/master/sc.package/README.md?raw=true", "README.md"}
            };
            return View(model);
        }

    }
}