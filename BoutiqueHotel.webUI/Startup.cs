using System;
using System.IO;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.business.Concrete;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.data.Concrete.EfCore;
using BoutiqueHotel.webUI.EmailServices;
using BoutiqueHotel.webUI.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BoutiqueHotel.webUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection")));
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                //Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(100);
                options.Lockout.AllowedForNewUsers = true;

                //E-Mail                
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            //Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".BoutiqueHotel.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:Username"],
                    _configuration["EmailSender:Password"]
                    )
                );

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICartService cartService)
        {
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"
            });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
             (
                 name: "allorders",
                 pattern: "allorders",
                 defaults: new { controller = "Order", action = "GetAllUserOrders" }
             );

                endpoints.MapControllerRoute
             (
                 name: "orders",
                 pattern: "orders",
                 defaults: new { controller = "Order", action = "GetUserOrders" }
             );

                endpoints.MapControllerRoute
              (
                  name: "checkout",
                  pattern: "checkout",
                  defaults: new { controller = "Cart", action = "Checkout" }
              );

                endpoints.MapControllerRoute
               (
                   name: "cart",
                   pattern: "cart",
                   defaults: new { controller = "Cart", action = "Index" }
               );

                endpoints.MapControllerRoute
                 (
                     name: "adminproducts",
                     pattern: "admin/products",
                     defaults: new { controller = "Admin", action = "ProductList" }
                 );

                endpoints.MapControllerRoute
                (
                    name: "adminaddproduct",
                    pattern: "admin/products/add",
                    defaults: new { controller = "Admin", action = "AddProduct" }
                );

                endpoints.MapControllerRoute
              (
                  name: "admineditproduct",
                  pattern: "admin/products/{id?}",
                  defaults: new { controller = "Admin", action = "EditProduct" }
              );

                endpoints.MapControllerRoute
                (
                    name: "admincategories",
                    pattern: "admin/categories",
                    defaults: new { controller = "Admin", action = "CategoryList" }
                );

                endpoints.MapControllerRoute
                (
                    name: "adminaddcategory",
                    pattern: "admin/categories/add",
                    defaults: new { controller = "Admin", action = "AddCategory" }
                );

                endpoints.MapControllerRoute
             (
                 name: "admineditcategory",
                 pattern: "admin/categories/{id?}",
                 defaults: new { controller = "Admin", action = "EditCategory" }
             );

                endpoints.MapControllerRoute
                 (
                     name: "search",
                     pattern: "search",
                     defaults: new { controller = "Order", action = "search" }
                 );

                endpoints.MapControllerRoute
                 (
                     name: "products",
                     pattern: "products/{category?}",
                     defaults: new { controller = "Order", action = "list" }
                 );

                endpoints.MapControllerRoute
               (
                   name: "productDetails",
                   pattern: "{url}",
                   defaults: new { controller = "Order", action = "details" }
               );

                endpoints.MapControllerRoute
                 (
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute
               (
                   name: "adminroles",
                   pattern: "admin/roles",
                   defaults: new { controller = "Admin", action = "RoleList" }
               );

                endpoints.MapControllerRoute
               (
                   name: "adminaddrole",
                   pattern: "admin/roles/add",
                   defaults: new { controller = "Admin", action = "AddRole" }
               );

                endpoints.MapControllerRoute
              (
                  name: "admineditrole",
                  pattern: "admin/roles/{id?}",
                  defaults: new { controller = "Admin", action = "EditRole" }
              );

                endpoints.MapControllerRoute
               (
                   name: "adminusers",
                   pattern: "admin/users",
                   defaults: new { controller = "Admin", action = "UserList" }
               );

                endpoints.MapControllerRoute
              (
                  name: "adminedituser",
                  pattern: "admin/users/{id?}",
                  defaults: new { controller = "Admin", action = "EditUser" }
              );
            });
            SeedIdentity.Seed(userManager, roleManager, cartService, configuration).Wait();
        }
    }
}
