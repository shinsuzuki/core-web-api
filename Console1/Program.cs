using Microsoft.Extensions.Configuration;

namespace Console1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json")
                .Build();

            Console.WriteLine(configuration["Url:WebAPIBaseUrl"]);
            Console.ReadLine();
            
        }
    }
}
