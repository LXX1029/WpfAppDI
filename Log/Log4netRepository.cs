using System;
using System.Collections.Generic;
using System.Text;
using log4net.Repository;

namespace WpfAppDI.Log
{
    public class Log4netRepository
    {
        public static ILoggerRepository loggerRepository { get; set; }
    }
}
