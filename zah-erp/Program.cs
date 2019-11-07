using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.IO;

namespace zah_erp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureWebHostDefaults(webHostBuilder =>
              {
                  webHostBuilder
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseIISIntegration()
                      .UseStartup<Startup>();
              })
              .Build();
            host.Run();
        }
    }
}
