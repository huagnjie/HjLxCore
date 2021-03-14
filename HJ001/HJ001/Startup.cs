using HJ001.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001
{
    public class Startup
    {
        /// <summary>
        /// 服务与.Net Core拿到各种配置文件
        /// </summary>
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940        
        public void ConfigureServices(IServiceCollection services)
        {
            //包含了依赖于MVC Core以及相关的第三方常用的所有方法
            services.AddMvc().AddXmlSerializerFormatters();
            //只包含了核心的MVC功能
            //services.AddMvcCore();

            services.AddSingleton<IStudentRepository,MockStudentRepository>();

            //services.AddTransient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 中间件
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Run");
            //});

            ////过渡中间件
            //app.Use(async (context, next) =>
            //{
            //    //中文化
            //    context.Response.ContentType = "text/plain;charset=utf-8";

            //    logger.LogInformation("M1:传入请求");
            //    await next();
            //    logger.LogInformation("M1:传出响应");
            //});
            #endregion

            #region 两种静态文件的中间件用法

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("htmlpage.html");
            //app.UseFileServer(fileServerOptions);

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("htmlpage.html");


            ////添加默认文件
            //app.UseDefaultFiles(defaultFilesOptions);

            ////添加静态文件，添加后才可以访问wwwroot里面的文件
            //app.UseStaticFiles();

            #endregion

            app.UseStaticFiles();

            //MVC默认带有信心的路由
            app.UseMvcWithDefaultRoute();

            //终端中间件一般只能有一个
            app.Run(async (context) =>
            {
                //logger.LogInformation("M3");
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //var configValue = _configuration["MyKey"];
                await context.Response.WriteAsync("Hosting Enviroment:" + env.EnvironmentName);
            });
        }
    }
}
