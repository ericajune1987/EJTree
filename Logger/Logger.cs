using Common;

namespace Logger
{
    public class Logger
    {
        private static readonly object m_lock = new object();

        private static readonly Logger instance;

        private LogLevel m_logLevel;
        private String? m_logFilePath;
        private String? m_logFileName;
        private bool m_logFileSet;
        private Logger() { }
        static Logger()
        {
            instance = new Logger();
            instance.m_logLevel = LogLevel.Info;
            instance.m_logFilePath = null;
            instance.m_logFileName = null;
            instance.m_logFileSet = false;
        }

        public static Logger GetInstance()
        {
            return instance;
        }

        public void SetLogLevel(LogLevel logLevel)
        {
            m_logLevel = logLevel;
        }

        public void SetLogFile(String? logFilePath = null, String? logFileName = null)
        {
            lock (m_lock)
            {
                if(m_logFileSet == true)
                {
                    throw new LoggerException("Logger log file already set, may only be set once");
                }

                if (((logFilePath == null) && (logFileName != null)) || 
                    ((logFileName == null) && (logFilePath != null)))
                {
                    throw new LoggerException("Logger SetLogFile arguments invalid");
                }

                m_logFilePath = logFilePath;
                m_logFileName = logFileName;
                m_logFileSet = true;

                String msg = $"\n{DateTime.UtcNow} Logger initialized to logFile {logFilePath} logFileName {logFileName} logLevel {m_logLevel}";

                if (m_logFilePath != null)
                {
                    File.WriteAllText(m_logFilePath + "\\" + m_logFileName, msg);
                } else
                {
                    Console.WriteLine(msg);
                }
            }
        }

        public void Log(LogLevel logLevel, String message)
        {
            if ((m_logLevel == LogLevel.Off) || (logLevel == LogLevel.Off))
            {
                return;
            }

            if (m_logLevel <= logLevel)
            {
                String level = Enum.GetName(typeof(LogLevel), logLevel)!.ToUpper();
                String msg = $"\n{level} {DateTime.UtcNow} {message}";

                if (m_logFilePath != null)
                {
                    File.WriteAllText(m_logFilePath + "\\" + m_logFileName, msg);
                } else
                {
                    Console.WriteLine(msg);
                }
            }
        }
    }
}