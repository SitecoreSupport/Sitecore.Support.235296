using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.Support.Forms.Mvc.Pipelines.Initialize
{
    public class InitializeRoutes : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
    {
        protected override void RegisterRoutes(RouteCollection routes, PipelineArgs args)
        {
            if (routes.Remove(RouteTable.Routes["Form"]))
            {
                routes.MapRoute("Form", "form/{action}", new
                {
                    controller = "Form",
                    action = "Process"
                }, new string[1]
                {
          "Sitecore.Support.Forms.Mvc.Controllers"
                });
            }
        }
    }
}