using DumperApplicationCore;
using DumperApplicationCore.BusinessLogic;
using Microsoft.AspNetCore.Rewrite;

namespace DumperWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSqlServerConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
            builder.Services.AddScoped(typeof(DumpAndFetch));

            var options = new RewriteOptions()
                .AddRewrite(@"^v?=(.*)$", "Dumper/Within/$1", skipRemainingRules: true);

            var app = builder.Build();
            app.UseRewriter(options);

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
    }
}