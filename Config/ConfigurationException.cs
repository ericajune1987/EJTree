using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    internal class ConfigurationException : Exception
    {
        public ConfigurationException(String message) : base(message) { }
    }
}
