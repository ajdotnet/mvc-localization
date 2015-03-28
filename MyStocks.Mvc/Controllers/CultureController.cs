using MyStocks.Mvc.Helper;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace MyStocks.Mvc.Controllers
{
    public class CultureController : Controller
    {
        //
        // GET: /SetPreferredCulture/de-DE
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public ActionResult SetPreferredCulture(string culture, string returnUrl)
        {
            this.Response.SetPreferredCulture(culture);
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }
    }
}
