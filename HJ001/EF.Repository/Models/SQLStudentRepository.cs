using StudentRepository.SqlServerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentRepository.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public SQLStudentRepository(AppDbContext context) {
            _context = context;
        }
        public Student Add(Student student)
        {
            _context.Student.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student = _context.Student.Find(id);
            if (student != null)
            {
                _context.Student.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Student.ToList();
        }

        public Student GetStudent(int id)
        {
            return _context.Student.Find(id);
        }

        public Student Update(Student updateSutdent)
        {
            var studentss = _context.Student.Attach(updateSutdent);
            studentss.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateSutdent;
        }
    }
}
