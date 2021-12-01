using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentRepository.Models;
using StudentRepository.ViewModels;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using Microsoft.Extensions.Logging;

namespace HJ001.Controllers
{
    /// <summary>
    /// 123
    /// </summary>
    [Produces("application/json")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hosting;
        private readonly ILogger logger;

        /// <summary>
        /// 通过构造函数注入了IStudentRepository
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="hosting"></param>
        /// <param name="logger"></param>
        public HomeController(IStudentRepository studentRepository, HostingEnvironment hosting,ILogger<HomeController> logger)
        {
            _studentRepository = studentRepository;
            this.hosting = hosting;
            this.logger = logger;
        }

        public IActionResult Index()
        {

            IEnumerable<Student> list = _studentRepository.GetAllStudents();
            return View(list);
            //return JsonConvert.SerializeObject(_studentRepository.GetStudent(1));
            //return Json(new { id = "1", name = "黄杰" });
        }


        public IActionResult Details(int? id)
        {
            logger.LogInformation("我警告你哦");

            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(id ?? 2),
                PageTitle = "偶吼吼"
            };
            return View(model);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                string uniqueFilesName = ProcessUploadedFileList(model);

                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName,
                    FilePath = uniqueFilesName.Trim(',')
                };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();
        }

        [HttpGet()]
        public ViewResult Edit(int Id)
        {
            Student student = _studentRepository.GetStudent(Id);
            if (student == null)
                throw new Exception("查询不到学生信息");
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath,
                ExistingFilePath = student.FilePath
            };
            return View(studentEditViewModel);
        }

        [HttpPost()]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);
                if (student == null)
                    throw new Exception("查询不到学生信息");
                student.Email = model.Email;
                student.Name = model.Name;
                student.ClassName = model.ClassName;
                //校验头像
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hosting.WebRootPath, "image", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    string uniqueFileName = ProcessUploadedFile(model);
                    student.PhotoPath = uniqueFileName;
                }
                //校验文件
                if (model.Files != null)
                {
                    if (model.ExistingFilePath != null)
                    {
                        string filePath = Path.Combine(hosting.WebRootPath, "image", model.ExistingFilePath);
                        System.IO.File.Delete(filePath);
                    }
                    string uniqueFilesName = ProcessUploadedFile(model);
                    student.FilePath = uniqueFilesName;
                }
                Student updateStudent = _studentRepository.Update(student);
                return RedirectToAction("Details", new { id = updateStudent.Id });
            }
            return View();
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="idList">id集合</param>
        /// <returns>数据集合</returns>
        [HttpGet("GetStudent/{idList}")]
        [Obsolete("测试过时")]
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

        [HttpGet("GetAllStudentCount")]
        public List<Student> GetAllStudentCount()
        {
            return _studentRepository.GetAllStudents().ToList();
        }

        /// <summary>
        /// 保存文件并返回文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hosting.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //因为使用了非托管资源,使用所以using进行回收
                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        /// <summary>
        /// 保存文件并返回文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadedFileList(StudentCreateViewModel model)
        {
            string uniqueFilesName = null;
            if (model.Files != null && model.Files.Count > 0)
            {
                foreach (var file in model.Files)
                {
                    string uploadsFolder = Path.Combine(hosting.WebRootPath, "File");
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        file.CopyTo(fileStream);
                    }
                    uniqueFilesName += fileName + ",";
                }
            }
            return uniqueFilesName;
        }
    }
}
