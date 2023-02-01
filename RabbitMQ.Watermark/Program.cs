using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Watermark.Models;
using RabbitMQ.Watermark.Services;

namespace RabbitMQ.Watermark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            ConfigurationManager configurationManager = new ConfigurationManager();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "productDb");
            });
            builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(" Write your own rabbitmq URI"),DispatchConsumersAsync=true});
            builder.Services.AddSingleton<RabbitMQClientService>();
            builder.Services.AddSingleton<RabbitMQPublisher>();
            //builder.Services.AddHostedService<ImageWatermarkProcessBackgroundService>();

            var app = builder.Build();

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

















//In this project In this project, the use of rabbitmq with .net core is simulated on the scenario of adding watermarks to the images.
//Before I start to explain the project, I would like to briefly talk about what rabbit MQ is used for and how it works.