using MyStocks.Mvc.Helper;
using System.Web.Mvc;

namespace MyStocks.Mvc.Controllers
{
    public class CultureController : Controller
    {
        //
        // GET: /SetPreferredCulture/de-DE
        public ActionResult SetPreferredCulture(string culture, string returnUrl)
        {
            this.Response.SetPreferredCulture(culture);
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }
    }
}
