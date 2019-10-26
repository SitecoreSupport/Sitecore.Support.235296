using System.Linq;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Mvc.ViewModels;

namespace Sitecore.Support.Forms.Mvc.Validators
{
  public class ReCaptchaValidator : ActionFilterAttribute
  {
    public string ParamName { get; set; }

    public static readonly ID ReCaptchaFieldId = new ID("{7FB270BE-FEFC-49C3-8CB4-947878C099E5}");

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (filterContext.ActionParameters.ContainsKey(ParamName))
      {
        var value = filterContext.ActionParameters[ParamName] as FormViewModel;
        if (value != null)
        {
          var formItem = FormItem.GetForm(value.Item.ID);
          if (formItem != null)
          {
            if (!ValidateSubmittedFormRecaptcha(formItem, value))
            {
              filterContext.Result = new EmptyResult();
              Log.Warn("Sitecore.Support.235296: there was an attempt to bypass the reCaptcha field validation while submitting the " + formItem.FormName + " form.", this);
            }
          }
        }
      }
    }

    protected bool ValidateSubmittedFormRecaptcha(FormItem formItem, FormViewModel submittedForm)
    {
      var hasRecaptcha = formItem.Fields.Any(f => f.TypeID == ReCaptchaFieldId);
      if (hasRecaptcha)
      {
        var hasRecaptchaInSubmittedForm = false;

        foreach (var section in submittedForm.Sections)
        {
          hasRecaptchaInSubmittedForm = section.Fields.Any(f =>
            f.Item[Form.Core.Configuration.FieldIDs.FieldLinkTypeID] ==
            ReCaptchaFieldId.ToString());

          //stop loop when Recaptcha found
          if (hasRecaptchaInSubmittedForm)
          { break; }
        }

        return hasRecaptchaInSubmittedForm;
      }

      return true;
    }
  }
}