using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRepository.Models;
using StudentRepository.ViewModels;
using HJ001.Busines;
using System.Threading;

namespace HJ001.Controllers
{
    [Produces("application/json")]
    public class TaskController : Controller
    {
        private readonly StudyTask _studyTask = new StudyTask();
        private readonly IStudentRepository _studentRepository;
        public TaskController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Task_TaskOne")]
        public ObjectResult TaskOne()
        {
            _studyTask.Send();
            return new ObjectResult("");
        }

        [HttpGet("Task_EnumOne")]
        public ObjectResult EnumOne()
        {
            Console.WriteLine((flagEnum.A | flagEnum.Arthur));
            Console.WriteLine((flagEnum.None | flagEnum.A));
            Console.WriteLine((flagEnum.Arthur & flagEnum.None));
            Console.WriteLine(1);
            return new ObjectResult("");
        }
    }
}
