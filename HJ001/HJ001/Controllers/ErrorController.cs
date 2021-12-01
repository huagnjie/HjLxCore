using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.Controllers
{
    [Produces("application/json")]
    public class ErrorController:Controller
    {
        [Route("Error/{statusCode}")]
        [HttpGet()]
        public IActionResult HttpStatusCodeHandler(int statusCode) {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你访问的页面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    ViewBag.BasePath = statusCodeResult.OriginalPathBase;
                    break;
                default:
                    break;
            }
            return View("NotFound");
        }
    }
}
