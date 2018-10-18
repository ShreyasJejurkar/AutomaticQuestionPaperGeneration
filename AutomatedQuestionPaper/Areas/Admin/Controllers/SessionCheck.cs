using System.Web.Mvc;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (session["Username"] == null)
            {
                var uriHelper = new UrlHelper(filterContext.RequestContext);
                var redirectUrl = uriHelper.Action("Index", "Account", new { area = "" });
                filterContext.Controller.TempData["SessionErrorMessage"] = "Please log in to your account first";
                filterContext.Result = new RedirectResult(redirectUrl);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}