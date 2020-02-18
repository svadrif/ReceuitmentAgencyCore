using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecruitmentAgencyCore.InitialData;
using RecruitmentAgencyCore.Security;
using RecruitmentAgencyCore.Data;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Service.Interfaces;
using RecruitmentAgencyCore.Service.Models;
using RecruitmentAgencyCore.Service.Services;

namespace RecruitmentAgencyCore
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
               
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            services.AddMvc()
                .AddSessionStateTempDataProvider();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;             
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders()
              .AddUserStore<RecruitmentAgencyUserStore>()
              .AddUserManager<RecruitmentAgencyUserManager>()
              .AddSignInManager<RecruitmentAgencySignInManager>();



            #region GenericRepository 

            services.AddTransient<IGenericRepository<Branch>, GenericRepository<Branch>>();
            services.AddTransient<IGenericRepository<Citizenship>, GenericRepository<Citizenship>>();
            services.AddTransient<IGenericRepository<Country>, GenericRepository<Country>>();
            services.AddTransient<IGenericRepository<Currency>, GenericRepository<Currency>>();
            services.AddTransient<IGenericRepository<District>, GenericRepository<District>>();
            services.AddTransient<IGenericRepository<Education>, GenericRepository<Education>>();
            services.AddTransient<IGenericRepository<EducationType>, GenericRepository<EducationType>>();
            services.AddTransient<IGenericRepository<Employer>, GenericRepository<Employer>>();
            services.AddTransient<IGenericRepository<Experience>, GenericRepository<Experience>>();
            services.AddTransient<IGenericRepository<FamilyStatus>, GenericRepository<FamilyStatus>>();
            services.AddTransient<IGenericRepository<ForeignLanguage>, GenericRepository<ForeignLanguage>>();
            services.AddTransient<IGenericRepository<Gender>, GenericRepository<Gender>>();
            services.AddTransient<IGenericRepository<JobSeeker>, GenericRepository<JobSeeker>>();
            services.AddTransient<IGenericRepository<JobSeekerForeignLanguage>, GenericRepository<JobSeekerForeignLanguage>>();
            services.AddTransient<IGenericRepository<LanguageLevel>, GenericRepository<LanguageLevel>>();
            services.AddTransient<IGenericRepository<Menu>, GenericRepository<Menu>>();
            services.AddTransient<IGenericRepository<MenuRolePermission>, GenericRepository<MenuRolePermission>>();
            services.AddTransient<IGenericRepository<Permission>, GenericRepository<Permission>>();
            services.AddTransient<IGenericRepository<PreviousWork>, GenericRepository<PreviousWork>>();
            services.AddTransient<IGenericRepository<Region>, GenericRepository<Region>>();
            services.AddTransient<IGenericRepository<Response>, GenericRepository<Response>>();
            services.AddTransient<IGenericRepository<Resume>, GenericRepository<Resume>>();
            services.AddTransient<IGenericRepository<Role>, GenericRepository<Role>>();
            services.AddTransient<IGenericRepository<Schedule>, GenericRepository<Schedule>>();
            services.AddTransient<IGenericRepository<SocialStatus>, GenericRepository<SocialStatus>>();
            services.AddTransient<IGenericRepository<Subscription>, GenericRepository<Subscription>>();
            services.AddTransient<IGenericRepository<TypeOfEmployment>, GenericRepository<TypeOfEmployment>>();
            services.AddTransient<IGenericRepository<University>, GenericRepository<University>>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Vacancy>, GenericRepository<Vacancy>>();

            #endregion

            //services.AddTransient<Seeder>();

            services.AddTransient<IMenuBuilder, MenuBuilder>();
            services.AddTransient<MenuService>();

            services.AddScoped<CustomUserStore>();
            services.AddScoped<RecruitmentAgencyUserStore>();
            services.AddScoped<RecruitmentAgencyUserManager>();
            services.AddScoped<RecruitmentAgencySignInManager>();

            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //using IServiceScope scope = app.ApplicationServices.CreateScope();
                //Seeder seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
                //await seeder.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
          
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}