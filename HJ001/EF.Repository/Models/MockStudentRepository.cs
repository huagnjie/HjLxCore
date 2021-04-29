using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRepository.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList = new List<Student>();
        public MockStudentRepository()
        {
            _studentList = new List<Student>() {
             new Student(){ Id = 1, Name = "黄杰", ClassName = ClassNameEnum.FirstGrade, Email="1341951524@qq.com" },
             new Student(){ Id = 2, Name = "李伟", ClassName = ClassNameEnum.SecondGrade, Email="1341951534@qq.com" },
             new Student(){ Id = 3, Name = "张伟", ClassName = ClassNameEnum.GradeThree, Email="1341951525@qq.com" },
            };
        }

        public Student Add(Student student)
        {
            student.Id = _studentList.Max(s => s.Id) + 1;
            _studentList.Add(student);
            return student;
        }

        public Student Delete(int id)
        {
            Student student = _studentList.FirstOrDefault(v => v.Id == id);
            if (student != null)
            {
                _studentList.Remove(student);
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(x => x.Id == id);
        }

        public Student Update(Student updateSutdent)
        {
            Student student = _studentList.FirstOrDefault(v => v.Id == updateSutdent.Id);
            if (student != null)
            {
                student.Name = updateSutdent.Name;
                student.Email = updateSutdent.Email;
                student.ClassName = updateSutdent.ClassName;
            }
            return student;
        }
    }
}
