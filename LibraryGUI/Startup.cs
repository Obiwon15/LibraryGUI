using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryGUI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryGUI.Data.Services;
using LibraryGUI.Data.Interfaces;
using System.Net.Mail;
using System.Net;
using LibraryGUI.Models;

namespace LibraryGUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddTransient<EmailHelper>();
            services.AddTransient<SmtpClient>((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<String>("Email:Smtp:smtp.gmail.com"),
                    Port = config.GetValue<int>("Email:Smtp:25"),
                    Credentials = new NetworkCredential(
                                            config.GetValue<String>("Email:Smtp:slaterzamani.com"),
                                            config.GetValue<String>("Email:Smtp:micheal123")
                                            )
                };

            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                //opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

            }).AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                /*.AddTokenProvider<EmailTokenProvider<Owner>>("emailconfirmation")*/;

            //services.Configure<DataProtectionTokenProviderOptions>(opt => 
            //opt.TokenLifespan = TimeSpan.FromHours(2));

            //services.Configure<DataProtectionTokenProviderOptions>(opt => 
            //opt.TokenLifespan = TimeSpan.FromDays(3));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("readonlypolicy", builder => builder.RequireRole("Admin", "Manager", "Audit"));
                options.AddPolicy("writepolicy", builder => builder.RequireRole("Admin", "Manager"));
                });
                
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
