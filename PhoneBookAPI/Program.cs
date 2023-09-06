using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBookAPI;
using PhoneBookAPI.Data;
using System.Reflection;

public class Program
{

    public static void Main(string[] args)
    {
        

        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Default CreateBuilder
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<StartUp>();
        }).ConfigureLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();
        });
        
        return builder;
    }

}