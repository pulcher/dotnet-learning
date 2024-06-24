using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Set up dependency injection
        var serviceProvider = new ServiceCollection();
        ConfigureServices(serviceProvider);

        var services = serviceProvider.BuildServiceProvider();

        // Use MyService
        var myService = services.GetService<MyService>();

        while (true)
        {
            myService.DoSomething();
            Task.Delay(1000).Wait();


            // Then change the appsetting.json in the bin dir.
            // I thought this would make it happen, but I guess not.
            // a full restart does though.
            var myOtherService = services.GetService<MyService>();
            myOtherService.DoSomething();
            Task.Delay(1000).Wait();
        }
    }

    private static void ConfigureServices(ServiceCollection serviceProvider)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        serviceProvider.Configure<MySettings>(configuration.GetSection("MySettings"));
        serviceProvider.AddSingleton<MyService>();
    }
}
