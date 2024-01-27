namespace mvc_api.Config
{
    public class MyConfig: IMyConfig
    {
        IConfigurationRoot _configurationRoot;
        
        public MyConfig()
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfigurationRoot GetConfigurationRoot()
        {
            return _configurationRoot;
        }
    }
}
