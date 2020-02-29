using System.Collections.Generic;
using System.Web.Mvc;
using Feature.SitecoreModule.Models;
using Feature.SitecoreModule.ViewModel;
using Sitecore.Mvc.Presentation;

namespace Feature.SitecoreModule.Controllers
{
    public class SitecoreModuleController : Controller
    {
        public ActionResult ModuleTitle()
        {
            var model = new __BaseSitecoreModule(PageContext.Current.Item);
            return View(model);
        }

        public ActionResult ModuleDocumentation()
        {
            // TODO: Replace with logic
            var model = new List<ModuleDocumentationEntry>
            {
                new ModuleDocumentationEntry {Title = "About", Content = "Some about content"},
                new ModuleDocumentationEntry {Title = "Documentation", Content = "Some about documentation"},
            };

            return View(model);
        }        
    }
}