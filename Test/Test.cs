public static class Test
{
    public static void Main(String[] args)
    {
        Console.WriteLine("EJTree test...");
        Console.WriteLine("Config test defaults");

        Config.Config config = new Config.Config();
        config.PrintConfig();

        Console.WriteLine("\nConfig test from file");

        config.InitializeFromFile();
        config.PrintConfig();

        Console.WriteLine("\nLogger init to Console");

        Logger.Logger logger = Logger.Logger.GetInstance();
        logger.SetLogLevel(Common.LogLevel.Info);
        logger.SetLogFile();
        
    }
}
