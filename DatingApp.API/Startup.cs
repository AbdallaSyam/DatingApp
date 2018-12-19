using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using DatingApp.API.Helper;
using AutoMapper;

namespace DatingApp.API
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
            #region DBContext
                 services.AddDbContext<DataContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
           
             #region AddScopedServices (InjectionDependency)
               services.AddScoped<DataContext,DataContext>();
               services.AddScoped<IAuthRepository,AuthRepository>();
               services.AddScoped<IDateRepository,DatingRepository>();
            #endregion

             #region AddSeedingService
                 services.AddTransient<Seed>();
             #endregion

             #region TokenAuthentication
                  services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options=>{
                Options.TokenValidationParameters= new TokenValidationParameters{

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSetting:Token").Value)),
                    ValidateIssuer=false,
                    ValidateAudience = false
                };
            });
             #endregion

         
           //Avoid the Cors Issue
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(Opt=>{
              Opt.SerializerSettings.ReferenceLoopHandling =Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddAutoMapper();
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               #region HandleErrorInProductionEnvironment
                       app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                   
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error!=null){
                                  context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
               #endregion
            
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            // seeder.SeedUsers();

                       //To Avoid Origin Error
            app.UseCors(X =>X.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); app.UseCors(X =>X.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                       //To Authenticate the services            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
