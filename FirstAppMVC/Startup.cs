using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FirstAppMVC.Services.Products;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.EntityConfiguration;
using FirstAppMVC.UI.Services.Products;
using FirstAppMVC.Services.Categories;
using FirstAppMVC.Services.Brands;
using FirstAppMVC.Services.Orders;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Services.Basket;

namespace FirstAppMVC
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
            string connectionString = Configuration.GetConnectionString("MainConnection");
            var optionBuilder = new DbContextOptionsBuilder();
            optionBuilder.UseSqlServer(connectionString);

            services.AddScoped<IEntityConfigurationContainer>(sp => new EntityConfigurationContainer());

            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            }
            );

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    options.ClientId = "373489560431-ei6tf5tv7bi4onvunmpi2ovt669kek7o.apps.googleusercontent.com";
            //    options.ClientSecret = "_GecxjCaQR23jjE50oli3CG8";
            //});

            services.AddSingleton<IApplicationDbContextFactory>(
                sp => new ApplicationDbContextFactory(optionBuilder.Options, new EntityConfigurationContainer()));

            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IBrandService, BrandService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();

            Mapper.Initialize(c => c.AddProfile(new MappingProfile()));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
                endpoints.MapRazorPages();
            });
        }
    }
}
