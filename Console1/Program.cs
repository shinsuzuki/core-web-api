using Microsoft.Extensions.Configuration;
using NLog;

namespace Console1
{
    internal class Program
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            // 設定ファイル
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json")
                .Build();

            Console.WriteLine(configuration["Url:WebAPIBaseUrl"]);

            // ログ
            for (int i = 0; i < 10; i++)
            {
                logger.Trace("tarace");
                logger.Info("information");
                logger.Warn("waraninig");
                logger.Error("error");
                logger.Fatal("fatal"); ;
            }



            Console.WriteLine("\npress enter key to exit...");
            Console.ReadLine();
        }
    }
}
