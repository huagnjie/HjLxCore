using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList = new List<Student>();
        public MockStudentRepository()
        {
            _studentList = new List<Student>() {
             new Student(){ Id = 1, Name = "黄杰", ClassName ="666", Email="1341951524@qq.com" },
             new Student(){ Id = 2, Name = "李伟", ClassName ="666", Email="1341951534@qq.com" },
             new Student(){ Id = 3, Name = "张伟", ClassName ="666", Email="1341951525@qq.com" },
             new Student(){ Id = 4, Name = "何炅", ClassName ="666", Email="1341951521@qq.com" },
            };
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
    }
}
