using StudentRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRepository.ViewModels
{
    /// <summary>
    /// ViewModel - 视图模型。在其他的各种框架中也可以被称为DTO
    /// </summary>
    public class HomeDetailsViewModel
    {
        public Student Student { get; set; }
        public string PageTitle { get; set; }
    }
}
