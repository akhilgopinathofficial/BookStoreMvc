using AutoMapper;
using BookStore.BL.Infrastructure;
using BookStore.Common;
using BookStore.Data.Configuration;
using BookStore.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web
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
            services.AddControllersWithViews();
            //Fetching Connection string from APPSETTINGS.JSON  
            var ConnectionString = Configuration.GetConnectionString("BookDbConstr");
            //Entity Framework  
            services.AddDbContext<BookContext>(options => options.UseSqlServer(ConnectionString));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperConfigWeb());
                cfg.AddProfile(new AutomapperConfigBL());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            RegisterDependencies.RegisterServices(services);
            RegisterDependencies.RegisterRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

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
