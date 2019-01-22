using Sitecore.Forms.Mvc.Controllers.ModelBinders;
using Sitecore.Forms.Mvc.ViewModels;
using System.Web.Mvc;
using Sitecore.Support.Forms.Mvc.Validators;

namespace Sitecore.Support.Forms.Mvc.Controllers
{
  public class FormController : Sitecore.Forms.Mvc.Controllers.FormController
  {
    [ReCaptchaValidator(ParamName = "formViewModel")]
    public override ActionResult Index([ModelBinder(typeof(FormModelBinder))] FormViewModel formViewModel)
    {
      return base.Index(formViewModel);
    }
  }
}