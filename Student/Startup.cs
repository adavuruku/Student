using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using Student.Data;
using Student.MyJWT;
using Student.Repositories;
using Student.Services;

namespace Student
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
            //services.AddDbContext<DataContext>(options =>
             //  options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            
             //db connection
            var connectionString = Configuration["PostgreSql:ConnectionString"];
            var dbPassword = Configuration["PostgreSql:DbPassword"];
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };
            services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.ConnectionString));
           // AddTrasient() -> service created for every instace -> object scope
           //Addscope -> create a single instance for a request -> request scope
           //AddSingleton -> application scope 
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
            
            services.AddScoped<ProductService, ProductImplementation>();
            services.AddScoped<TestingService, TestServiceImplementation>();
            //db ends
            
            // configure strongly typed settings object
            //we inject the part(section) of our appsetting.json we want here
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Student", Version = "v1"}); });
            
            
        }

        public IRouter BuildRouter(IApplicationBuilder applicationBuilder)
        {
            var builder = new RouteBuilder(applicationBuilder);
            builder.MapMiddlewareGet("/api/v1/user", appBuilder => {
                // your Middleware1
                //appBuilder.Use(MyMiddleware);
            });
            return builder.Build();
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student v1"));
            }

           /*
            Middle ware inasp.net core
            app.Use(async (context, next) =>
            
            {
                await context.Response.WriteAsync("Hello First Middleware");
                await next();
                await context.Response.WriteAsync("Hello First Again Middleware");
            });
            
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello Second Middleware");
            });
            
            */

            app.UseHttpsRedirection();
 
             //YOU NEED UseRouting and UseEndpoint to enable use routing 
             app.UseRouting();
 
             app.UseMiddleware<JwtMiddleware>();
             
             //app.UseMiddleware<MyMiddleware>();
             
             app.UseAuthorization();
             
             app.UseEndpoints(endpoints =>endpoints.MapControllers());
 
            /* app.UseEndpoints(endpoints =>
             {
                 //endpoints.MapControllers();
                 endpoints.Map("/test",
                     async context => { await context.Response.WriteAsync("Middleware In Routing"); });

                 //create defaault routee to handle 404
                 endpoints.MapControllerRoute(
                     "default",
                     "{controller=Test}/{action=NotFoundAction}");
             });*/
         
        }
    }
}