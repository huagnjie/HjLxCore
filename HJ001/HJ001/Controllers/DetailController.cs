using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRepository.Models;
using StudentRepository.ViewModels;

namespace HJ001.Controllers
{
    [Produces("application/json")]
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
