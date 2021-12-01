using HJ001.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using StudentRepository.SqlServerRepository;
using Microsoft.EntityFrameworkCore;

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
            //EFCore
            //使用Pool版本是有连接池的，所以当连接池存在时，不会重新创建新实例
            services.AddDbContextPool<AppDbContext>(options =>
//设置数据库类型为Sqlserver,并且引入连接字符串
options.UseSqlServer(_configuration.GetConnectionString("StudentConn")));

            //只包含了核心的MVC功能
            //services.AddMvcCore();

            //services.AddScopedOne();
            //services.AddScopedTwo();

            //注册Swagger生成器,定义一个Swagger文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bingle API",
                    Description = "一个简单的ASP.NET Core Web API",
                    TermsOfService = new Uri("https://www.cnblogs.com/namelessblog/"),
                    Contact = new OpenApiContact
                    {
                        Name = "bingle",
                        Email = string.Empty,
                        Url = new Uri("https://www.cnblogs.com/namelessblog/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "许可证",
                        Url = new Uri("https://www.cnblogs.com/namelessblog/"),
                    }
                });                
                //为 Swagger JSON and UI设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) { c.IncludeXmlComments(xmlPath); }
                //忽略过时属性
                //c.IgnoreObsoleteActions();
                //c.TagActionsBy(api => api.HttpMethod);
            });

            //包含了依赖于MVC Core以及相关的第三方常用的所有方法
            services.AddMvc().AddXmlSerializerFormatters();

            //services.AddSingleton<IStudentRepository, MockStudentRepository>();
            services.AddStudentRepository();
            //services.AddTransient();
        }

        #region Map和MapWhen

        //private static void HandleMap(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Handle Map");
        //    });
        //}

        //private static void HandleBranch(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        var branchVer = context.Request.Query["branch"];
        //        await context.Response.WriteAsync($"Branch used = {branchVer}");
        //    });
        //}

        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            #region Map和MapWhen

            ////Map表示地图的意思，表示当前访问路径中存在了"/map"的时候就调用HandleMap方法，否则就跳过 - 路径
            //app.Map("/map", HandleMap);

            ////当判断当前请求参数中存在branch参数时，则调用HandleBranch方法 - 参数
            //app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);

            ////Map支持层级分区，详情如下
            //app.Map("/leve11", leve11App => {
            //    leve11App.Map("/leve12a", leve12AApp =>
            //    {

            //    });
            //    leve11App.Map("/leve12b", leve12BApp =>
            //    {

            //    });
            //});

            #endregion

            #region 中间件
            //app.UseScoped();
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

            //app.Use((context, next) => {
            //    context.Response.WriteAsync("I'm is huangjie");

            //    return next();
            //});

            //app.UseRequertCulture(); ;

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


            //传统路由
            //app.UseMvc(rutes =>
            //{
            //    rutes.MapRoute("default", "{controller-Home}/{action=Index}/{id?}");
            //});

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();

            //启用中间件以提供用户界面（HTML、JS、CSS等），特别是指定JSON端点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //页面头名称
                c.DocumentTitle = "平台API";
                //页面API文档格式 Full = 全部展开, List = 只展开列表, None = 都不展开
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                //c.RoutePrefix = string.Empty;
            });


            //MVC默认带有信息的路由
            app.UseMvcWithDefaultRoute();

            //属性路由
            //app.UseMvc();

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
