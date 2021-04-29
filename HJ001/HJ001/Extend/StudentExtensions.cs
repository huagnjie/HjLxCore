using Microsoft.Extensions.DependencyInjection;
using StudentRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HJ001
{
    public static class StudentExtensions
    {
        /// <summary>
        /// 添加服务的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddStudentRepository(this IServiceCollection services)
        {
            return services.AddScoped<IStudentRepository,SQLStudentRepository>();
        }
    }
}
