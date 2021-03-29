using HJ001.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HJ001.ViewModels;

namespace HJ001.Controllers
{
    /// <summary>
    /// 123
    /// </summary>
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 通过构造函数注入了IStudentRepository
        /// </summary>
        /// <param name="studentRepository"></param>
        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{Home}/{Index}")]
        public IActionResult Index()
        {
            IEnumerable<Student> list = _studentRepository.GetAllStudents();
            return View(list);
            //return JsonConvert.SerializeObject(_studentRepository.GetStudent(1));
            //return Json(new { id = "1", name = "黄杰" });
        }

        [HttpGet("{Home}/{Details}")]
        public IActionResult Details()
        {
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(2),
                PageTitle = "偶吼吼"
            };
            return View(model);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="idList">id集合</param>
        /// <returns>数据集合</returns>
        [HttpGet("GetStudent/{idList}")]
        public ObjectResult GetStudent(List<string> idList)
        {
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(2),
                PageTitle = "偶吼吼"
            };
            return new ObjectResult(model);
        }

        [HttpPost("SetStudent/{idList}")]
        public ObjectResult SetStudent(List<string> idList)
        {
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(2),
                PageTitle = "偶吼吼"
            };
            return new ObjectResult(model);
        }

        [HttpDelete("DeleteStudent/{idList}")]
        public ObjectResult DeleteStudent(List<string> idList)
        {
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(2),
                PageTitle = "偶吼吼"
            };
            return new ObjectResult(model);
        }
    }
}
