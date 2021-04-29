using HJ001.HandleService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.Handlers
{
    /// <summary>
    /// 创建中间件代理类
    /// </summary>
    public class ScopedMiddleware
    {
        private readonly RequestDelegate _next;

        public ScopedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext context, IScopedService scoped)
        {
            context.Response.ContentType = "text/plain;charset=utf-8";
            scoped.MyName = "huangjie";
            await _next(context);
        }
    }
}

/// <summary>
/// 添加依赖服务注册
/// </summary>
namespace Microsoft.Extensions.DependencyInjection
{
    using HJ001.Handlers;
    public static partial class ScopedMiddlewareExtensions
    {
        /// <summary>
        /// 添加服务的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddScopedOne(this IServiceCollection services)
        {
            return services.AddScoped<IScopedService, ScopedService>();
        }

        public static IServiceCollection AddScopedTwo(this IServiceCollection services)
        {
            return services.AddScoped<IScopedService, ScopedTwoService>();
        }
    }
}

/// <summary>
/// 创建中间件控制类
/// </summary>
namespace Microsoft.AspNetCore.Builder
{
    using HJ001.Handlers;
    /// <summary>
    /// 创建中间件扩展类
    /// </summary>
    public static partial class ScopedMiddlewareExtensions
    {
        /// <summary>
        /// 使用中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseScoped(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ScopedMiddleware>();
        }
    }
}