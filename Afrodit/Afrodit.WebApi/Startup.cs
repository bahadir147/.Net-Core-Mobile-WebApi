using Afrodit.Business.Abstract;
using Afrodit.Business.Concrete;
using Afrodit.Repositories.Abstract;
using Afrodit.Repositories.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Afrodit.Core.Helper;
using System.Text;
using Afrodit.WebApi.CustomMiddlewares;
using Microsoft.AspNetCore.Http;

namespace Afrodit.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region JWT

            var key = Encoding.ASCII.GetBytes(AppSettingsJson.GetString("AppSettings:Secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            #endregion

            #region INJECTION

            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IUserRepository, EFUserRepository>();

            services.AddScoped<IPostRepository, EFPostRepository>();
            services.AddScoped<IPostService, PostManager>();

            services.AddScoped<IFollowerRepository, EFFollowerRepository>();
            services.AddScoped<IFollowerService, FollowerManager>();

            services.AddScoped<IPhotoRepository, EFPhotoRepository>();
            services.AddScoped<IPhotoService, PhotoManager>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();
        }
    }
}
