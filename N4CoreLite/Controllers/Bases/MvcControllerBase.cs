#nullable disable

using Microsoft.AspNetCore.Mvc;
using N4CoreLite.Models;
using System.Globalization;

namespace N4CoreLite.Controllers.Bases
{
    public abstract class MvcControllerBase : Controller
    {
        protected MvcControllerBase()
        {
            CultureInfo cultureInfo = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        protected virtual async Task SetViewData(string message = null, PageOrderModel pageOrder = null)
        {
            ViewBag.Message = message; // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
            ViewBag.PageOrder = pageOrder;
        }

        protected virtual void SetTempData(string message)
        {
            TempData["Message"] = message; // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
        }
    }
}
