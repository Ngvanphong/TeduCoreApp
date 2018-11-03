using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using TeduCoreApp.Application.AutoMapper;
using TeduCoreApp.Application.Implementation;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.WebApi
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("TeduCoreApp.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            // Api Page
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title="TeduCoreApp",
                });
            });
            //Crors orgin
            services.AddCors(o => o.AddPolicy("TeduCorsPolicy", builder =>
               builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            ));

            //Automapper
            services.AddAutoMapper();
            var configMappper = AutoMapperConfig.RegisterMappings();
            services.AddScoped<IMapper>(sp => configMappper.CreateMapper());
            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            //UnitOfWork
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            //Repository 
            services.AddTransient<IRepository<ProductCategory, int>, EFRepository<ProductCategory, int>>();

            // Service
            services.AddTransient<IProductCategoryService, ProductCategoryService>();

            services.AddMvc()
                .AddJsonOptions(option=>option.SerializerSettings.ContractResolver=new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //set allow any domain
            app.UseCors("TeduCorsPolicy");
            // use swash
            app.UseSwagger();
            app.UseSwaggerUI(s=>s.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1.1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseMvc();
        }
    }
}
