using System.Web.Mvc;
using Feature.SitecoreModule.Models;
using Foundation.GitHubApi.Repositories;
using Sitecore.Mvc.Presentation;

namespace Feature.SitecoreModule.Controllers
{
    public class SitecoreModuleController : Controller
    {
        private readonly IGitHubApiRepository _gitHubApiRepository;

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
            var sitecoreModule = new __BaseSitecoreModule(PageContext.Current.Item);
            var docPages = _gitHubApiRepository.GetDocumentationPages(sitecoreModule.GitRepository);
            var readmePage = _gitHubApiRepository.GetRootPage(sitecoreModule.GitRepository);
            if (readmePage == null) 
                return View(docPages);
            readmePage.name = "About";
            docPages.Insert(0,readmePage);
            return View(docPages);
        }

        public ActionResult ModuleDownloadWindow()
        {
            var sitecoreModule = new __BaseSitecoreModule(PageContext.Current.Item);
            var downloads = _gitHubApiRepository.GetDownloads(sitecoreModule.GitRepository);
            return View(downloads);
        }

        public ActionResult ModuleContributors()
        {
            var sitecoreModule = new __BaseSitecoreModule(PageContext.Current.Item);
            var contributors = _gitHubApiRepository.GetContributors(sitecoreModule.GitRepository);
            return View(contributors);
        }
    }
}