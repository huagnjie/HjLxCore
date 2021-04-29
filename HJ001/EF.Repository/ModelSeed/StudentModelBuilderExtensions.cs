using Microsoft.EntityFrameworkCore;
using StudentRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRepository.ModelSeed
{
    public static class StudentModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 1,
                Name = "黄杰",
                ClassName = ClassNameEnum.FirstGrade,
                Email = "1341951524@163.com"

            }, new Student
            {
                Id = 2,
                Name = "黄小杰",
                ClassName = ClassNameEnum.SecondGrade,
                Email = "1341951524@qq.com"

            });
        }
    }
}
