using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRepository.ViewModels
{
    public class StudentEditViewModel:StudentCreateViewModel
    {
        public int Id { get; set; }

        public string ExistingPhotoPath { get; set; }

        public string ExistingFilePath { get; set; }
    }
}
