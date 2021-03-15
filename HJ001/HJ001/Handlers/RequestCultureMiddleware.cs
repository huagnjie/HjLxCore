using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.Handlers
{
    /// <summary>
    /// 添加中间件 - 基于约定的中间件开发
    /// </summary>
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 基与构造函数的注入
        /// </summary>
        /// <param name="next"></param>
        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.WriteAsync("I'm is huangjie");
            await _next(context);
        }
    }

    /// <summary>
    /// 构建一个扩展方法 可以让他在configure里面直接调用
    /// 通过委托构造中间件，应用程序在运行时创建这个中间件，并将它添加到管道中。这里需要注意的是，中间件的创建是单例的，
    /// 每个中间件在应用程序生命周期内只有一个实例。
    /// </summary>
    public static class RequestCultureMiddlewareExtensions {
        public static IApplicationBuilder UseRequertCulture(this IApplicationBuilder builder) {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
