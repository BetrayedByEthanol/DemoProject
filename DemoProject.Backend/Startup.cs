using DemoProject.Backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using DemoProject.Backend.ModelDTOs;
using DemoProject.Backend.Security;
using DemoProject.Backend.Middleware;
using System.Security.Claims;
using DemoProject.Backend.Hubs;

namespace DemoProject.Backend
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

            services.AddControllers();
            services.AddSignalR();
            Mapping.cofigureMappingTable();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = new TimeSpan(0, 5, 0);

            });

            services.AddTransient<IUserStore<UserIdentity>, ApplicationUserStore>();
            services.AddTransient<IPasswordHasher<UserIdentity>, Sec>();
            services.AddTransient<ILookupNormalizer, LookUpNormalizer>();
            services.AddTransient<IdentityErrorDescriber>();
            services.AddTransient<UserManager<UserIdentity>>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");
            services.AddAuthentication(options =>
            {
                options.AddScheme<OAuthAuthenticationScheme>("OAuth", "OAuth");

                options.DefaultAuthenticateScheme = "OAuth";
                options.DefaultChallengeScheme = "OAuth";

            });

            services.AddAuthorization();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbcontext, UserManager<UserIdentity> usermanager)
        {
            new DemoSetup(dbcontext, usermanager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseDebugMiddleware();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BugHub>("/BugHub");
                endpoints.MapControllers();
            });
        }
    }
}
