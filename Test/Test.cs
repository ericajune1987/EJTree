using EJTree;
using Logger;
using System.Collections;
using System.Text;

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

        Console.WriteLine("\nTest KeySet");

        IKeySet<int> keys = new KeySet<int>();
        LogKeySetState(logger, keys);

        keys.AddKey(1);
        LogKeySetState(logger, keys);

        keys.AddKey(2);
        LogKeySetState(logger, keys);

        keys.AddKey(5);
        LogKeySetState(logger, keys);

        keys.AddKey(4);
        LogKeySetState(logger, keys);

        keys.AddKey(0);
        LogKeySetState(logger, keys);

        keys.AddKey(3);
        LogKeySetState(logger, keys);
    }

    public static void LogKeySetState<T>(Logger.Logger logger, IKeySet<T> keys) where T : IComparable
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("size: " + keys.Size() + " ");

        if (keys.Size() > 0)
        {
            sb.Append("first: " + keys.FirstKey() + " ");
            sb.Append("last: " + keys.LastKey() + " ");
        }

        sb.Append("keys: ");
        foreach (IComparable key in keys)
        {
            if (key != null)
            {
                sb.Append(key.ToString() + " ");
            } else
            {
                sb.Append("null ");
            }
        }

        logger.Log(Common.LogLevel.Info, sb.ToString());
    }
}
