using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerException : Exception
    {
        public LoggerException(String message) : base(message) { }
    }
}
