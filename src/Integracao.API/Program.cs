using Integracao_Glpi_DevOps;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Serilog;

namespace IntegracaoGLPI_DevOps;

public class Program
{
    public static void Main(string[] args) { CreateHostBuilder(args).Build().Run(); }
    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}

