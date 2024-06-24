using Microsoft.Extensions.Configuration;

namespace consoleIOptions
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuraiton = builder.Build();

            var mySettings = new MySetting();
            configuraiton.GetSection("MySettings").Bind(mySettings);

            while (true)
            {
                Console.WriteLine($"key1: {mySettings.Key1}, key2: {mySettings.Key2}, key3: {mySettings.Key3}");
                Task.Delay(2000).Wait();
            }
        }
    }

    public class MySetting
    {
        public string? Key1 { get; set; }
        public string? Key2 { get; set; }
        public int? Key3 { get; set; }
    }

}
