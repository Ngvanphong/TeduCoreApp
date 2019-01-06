using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using TeduCoreApp.Application.Implementation;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Helpers;
using TeduCoreApp.WebApi.ServiceLocators;
using TeduCoreApp.WebApi.Signalr;

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
            //Token
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            // Api Page
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TeduCoreApp",
                });
            });
            //Crors orgin
            services.AddCors(o => o.AddPolicy("TeduCorsPolicy", builder =>
               builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            ));

            // Indentity
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            //Config Indentity
            services.Configure<IdentityOptions>(option =>
            {
                //password setting
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                //lock setting
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                option.Lockout.MaxFailedAccessAttempts = 10;
                // check had email
                option.User.RequireUniqueEmail = true;
            });

            //Config authen
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });

            //Automapper
            services.AddAutoMapper();
            //var configMappper = AutoMapperConfig.RegisterMappings();
            //services.AddScoped(sp => configMappper.CreateMapper());

            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            //UnitOfWork
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            //Cliam
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactoryApi>();

            //Permission
            services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationCrudHandler>();

  
            //Repository

            services.AddTransient<IRepository<ProductCategory, int>, EFRepository<ProductCategory, int>>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IRepository<Product, int>, EFRepository<Product, int>>();
            services.AddTransient<IRepository<ProductTag, int>, EFRepository<ProductTag, int>>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRepository<ProductImage, int>, EFRepository<ProductImage, int>>();
            services.AddTransient<IRepository<Size, int>, EFRepository<Size, int>>();
            services.AddTransient<IRepository<Color, int>, EFRepository<Color, int>>();
            services.AddTransient<IRepository<ProductQuantity, int>, EFRepository<ProductQuantity, int>>();
            services.AddTransient<IRepository<Blog, int>, EFRepository<Blog, int>>();
            services.AddTransient<IRepository<BlogTag, int>, EFRepository<BlogTag, int>>();
            services.AddTransient<IRepository<BlogImage, int>, EFRepository<BlogImage, int>>();
            services.AddTransient<IRepository<Slide, int>, EFRepository<Slide, int>>();
            services.AddTransient<IRepository<Bill, int>, EFRepository<Bill, int>>();
            services.AddTransient<IRepository<BillDetail, int>, EFRepository<BillDetail, int>>();
            services.AddTransient<IRepository<BillUserAnnoucement, int>, EFRepository<BillUserAnnoucement, int>>();
            services.AddTransient<IRepository<WholePrice, int>, EFRepository<WholePrice, int>>();
            services.AddTransient<IRepository<Advertistment,int>, EFRepository<Advertistment,int>>();
            services.AddTransient<IRepository<AdvertistmentPage, string>, EFRepository<AdvertistmentPage, string>>();
            services.AddTransient<IRepository<AdvertistmentPosition, string>, EFRepository<AdvertistmentPosition, string>>();
            services.AddTransient<IRepository<Data.Entities.Contact, string>, EFRepository<Data.Entities.Contact, string>>();
            services.AddTransient<IRepository<Page, int>, EFRepository<Page, int>>();
            services.AddTransient<IRepository<PageImage, int>, EFRepository<PageImage, int>>();
            services.AddTransient<IRepository<Pantner, int>, EFRepository<Pantner, int>>();
            services.AddTransient<IRepository<Subcrible, int>, EFRepository<Subcrible, int>>();

            // Service
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IProductQuantityService, ProductQuantityService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IBlogImageService, BlogImageService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IBillUserAnnoucementService, BillUserAnnoucementService>();
            services.AddTransient<IWholePriceService, WholePriceService>();
            services.AddTransient<IAdvertistmentService, AdvertistmentService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IPageImageService, PageImageService>();
            services.AddTransient<IPantnerService, PantnerService>();
            services.AddTransient<ISubcribleService, SubcribleService>();

            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());

            services.AddMvc()
                .AddJsonOptions(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ContractResolver =
                new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //set allow any domain
            app.UseCors("TeduCorsPolicy");
            // use swash
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1.1"));

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

            app.UseSignalR(routes =>
            {
                routes.MapHub<WebHub>("/hub");
            });
        }
    }
}