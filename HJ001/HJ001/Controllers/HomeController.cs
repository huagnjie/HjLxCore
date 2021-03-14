using HJ001.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HJ001.Controllers
{
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

        public string Index()
        {
            return JsonConvert.SerializeObject(_studentRepository.GetStudent(1));
            //return Json(new { id = "1", name = "黄杰" });
        }

        public ObjectResult Details() {
            Student model = _studentRepository.GetStudent(2);
            return new ObjectResult(model);
        }
    }
}
