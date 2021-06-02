using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.ListenAnyIP(80);
                        options.ListenAnyIP(443, listenOptions =>
                       {
                           listenOptions.UseHttps("localhost.pfx", "443");
                       });
                    });
                });
    }
}
