using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.Controllers
{
    [Route("[controller]")]
    public class DetailController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
