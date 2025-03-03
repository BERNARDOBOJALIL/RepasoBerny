using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepasoBerny.Shared.Entities;
using RepasoBerny.Web.Data;
using RepasoBerny.Web.Helpers;

namespace RepasoBerny.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext <DataContext>(x => x.UseSqlServer("name = con"));
            builder.Services.AddScoped<IUserHelper, UserHelper>();
            builder.Services.AddTransient<Seeder>();
            builder.Services.AddIdentity<User, IdentityRole>(
                x=>
                {
                    x.User.RequireUniqueEmail = true;
                    x.Password.RequireDigit = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequiredLength = 6;
                }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            var app = builder.Build();
            SeederApp(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void SeederApp(WebApplication app)
        {
            IServiceScopeFactory? serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using (IServiceScope? serviceScope = serviceScopeFactory!.CreateScope())
            {
                Seeder? seeder = serviceScope.ServiceProvider.GetService<Seeder>();
                seeder!.SeedAsync().Wait();
            }
        }
    }
}
