using Afrodit.Business.Abstract;
using Afrodit.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Afrodit.WebApi.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public AuthenticationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader == null)
            {
                await _next(context);
                return;
            }

            if (authHeader != null && authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                var userClaim = context.User.Identities.FirstOrDefault();

                if (userClaim == null)
                    context.Response.StatusCode = 401;
                else
                {
                    var userid = userClaim.Name;
                    var user = await _userService.GetUserById(int.Parse(userid));
                    if (user == null || user.Status == Status.Passive)
                        context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }

            if (context.Response.StatusCode == 401)
                context.User = null;

            await _next(context);
        }
    }
}