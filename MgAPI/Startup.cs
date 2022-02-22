using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MgAPI.Authorization;
using MgAPI.Data;
using MgAPI.Helpers;
using MgAPI.Models;
using MgAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MgAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("TestDb"));
            //options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"))); 
            services.AddCors();
            services.AddControllers()
            .AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());             
            });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<UsersContext>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, Context context)
        {
            createTestUsers(context);

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }

        private void createTestUsers(Context context)
        {
            // add hardcoded test users to db on startup
            var testUsers = new List<User>
            {
                new User { ID = "admin1", Firstname = "Admin", Lastname = "User", Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
                new User { ID = "user1", Firstname = "Normal", Lastname = "User", Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.Moderator }
            };
            context.Users.AddRange(testUsers);
            context.SaveChanges();
        }
    }
}
