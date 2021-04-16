using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.Extensions.FileProviders;
/// <summary>
/// 
///  Sam Painter
///  
/// 
/// This was my first ASP.NET Core & Angular Web API project that I followed from a tutorial on Youtube. 
/// 
/// The API allows you to enter Employees into a directory that you can then sort by Department
/// You can also sort and filter 
/// 
/// 
/// 
/// </summary>

namespace WebAPI
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


            //Add CORS (cross origin resource sharing) relaxes same-origin policy/ web security 

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                 .AllowAnyHeader());
            });

            //JSON Serializer (this converts .NET objects into their JSON equivalent and back again by  mapping the .net object property to json prop.)

            services.AddControllersWithViews()
                 .AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                 .Json.ReferenceLoopHandling.Ignore)
                 .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                 = new DefaultContractResolver());               
                

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //added this for CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseStaticFiles(new StaticFileOptions
            {

                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
                    RequestPath="/Photos"
            });


        }
    }
}