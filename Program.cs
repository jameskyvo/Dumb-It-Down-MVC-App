using LaserCatEyes.HttpClientListener;
using OpenAI.GPT3;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace DumbItDown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddOpenAIService();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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