using System;

namespace Config
{
    public class Config
    {
        private String m_currentConfigPath;

        private bool m_initialized = false;
        private Dictionary<String, String>? m_configVals = null;

        // configuration keys
        private const String m_orderKey = "ORDER";
        private const String m_persistenceModeKey = "PERSISTENCE MODE";
        private const String m_logFilePathKey = "LOG FILE PATH";
        private const String m_logLevelKey = "LOG LEVEL";

        // default configuration values
        private const int m_defaultOrder = 2;
        private const PersistenceMode m_defaultPersistenceMode = PersistenceMode.Memory;
        private String m_defaultLogFilePath = Environment.CurrentDirectory + "\\log.txt";
        private const LogLevel m_defaultLogLevel = LogLevel.Info;

        public Config() : this(Environment.CurrentDirectory + "\\config.txt") { }
        public Config(String configPath)
        {
            m_currentConfigPath = configPath;
        }

        public void InitializeFromFile()
        {
            m_initialized = false;

            m_configVals = new Dictionary<String, String>();
            String[] lines = File.ReadAllLines(m_currentConfigPath);

            foreach (String line in lines)
            {
                if (line.StartsWith('#'))
                {
                    continue;
                }

                String[] parts = line.Split(':');

                if (parts.Length != 2)
                {
                    throw new ConfigurationException("Invalid configuration line: " + line);
                }

                m_configVals.Add(parts[0].Trim().ToUpper(), parts[1].Trim());
            }

            m_initialized = true;
        }

        public int GetOrder()
        {
            if ((m_configVals != null) && m_configVals.ContainsKey(m_orderKey))
            {
                return Int32.Parse(m_configVals[m_orderKey]);
            }
            return m_defaultOrder;
        }

        public PersistenceMode GetPersistenceMode()
        {
            if ((m_configVals != null) && m_configVals.ContainsKey(m_persistenceModeKey))
            {
                return Enum.Parse<PersistenceMode>(m_configVals[m_persistenceModeKey]);
            }

            return m_defaultPersistenceMode;
        }

        public String GetLogFilePath()
        {
            if ((m_configVals != null) && m_configVals.ContainsKey(m_logFilePathKey))
            {
                return m_configVals[m_logFilePathKey];
            }

            return m_defaultLogFilePath;
        }

        public LogLevel GetLogLevel()
        {
            if ((m_configVals != null) && m_configVals!.ContainsKey(m_logLevelKey))
            {
                return Enum.Parse<LogLevel>(m_configVals[m_logLevelKey]);
            }

            return m_defaultLogLevel;
        }

        public void PrintConfig()
        {
            Console.WriteLine("\nConfig Initialized from file: " + m_initialized);

            if (m_configVals != null)
            {
                Console.WriteLine("Config: Current config path: " + m_currentConfigPath);
                Console.WriteLine("Config key count (from config file): " + m_configVals!.Count);

                foreach (String key in m_configVals.Keys)
                {
                    String val = m_configVals[key];
                    Console.WriteLine(key + " : " + val);
                }
            }

            Console.WriteLine("\nUsing defaults for keys not found in config:");

            if ((m_configVals == null) || !m_configVals.ContainsKey(m_orderKey))
            {
                Console.WriteLine(m_orderKey + " : " + GetOrder());
            }
            if ((m_configVals == null) || !m_configVals.ContainsKey(m_persistenceModeKey))
            {
                Console.WriteLine(m_persistenceModeKey + " : " + GetPersistenceMode());
            }
            if ((m_configVals == null) || !m_configVals.ContainsKey(m_logFilePathKey))
            {
                Console.WriteLine(m_logFilePathKey + " : " + GetLogFilePath());
            }
            if ((m_configVals == null) || !m_configVals.ContainsKey(m_logLevelKey))
            {
                Console.WriteLine(m_logLevelKey + " : " + GetLogLevel());
            }
        }
    }
}