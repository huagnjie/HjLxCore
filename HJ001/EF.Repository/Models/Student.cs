using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRepository.Models
{
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student
    {
        public int Id { get; set; }

        [Display(Name="姓名")]
        [Required(ErrorMessage ="请输入姓名"),MaxLength(50,ErrorMessage ="姓名长度不能超过50个字符")]
        [MinLength(2,ErrorMessage ="姓名长度不能低于2个字符")]
        public string Name { get; set; }

        [Display(Name = "班级名称")]
        [Required(ErrorMessage = "请输入班级名称")]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage ="邮箱格式不正确")]
        public string Email { get; set; }

        [Display(Name = "头像文件路径")]
        public string PhotoPath { get; set; }

        [Display(Name = "个人附件")]
        public string FilePath { get; set; }
    }
}
